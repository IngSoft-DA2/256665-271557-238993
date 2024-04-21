using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.ConstructionCompanyRequests;
using WebModel.Responses.ConstructionCompanyResponses;

namespace Test.ApiControllers;

[TestClass]
public class ConstructionCompanyControllerTest
{
    #region Initilizing aspects
    
    private Mock<IConstructionCompanyAdapter> _constructionCompanyAdapter;
    private ConstructionCompanyController _constructionCompanyController;

    [TestInitialize]
    public void Initialize()
    {
        _constructionCompanyAdapter = new Mock<IConstructionCompanyAdapter>(MockBehavior.Strict);
        _constructionCompanyController = new ConstructionCompanyController(_constructionCompanyAdapter.Object);
    }
    
    #endregion

    #region GetConstructionCompanies
    
    [TestMethod]
    public void GetConstructionCompanies_OkIsReturned()
    {
        IEnumerable<GetConstructionCompanyResponse> expectedConstructionCompanies = new List<GetConstructionCompanyResponse>()
        {
            new GetConstructionCompanyResponse()
            {
                Id = Guid.NewGuid(),
                Name = "Construction Company 1"
            }
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedConstructionCompanies);
        
        _constructionCompanyAdapter.Setup(adapter => adapter.GetConstructionCompanies()).Returns(expectedConstructionCompanies);
        
        IActionResult controllerResponse = _constructionCompanyController.GetConstructionCompanies();

        _constructionCompanyAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetConstructionCompanyResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetConstructionCompanyResponse>;
        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(expectedConstructionCompanies));
    }
    
    [TestMethod]
    public void GetAllConstructionCompanies_InternalServerErrorIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;
        
        _constructionCompanyAdapter.Setup(adapter => adapter.GetConstructionCompanies()).Throws(new Exception("Something went wrong"));

        IActionResult controllerResponse = _constructionCompanyController.GetConstructionCompanies();

        _constructionCompanyAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
    
    #endregion

    #region CreaterConstructionCompany
    
    [TestMethod]
    public void CreateConstructionCompany_CreatedAtActionIsReturned()
    {
        CreateConstructionCompanyResponse expectedConstructionCompany = new CreateConstructionCompanyResponse()
        {
            Id = Guid.NewGuid()
        };

        CreatedAtActionResult expectedControllerResponse =
            new CreatedAtActionResult("CreateConstructionCompany", "CreateConstructionCompany"
                , expectedConstructionCompany.Id, expectedConstructionCompany);
        
        _constructionCompanyAdapter.Setup(adapter => adapter.CreateConstructionCompany(It.IsAny<CreateConstructionCompanyRequest>())).Returns(expectedConstructionCompany);
        
        IActionResult controllerResponse = _constructionCompanyController.CreateConstructionCompany(It.IsAny<CreateConstructionCompanyRequest>());

        _constructionCompanyAdapter.VerifyAll();

        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        
        Assert.IsNotNull(controllerResponseCasted);

        CreateConstructionCompanyResponse? controllerResponseValueCasted =
            controllerResponseCasted.Value as CreateConstructionCompanyResponse;
        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedConstructionCompany.Id, controllerResponseValueCasted.Id);
    }
    
    [TestMethod]
    public void CreateConstructionCompany_BadRequestIsReturned()
    {
        BadRequestObjectResult expectedControllerResponse = new BadRequestObjectResult("Something went wrong");
        
        _constructionCompanyAdapter.Setup(adapter => adapter.CreateConstructionCompany(It.IsAny<CreateConstructionCompanyRequest>())).Throws(new ObjectErrorAdapterException("Something went wrong"));

        IActionResult controllerResponse = _constructionCompanyController.CreateConstructionCompany(It.IsAny<CreateConstructionCompanyRequest>());

        _constructionCompanyAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;    
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
    
    [TestMethod]
    public void CreateConstructionCompany_InternalServerErrorIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;
        
        _constructionCompanyAdapter.Setup(adapter => adapter.CreateConstructionCompany(It.IsAny<CreateConstructionCompanyRequest>())).Throws(new Exception("Something went wrong"));

        IActionResult controllerResponse = _constructionCompanyController.CreateConstructionCompany(It.IsAny<CreateConstructionCompanyRequest>());

        _constructionCompanyAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
    
    #endregion
}