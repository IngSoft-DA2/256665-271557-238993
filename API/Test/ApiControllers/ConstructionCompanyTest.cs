using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Responses.ConstructionCompanyResponses;

namespace Test.ApiControllers;

[TestClass]
public class ConstructionCompanyTest
{
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

        Mock<IConstructionCompanyAdapter> constructionCompanyAdapter = new Mock<IConstructionCompanyAdapter>(MockBehavior.Strict);
        constructionCompanyAdapter.Setup(adapter => adapter.GetConstructionCompanies()).Returns(expectedConstructionCompanies);
        
        ConstructionCompanyController constructionCompanyController = new ConstructionCompanyController(constructionCompanyAdapter.Object);
        IActionResult controllerResponse = constructionCompanyController.GetConstructionCompanies();

        constructionCompanyAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<ConstructionCompanyResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<ConstructionCompanyResponse>;
        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }
}