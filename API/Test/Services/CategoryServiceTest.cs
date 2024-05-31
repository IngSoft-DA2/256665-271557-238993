using Domain;
using IDataAccess;
using IRepository;
using Moq;
using Repositories.CustomExceptions;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class CategoryServiceTest
{
    #region Initialize

    private Mock<ICategoryRepository> _categoryRepository;
    private CategoryService _categoryService;

    [TestInitialize]
    public void Initialize()
    {
        _categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);
        _categoryService = new CategoryService(_categoryRepository.Object);
    }

    #endregion

    #region Get all categories

    [TestMethod]
    //Happy path
    public void GetAllCategories_CategoriesAreReturned()
    {
        IEnumerable<CategoryComponent> categoriesInDb = new List<CategoryComponent>
        {
            new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Category1",
                CategoryFatherId = null
            },
            new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Category2",
                CategoryFatherId = null
            }
        };


        _categoryRepository.Setup(categoryRepository => categoryRepository.GetAllCategories()).Returns(categoriesInDb);

        IEnumerable<CategoryComponent> categories = _categoryService.GetAllCategories();

        Assert.AreEqual(categoriesInDb.Count(), categories.Count());
        Assert.IsTrue(categories.SequenceEqual(categoriesInDb));
    }

    #region Get all categories, repository validations

    [TestMethod]
    public void GetAllCategories_UnknownServiceExceptionIsThrown()
    {
        _categoryRepository.Setup(categoryRepository => categoryRepository.GetAllCategories())
            .Throws(new UnknownRepositoryException("Unknown Error"));
        Assert.ThrowsException<UnknownServiceException>(() => _categoryService.GetAllCategories());
    }

    #endregion

    #endregion

    #region Get Category By Id

    [TestMethod]
    //Happy path
    public void GetCategoryById_ReturnsCategory()
    {
        Category categoryInDb = new Category()
        {
            Id = Guid.NewGuid(),
            Name = "Category1"
        };

        _categoryRepository.Setup(categoryRepository => categoryRepository.GetCategoryById(It.IsAny<Guid>()))
            .Returns(categoryInDb);

        CategoryComponent categoryFound = _categoryService.GetCategoryById(It.IsAny<Guid>());

        Assert.AreEqual(categoryFound, categoryInDb);
    }

    #region Get Category By Id, repository validations

    [TestMethod]
    public void GetCategoryById_ObjectNotFoundServiceExceptionIsThrown()
    {
        _categoryRepository.Setup(categoryRepository => categoryRepository.GetCategoryById(It.IsAny<Guid>()))
            .Returns(() => null);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() =>
            _categoryService.GetCategoryById(It.IsAny<Guid>()));
    }

    [TestMethod]
    public void GetCategoryById_UnknownExceptionIsThrown()
    {
        _categoryRepository.Setup(categoryRepository => categoryRepository.GetCategoryById(It.IsAny<Guid>()))
            .Throws(new UnknownRepositoryException("Unknown Error"));

        Assert.ThrowsException<UnknownServiceException>(() => _categoryService.GetCategoryById(It.IsAny<Guid>()));
    }

    #endregion

    #endregion

    #region Create Category

    [TestMethod]
    public void CreateCategory_CategoryIsCreated()
    {
        CategoryComponent categoryToCreateWithValidData = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Category1"
        };

        _categoryRepository.Setup(categoryRepository => categoryRepository.CreateCategory(It.IsAny<CategoryComponent>()));
        _categoryService.CreateCategory(categoryToCreateWithValidData);
        _categoryRepository.VerifyAll();
    }
    
    [TestMethod]
    public void CreateSubCategory_CategoryIsCreated()
    {
        CategoryComponent categoryToBeFather = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Category1"
        };
        
        CategoryComponent categoryToCreateWithValidData = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Category1",
            CategoryFatherId = Guid.NewGuid()
        };
        
        _categoryRepository.Setup(categoryRepository => categoryRepository.GetCategoryById(It.IsAny<Guid>())).Returns(categoryToBeFather);
        _categoryRepository.Setup(categoryRepository => categoryRepository.DeleteCategory(It.IsAny<CategoryComponent>()));
        _categoryRepository.Setup(categoryRepository => categoryRepository.CreateCategory(It.IsAny<CategoryComponent>()));
        _categoryRepository.Setup(categoryRepository => categoryRepository.UpdateCategory(It.IsAny<CategoryComponent>()));
        
        _categoryService.CreateCategory(categoryToCreateWithValidData);
        _categoryRepository.VerifyAll();
    }

    #region Create Category, Domain Validations 

    [TestMethod]
    public void CreateCategoryWithEmptyName_ThrowsObjectErrorException()
    {
        CategoryComponent categoryExample = new Category()
        {
            Id = Guid.NewGuid(),
            Name = ""
        };

        Assert.ThrowsException<ObjectErrorServiceException>(() => _categoryService.CreateCategory(categoryExample));
        _categoryRepository.VerifyAll();
    }

    #endregion

    #region Create Category, Repository Validations

    [TestMethod]
    public void CreateCategory_ThrowsUnknownServiceException()
    {
        CategoryComponent categoryToAdd = new Category
        {
            Id = Guid.NewGuid(),
            Name = "category1"
        };

        _categoryRepository.Setup(categoryRepository => categoryRepository.CreateCategory(It.IsAny<Category>()))
            .Throws(new UnknownRepositoryException("Unknown Error"));

        Assert.ThrowsException<UnknownServiceException>(() => _categoryService.CreateCategory(categoryToAdd));
        _categoryRepository.VerifyAll();
    }

    #endregion

    #endregion
}