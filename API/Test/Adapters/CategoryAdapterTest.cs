using Adapter;
using Domain;
using IServiceLogic;
using Moq;
using WebModel.Responses.CategoryResponses;

namespace Test.Adapters;

[TestClass]

public class CategoryAdapterTest
{
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
        
        Mock<ICategoryServiceLogic> categoryServiceLogic = new Mock<ICategoryServiceLogic>();
        categoryServiceLogic.Setup(service => service.GetAllCategories()).Returns(expectedServiceResponse);
        
        CategoryAdapter categoryAdapter = new CategoryAdapter(categoryServiceLogic.Object);

        IEnumerable<GetCategoryResponse> adapterResponse = categoryAdapter.GetAllCategories();
        
        categoryServiceLogic.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.Count().Equals(expectedServiceResponse.Count()));
        Assert.IsTrue(adapterResponse.SequenceEqual(expectedAdapterResponse));
    }
    
    [TestMethod]
    public void GetAllCategories_ShouldThrowException()
    {
        Mock<ICategoryServiceLogic> categoryServiceLogic = new Mock<ICategoryServiceLogic>();
        categoryServiceLogic.Setup(service => service.GetAllCategories()).Throws(new Exception("Something went wrong"));
        
        CategoryAdapter categoryAdapter = new CategoryAdapter(categoryServiceLogic.Object);

        Assert.ThrowsException<Exception>(() => categoryAdapter.GetAllCategories());
    }
    
}