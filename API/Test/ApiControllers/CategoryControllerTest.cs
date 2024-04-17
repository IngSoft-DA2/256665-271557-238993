using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests;
using WebModel.Requests.CategoryRequests;
using WebModel.Responses.CategoryResponses;
using WebModels.Responses;
using Guid = System.Guid;

namespace Test.ApiControllers;

[TestClass]
public class CategoryControllerTest
{
    #region Initialize

    private Mock<ICategoryAdapter> _categoryAdapter;
    private CategoryController _categoryController;

    [TestInitialize]
    public void Initialize()
    {
        _categoryAdapter = new Mock<ICategoryAdapter>(MockBehavior.Strict);
        _categoryController = new CategoryController(_categoryAdapter.Object);
    }

    #endregion

    #region Get All Categories

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

        _categoryAdapter.Setup(adapter => adapter.GetAllCategories()).Returns(expectedControllerResponseValue.ToList());

        IActionResult controllerResponse = _categoryController.GetAllCategories();
        _categoryAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetCategoryResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetCategoryResponse>;

        Assert.IsNotNull(controllerResponseValueCasted);
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedControllerResponseValue.SequenceEqual(controllerResponseValueCasted));
    }

    [TestMethod]
    public void GetAllCategoriesRequest_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        _categoryAdapter.Setup(adapter => adapter.GetAllCategories()).Throws(new Exception("Specific Internal Error"));

        IActionResult controllerResponse = _categoryController.GetAllCategories();
        _categoryAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    #endregion


    [TestMethod]
    public void CreateCategoryRequest_OkIsReturned()
    {
        CreateCategoryResponse expectedControllerValue = new CreateCategoryResponse
        {
            Id = Guid.NewGuid(),
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedControllerValue);

        _categoryAdapter.Setup(adapter => adapter.CreateCategory(It.IsAny<CreateCategoryRequest>()))
            .Returns(expectedControllerValue);

        IActionResult controllerResponse = _categoryController.CreateCategory(It.IsAny<CreateCategoryRequest>());

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        CreateCategoryResponse? controllerValueResponse = controllerResponseCasted.Value as CreateCategoryResponse;
        Assert.IsNotNull(controllerValueResponse);
        
        Assert.AreEqual(expectedControllerResponse.StatusCode,controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerValue.Id, controllerValueResponse.Id);
    }
}