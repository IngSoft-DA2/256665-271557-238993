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
    #region Initialize

    private DbContext _dbContext;
    private CategoryRepository _categoryRepository;

    [TestInitialize]
    public void TestInitialize()
    {
        _dbContext = CreateDbContext("CategoryRepositoryTest");
        _dbContext.Set<CategoryComponent>();
        _categoryRepository = new CategoryRepository(_dbContext);
    }

    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
    }

    #endregion

    #region Get all categories

    [TestMethod]
    public void GetAllCategories_CategoriesAreReturn()
    {
        CategoryComponent categoryInDb = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Category1"
        };
        CategoryComponent categoryInDb2 = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Category2"
        };

        Guid categoryCompositeId = Guid.NewGuid();

        CategoryComponent subCategory = new Category
        {
            Id = Guid.NewGuid(),
            Name = "SubCategory",
            CategoryFatherId = categoryCompositeId
        };

        CategoryComponent categoryComposite = new CategoryComposite
        {
            Id = categoryCompositeId,
            Name = "CategoryComposite",
            SubCategories = new List<CategoryComponent> { subCategory }
        };

        IEnumerable<CategoryComponent> expectedCategories = new List<CategoryComponent>
            { categoryInDb, categoryInDb2, subCategory, categoryComposite };

        _dbContext.Set<CategoryComponent>().Add(categoryInDb);
        _dbContext.Set<CategoryComponent>().Add(categoryInDb2);
        _dbContext.Set<CategoryComponent>().Add(categoryComposite);
        _dbContext.SaveChanges();

        IEnumerable<CategoryComponent> categoriesResponse = _categoryRepository.GetAllCategories();

        Assert.IsTrue(expectedCategories.SequenceEqual(categoriesResponse));
    }

    [TestMethod]
    public void GetAllCategories_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<CategoryComponent>()).Throws(new Exception());

        _categoryRepository = new CategoryRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _categoryRepository.GetAllCategories());
        _mockDbContext.VerifyAll();
    }

    #endregion

    #region Get category component by id

    [TestMethod]
    public void GetCategoryById_CategoryIsReturn()
    {
        Guid categoryCompositeId = Guid.NewGuid();

        CategoryComponent subCategory = new Category
        {
            Id = Guid.NewGuid(),
            Name = "SubCategory",
            CategoryFatherId = categoryCompositeId
        };

        CategoryComponent categoryComposite = new CategoryComposite
        {
            Id = categoryCompositeId,
            Name = "CategoryComposite",
            SubCategories = new List<CategoryComponent> { subCategory }
        };

        _dbContext.Set<CategoryComponent>().Add(categoryComposite);
        _dbContext.SaveChanges();

        CategoryComponent categoryResponse = _categoryRepository.GetCategoryById(categoryComposite.Id);

        Assert.AreEqual(categoryComposite, categoryResponse);
    }

    [TestMethod]
    public void GetCategoryById_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<CategoryComponent>()).Throws(new Exception());

        _categoryRepository = new CategoryRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _categoryRepository.GetCategoryById(Guid.NewGuid()));
        _mockDbContext.VerifyAll();
    }

    #endregion

    #region Create Category

    [TestMethod]
    public void CreateCategoryLeaf_CategoryIsAdded()
    {
        Guid categoryFatherId = Guid.NewGuid();
        CategoryComponent categoryToAdd = new CategoryComposite()
        {
            Id = categoryFatherId,
            Name = "Category1",
            SubCategories = new List<CategoryComponent>
            {
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "SubCategory",
                    CategoryFatherId = categoryFatherId
                }
            }
        };

        _categoryRepository.CreateCategory(categoryToAdd);
        CategoryComponent categoryInDb = _dbContext.Set<CategoryComponent>().Find(categoryToAdd.Id);

        Assert.AreEqual(categoryToAdd, categoryInDb);
    }

    [TestMethod]
    public void CreateCategory_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<CategoryComponent>()).Throws(new Exception());

        _categoryRepository = new CategoryRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _categoryRepository.CreateCategory(new Category()));
        _mockDbContext.VerifyAll();
    }

    #endregion

    #region Delete Category

    [TestMethod]
    public void DeleteCategory_CategoryIsDeleted()
    {
        CategoryComponent categoryToDelete = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Category1"
        };

        _dbContext.Set<CategoryComponent>().Add(categoryToDelete);
        _dbContext.SaveChanges();

        _categoryRepository.DeleteCategory(categoryToDelete);
        CategoryComponent categoryInDb = _dbContext.Set<CategoryComponent>().Find(categoryToDelete.Id);

        Assert.IsNull(categoryInDb);
    }

    [TestMethod]
    public void DeleteCategory_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<CategoryComponent>()).Throws(new Exception());

        _categoryRepository = new CategoryRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() =>
            _categoryRepository.DeleteCategory(It.IsAny<CategoryComponent>()));
        _mockDbContext.VerifyAll();
    }

    #endregion


    #region Update Category

    [TestMethod]
    public void UpdateCategoryComponent_CategoryCompositeIsUpdated()
    {
        CategoryComponent categoryComponentWithoutUpdate = new CategoryComposite()
        {
            Id = Guid.NewGuid(),
            Name = "Category1",
            SubCategories = new List<CategoryComponent> { }
        };

        _dbContext.Set<CategoryComponent>().Add(categoryComponentWithoutUpdate);
        _dbContext.SaveChanges();

        CategoryComponent categoryComponentWithUpdates = new CategoryComposite()
        {
            Id = categoryComponentWithoutUpdate.Id,
            Name = categoryComponentWithoutUpdate.Name,
            SubCategories = new List<CategoryComponent>
            {
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Category1",
                    CategoryFatherId = categoryComponentWithoutUpdate.Id
                }
            }
        };

        _categoryRepository.UpdateCategory(categoryComponentWithUpdates);
        CategoryComponent categoryInDb = _dbContext.Set<CategoryComponent>().Find(categoryComponentWithUpdates.Id);

        Assert.IsTrue(categoryComponentWithUpdates.Equals(categoryInDb));
    }

    [TestMethod]
    public void UpdateCategoryComponent_CategoryIsUpdated()
    {
        CategoryComponent categoryComponentWithoutUpdate = new Category()
        {
            Id = Guid.NewGuid(),
            Name = "Category1",
        };

        _dbContext.Set<CategoryComponent>().Add(categoryComponentWithoutUpdate);
        _dbContext.SaveChanges();

        CategoryComponent categoryComponentWithUpdates = new Category()
        {
            Id = categoryComponentWithoutUpdate.Id,
            Name = "New name",
        };

        _categoryRepository.UpdateCategory(categoryComponentWithUpdates);
        CategoryComponent categoryInDb = _dbContext.Set<CategoryComponent>().Find(categoryComponentWithUpdates.Id);

        Assert.IsTrue(categoryComponentWithUpdates.Equals(categoryInDb));
    }

    #endregion

    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Database.EnsureDeleted();
    }
}