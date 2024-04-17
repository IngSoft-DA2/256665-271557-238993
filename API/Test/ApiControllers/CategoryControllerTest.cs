using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests;
using WebModel.Responses.CategoryResponses;
using WebModels.Responses;
using Guid = System.Guid;

namespace Test.ApiControllers;

[TestClass]
public class CategoryControllerTest
{

    [TestMethod]
    public void GetAllCategoriesRequest_OkIsReturned()
    {
        IEnumerable<GetCategoryResponse> expectedControllerResponseValue = new List<GetCategoryResponse>
        {
            new GetCategoryResponse()
            {
                Id = Guid.NewGuid(),
                Name = "Plumber"
            }
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedControllerResponseValue);

        Mock<ICategoryAdapter> _categoryAdapter = new Mock<ICategoryAdapter>(MockBehavior.Strict);

        _categoryAdapter.Setup(adapter => adapter.GetAllCategories()).Returns(expectedControllerResponseValue.ToList());
        
        CategoryController _categoryController = new CategoryController(_categoryAdapter.Object);
        
        IActionResult controllerResponse = _categoryController.GetAllCategories();
        _categoryAdapter.VerifyAll();
        
        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetCategoryResponse>? controllerResponseValueCasted = controllerResponseCasted.Value as List<GetCategoryResponse>;
        Assert.IsNotNull(controllerResponseValueCasted);
   Assert.AreEqual(expectedControllerResponse.StatusCode,controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedControllerResponseValue.SequenceEqual(controllerResponseValueCasted));
    }

    [TestMethod]
    public void GetAllCategoriesRequest_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        Mock<ICategoryAdapter> _categoryAdapter = new Mock<ICategoryAdapter>(MockBehavior.Strict);
        _categoryAdapter.Setup(adapter => adapter.GetAllCategories()).Throws(new Exception("Specific Internal Error"));

        CategoryController _categoryController = new CategoryController(_categoryAdapter.Object);

        IActionResult controllerResponse = _categoryController.GetAllCategories();
        _categoryAdapter.VerifyAll();
        
        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode,controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value,controllerResponseCasted.Value);

    }
    
}