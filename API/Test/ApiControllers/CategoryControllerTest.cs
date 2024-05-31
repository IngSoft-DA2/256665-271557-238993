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
                Name = "Plumber",
                SubCategories = null
            },
            new GetCategoryResponse()
            {
                Id = Guid.NewGuid(),
                Name = "Electrician",
                SubCategories = new List<GetCategoryResponse>
                {
                    new GetCategoryResponse
                    {
                        Id = Guid.NewGuid(),
                        Name = "Electrician 1",
                        SubCategories = null
                    }
                }
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
    
    #endregion

    #region Create Category

    [TestMethod]
    public void CreateCategoryRequest_OkIsReturned()
    {
        CreateCategoryResponse expectedResponse = new CreateCategoryResponse
        {
            Id = Guid.NewGuid(),
        };
        
        CreatedAtActionResult expectedControllerResponse =
            new CreatedAtActionResult("CreateConstructionCompanyAdmin", "CreateConstructionCompanyAdmin"
                , expectedResponse.Id, expectedResponse);
        
        _categoryAdapter.Setup(adapter => adapter.CreateCategory(It.IsAny<CreateCategoryRequest>()))
            .Returns(expectedResponse);

        IActionResult controllerResponse = _categoryController.CreateCategory(It.IsAny<CreateCategoryRequest>());

        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        Assert.IsNotNull(controllerResponseCasted);

        CreateCategoryResponse? controllerValueResponse = controllerResponseCasted.Value as CreateCategoryResponse;
        Assert.IsNotNull(controllerValueResponse);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerValueResponse);
    }
    
    #endregion
}