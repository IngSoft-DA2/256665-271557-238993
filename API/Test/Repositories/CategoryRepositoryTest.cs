using System.Collections;
using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories.CustomExceptions;

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
    
    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
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
    
    [TestMethod]
    public void GetAllCategories_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<Category>()).Throws(new Exception());
        
        _categoryRepository = new CategoryRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _categoryRepository.GetAllCategories());
        _mockDbContext.VerifyAll();
    }

    [TestMethod]
    public void GetCategoryById_CategoryIsReturn()
    {
        Category categoryInDb = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Category1"
        };
        
        _dbContext.Set<Category>().Add(categoryInDb);
        _dbContext.SaveChanges();
        
        Category categoryResponse = _categoryRepository.GetCategoryById(categoryInDb.Id);
        
        Assert.AreEqual(categoryInDb, categoryResponse);
    }
    
    [TestMethod]
    public void GetCategoryById_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<Category>()).Throws(new Exception());
        
        _categoryRepository = new CategoryRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _categoryRepository.GetCategoryById(Guid.NewGuid()));
        _mockDbContext.VerifyAll();
    }
    
    [TestMethod]
    public void CreateCategory_CategoryIsAdded()
    {
        Category categoryToAdd = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Category1"
        };
        
        _categoryRepository.CreateCategory(categoryToAdd);
        Category categoryInDb = _dbContext.Set<Category>().Find(categoryToAdd.Id);
        
        Assert.AreEqual(categoryToAdd, categoryInDb);
    }
    
    [TestMethod]
    public void CreateCategory_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<Category>()).Throws(new Exception());
        
        _categoryRepository = new CategoryRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _categoryRepository.CreateCategory(new Category()));
        _mockDbContext.VerifyAll();
    }
    
}