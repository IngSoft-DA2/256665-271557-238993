using DataAccess.DbContexts;
using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class CategoryRepository :  ICategoryRepository
{
    private readonly DbContext _dbContext;
    public CategoryRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public IEnumerable<Category> GetAllCategories()
    {
        return _dbContext.Set<Category>().ToList();
    }

    public Category GetCategoryById(Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public void CreateCategory(Category categoryToAdd)
    {
        throw new NotImplementedException();
    }
}