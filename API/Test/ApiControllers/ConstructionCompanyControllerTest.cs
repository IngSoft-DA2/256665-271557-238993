using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Responses.ConstructionCompanyResponses;

namespace Test.ApiControllers;

[TestClass]
public class ConstructionCompanyControllerTest
{
    private Mock<IConstructionCompanyAdapter> _constructionCompanyAdapter;
    private ConstructionCompanyController _constructionCompanyController;

    [TestInitialize]
    public void Initialize()
    {
        _constructionCompanyAdapter = new Mock<IConstructionCompanyAdapter>(MockBehavior.Strict);
        _constructionCompanyController = new ConstructionCompanyController(_constructionCompanyAdapter.Object);
    }
    
    [TestMethod]
    public void GetAllConstructionCompanies_OkIsReturned()
    {
        IEnumerable<ConstructionCompanyResponse> expectedConstructionCompanies = new List<ConstructionCompanyResponse>()
        {
            new ConstructionCompanyResponse()
            {
                Name = "Construction Company 1"
            }
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedConstructionCompanies);
        
        _constructionCompanyAdapter.Setup(adapter => adapter.GetConstructionCompanies()).Returns(expectedConstructionCompanies);
        
        IActionResult controllerResponse = _constructionCompanyController.GetConstructionCompanies();

        _constructionCompanyAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<ConstructionCompanyResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<ConstructionCompanyResponse>;
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
}