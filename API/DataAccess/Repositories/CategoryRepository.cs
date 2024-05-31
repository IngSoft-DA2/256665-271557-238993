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


    public IEnumerable<Category> GetAllCategories()
    {
        try
        {
            return _dbContext.Set<Category>().ToList();
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
            CategoryComponent categoryFound = _dbContext.Set<CategoryComponent>().Find(categoryId);
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

    public void DeleteCategory(CategoryComponent category)
    {
        try
        {
            _dbContext.Set<CategoryComponent>().Remove(category);
            _dbContext.Entry(category).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public void UpdateCategory(CategoryComponent categoryFatherComposite)
    {
        throw new NotImplementedException();
    }
}