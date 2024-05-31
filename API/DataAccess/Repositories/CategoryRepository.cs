using DataAccess.DbContexts;
using Domain;
using IDataAccess;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

namespace DataAccess.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DbContext _dbContext;

    public CategoryRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public IEnumerable<CategoryComponent> GetAllCategories()
    {
        try
        {
            return _dbContext.Set<CategoryComponent>().ToList();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public CategoryComponent GetCategoryById(Guid categoryId)
    {
        try
        {
            var categoryFound = _dbContext.Set<CategoryComponent>().FirstOrDefault(x => x.Id == categoryId);
            
            if (categoryFound is CategoryComposite composite)
            {
                _dbContext.Entry(composite).Collection(c => c.SubCategories).Load();
            }

            return categoryFound;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public void CreateCategory(CategoryComponent categoryToAdd)
    {
        try
        {
            _dbContext.Set<CategoryComponent>().Add(categoryToAdd);
            _dbContext.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public void DeleteCategory(CategoryComponent categoryComponent)
    {
        try
        {
            _dbContext.Set<CategoryComponent>().Remove(categoryComponent);
            _dbContext.Entry(categoryComponent).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public void UpdateCategory(CategoryComponent categoryComponentWithChanges)
    {
        CategoryComponent categoryComponentInDb =
            _dbContext.Set<CategoryComponent>().Find(categoryComponentWithChanges.Id);

        if (categoryComponentInDb is not null)
        {
            _dbContext.Entry(categoryComponentInDb).CurrentValues.SetValues(categoryComponentWithChanges);

            List<CategoryComponent>? childs = categoryComponentWithChanges.GetChilds();
            
            if (childs is not null)
            {   
                int childsCount = categoryComponentWithChanges.GetChilds().Count;
                for (int i = 0; i < childsCount; i++)
                {
                    categoryComponentInDb.AddChild(childs[i]);
                }
            }

            _dbContext.SaveChanges();
        }
    }
}