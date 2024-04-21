using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.CategoryResponses;

namespace Test.Adapters;

[TestClass]

public class CategoryAdapterTest
{
    #region Initializing Aspects
    
    private Mock<ICategoryServiceLogic> _categoryServiceLogic;
    private CategoryAdapter _categoryAdapter;
    private Category genericCategory1;
    private Category genericCategory2;
    
    [TestInitialize]
    public void Initialize()
    {
        _categoryServiceLogic = new Mock<ICategoryServiceLogic>(MockBehavior.Strict);
        _categoryAdapter = new CategoryAdapter(_categoryServiceLogic.Object);
        genericCategory1 = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Electrician"
        };
        genericCategory2 = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Plumber"
        };
    }
    
    #endregion
    
    #region Get All Categories
    
    [TestMethod]
    public void GetAllCategories_ShouldConvertFromDomainToResponse()
    {
        IEnumerable<Category> expectedServiceResponse = new List<Category> {genericCategory1, genericCategory2};
        
        IEnumerable<GetCategoryResponse> expectedAdapterResponse = new List<GetCategoryResponse>
        {
            new GetCategoryResponse
            {
                Id = expectedServiceResponse.First().Id,
                Name = expectedServiceResponse.First().Name
            },
            new GetCategoryResponse
            {
                Id = expectedServiceResponse.Last().Id,
                Name = expectedServiceResponse.Last().Name
            }
        };
        
        _categoryServiceLogic.Setup(service => service.GetAllCategories()).Returns(expectedServiceResponse);

        IEnumerable<GetCategoryResponse> adapterResponse = _categoryAdapter.GetAllCategories();
        
        _categoryServiceLogic.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.Count().Equals(expectedServiceResponse.Count()));
        Assert.IsTrue(adapterResponse.SequenceEqual(expectedAdapterResponse));
    }
    
    [TestMethod]
    public void GetAllCategories_ShouldThrowException()
    {
        _categoryServiceLogic.Setup(service => service.GetAllCategories()).Throws(new Exception("Something went wrong"));
        
        Assert.ThrowsException<Exception>(() => _categoryAdapter.GetAllCategories());
    }
    
    #endregion
    
    #region Get Category By Id
    
    [TestMethod]
    public void GetCategoryById_ShouldConvertFromDomainToResponse()
    {
        Category expectedServiceResponse = genericCategory1;
        
        GetCategoryResponse expectedAdapterResponse = new GetCategoryResponse
        {
            Id = expectedServiceResponse.Id,
            Name = expectedServiceResponse.Name
        };
        
        _categoryServiceLogic.Setup(service => service.GetCategoryById(It.IsAny<Guid>())).Returns(expectedServiceResponse);

        GetCategoryResponse adapterResponse = _categoryAdapter.GetCategoryById(It.IsAny<Guid>());
        
        _categoryServiceLogic.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.Equals(adapterResponse));
    }
    
    [TestMethod]
    public void GetCategoryById_ShouldThrowObjectNotFoundException()
    {
        _categoryServiceLogic.Setup(service => service.GetCategoryById(It.IsAny<Guid>())).Throws(new ObjectNotFoundServiceException());
        
        Assert.ThrowsException<ObjectNotFoundAdapterException>(() => _categoryAdapter.GetCategoryById(It.IsAny<Guid>()));
    }
    
    [TestMethod]
    public void GetCategoryById_ShouldThrowException()
    {
        _categoryServiceLogic.Setup(service => service.GetCategoryById(It.IsAny<Guid>())).Throws(new Exception("Something went wrong"));
        
        Assert.ThrowsException<Exception>(() => _categoryAdapter.GetCategoryById(It.IsAny<Guid>()));
    }
    
    #endregion
    
}