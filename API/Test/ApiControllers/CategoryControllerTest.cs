using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests;
using WebModel.Requests.CategoryRequests;
using WebModel.Responses.CategoryResponses;
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

    #region Get Category By Id

    [TestMethod]
    public void GetCategoryByIdRequest_OkIsReturned()
    {
        GetCategoryResponse categoryFoundInDb = new GetCategoryResponse
        {
            Id = Guid.NewGuid(),
            Name = "Plumber"
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(categoryFoundInDb.Id);

        _categoryAdapter.Setup(adapter => adapter.GetCategoryById(categoryFoundInDb.Id))
            .Returns(categoryFoundInDb);

        IActionResult controllerResponse = _categoryController.GetCategoryById(categoryFoundInDb.Id);
        _categoryAdapter.VerifyAll();
        
        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        GetCategoryResponse? controllerValue = controllerResponseCasted.Value as GetCategoryResponse;
        Assert.IsNotNull(controllerValue);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(categoryFoundInDb.Equals(controllerValue));
    }

    [TestMethod]
    public void GetCategoryByIdRequest_NotFoundIsReturned()
    {
        NotFoundObjectResult expectedControllerResponse = new NotFoundObjectResult("Category was not found in database");

        _categoryAdapter.Setup(adapter => adapter.GetCategoryById(It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundAdapterException());

        IActionResult controllerResponse = _categoryController.GetCategoryById(It.IsAny<Guid>());
        _categoryAdapter.VerifyAll();
        
        NotFoundObjectResult? controllerResponseCasted = controllerResponse as NotFoundObjectResult;
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(expectedControllerResponse.StatusCode,controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value,controllerResponseCasted.Value);
    }

    [TestMethod]
    public void GetCategoryByIdRequest_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        _categoryAdapter.Setup(adapter => adapter.GetCategoryById(It.IsAny<Guid>()))
            .Throws(new Exception("Some specific error"));

        IActionResult controllerResponse = _categoryController.GetCategoryById(It.IsAny<Guid>());
        _categoryAdapter.VerifyAll();
        
        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(expectedControllerResponse.StatusCode,controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value,controllerResponseCasted.Value);
    }

    #endregion

    #region Create Category

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

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerValue.Id, controllerValueResponse.Id);
    }

    [TestMethod]
    public void CreateCategoryRequest_BadRequestIsReturned()
    {
        BadRequestObjectResult expectedControllerResponse = new BadRequestObjectResult("An specific Error");

        _categoryAdapter.Setup(adapter => adapter.CreateCategory(It.IsAny<CreateCategoryRequest>()))
            .Throws(new ObjectErrorAdapterException("An specific Error"));

        IActionResult controllerResponse = _categoryController.CreateCategory(It.IsAny<CreateCategoryRequest>());
        _categoryAdapter.VerifyAll();

        BadRequestObjectResult? controllerResponseCasted = controllerResponse as BadRequestObjectResult;

        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    [TestMethod]
    public void CreateCategoryRequest_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        _categoryAdapter.Setup(adapter => adapter.CreateCategory(It.IsAny<CreateCategoryRequest>()))
            .Throws(new Exception("Exception not recognized"));

        IActionResult controllerResponse = _categoryController.CreateCategory(It.IsAny<CreateCategoryRequest>());
        _categoryAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    #endregion
}