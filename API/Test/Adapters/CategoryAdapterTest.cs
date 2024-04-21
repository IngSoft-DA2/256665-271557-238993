using Adapter;
using Domain;
using IServiceLogic;
using Moq;
using WebModel.Responses.CategoryResponses;

namespace Test.Adapters;

[TestClass]

public class CategoryAdapterTest
{
    private Mock<ICategoryServiceLogic> _categoryServiceLogic;
    private CategoryAdapter _categoryAdapter;
    
    [TestInitialize]
    public void Initialize()
    {
        _categoryServiceLogic = new Mock<ICategoryServiceLogic>(MockBehavior.Strict);
        _categoryAdapter = new CategoryAdapter(_categoryServiceLogic.Object);
    }
    
    
    [TestMethod]
    public void GetAllCategories_ShouldConvertFromDomainToResponse()
    {
        IEnumerable<Category> expectedServiceResponse = new List<Category>
        {
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Test Category"
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Test Category 2"
            }
            
        };
        
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
    
}