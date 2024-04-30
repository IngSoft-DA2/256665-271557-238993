using System.Collections;
using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Test.Repositories;

[TestClass]
public class CategoryRepositoryTest
{
    private DbContext _dbContext;
    private CategoryRepository _categoryRepository;

    [TestInitialize]
    public void TestInitialize()
    {
        _dbContext = CreateDbContext("CategoryRepositoryTest");
        _dbContext.Set<Category>();
        _categoryRepository = new CategoryRepository(_dbContext);
    }

    [TestMethod]
    public void GetAllCategories_CategoriesAreReturn()
    {
        Category categoryInDb = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Category1"
        };
        Category categoryInDb2 = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Category2"
        };
        
        IEnumerable<Category> expectedCategories = new List<Category> {categoryInDb, categoryInDb2};

        _dbContext.Set<Category>().Add(categoryInDb);
        _dbContext.Set<Category>().Add(categoryInDb2);
        _dbContext.SaveChanges();
        
        IEnumerable<Category> categoriesResponse = _categoryRepository.GetAllCategories();
        
        Assert.IsTrue(expectedCategories.SequenceEqual(categoriesResponse));
    }

    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
    }
}