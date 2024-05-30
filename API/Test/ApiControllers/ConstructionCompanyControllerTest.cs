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

    #region Get Construction Companies
    
    [TestMethod]
    public void GetConstructionCompanies_OkIsReturned()
    {
        IEnumerable<GetConstructionCompanyResponse> expectedConstructionCompanies = new List<GetConstructionCompanyResponse>()
        {
            new GetConstructionCompanyResponse()
            {
                Id = Guid.NewGuid(),
                Name = "Construction Company 1",
            }
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedConstructionCompanies);
        
        _constructionCompanyAdapter.Setup(adapter => adapter.GetAllConstructionCompanies()).Returns(expectedConstructionCompanies);
        
        IActionResult controllerResponse = _constructionCompanyController.GetAllConstructionCompanies();

        _constructionCompanyAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetConstructionCompanyResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetConstructionCompanyResponse>;
        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(expectedConstructionCompanies));
    }
   
    #endregion

    #region Create Construction Company
    
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
        Assert.IsTrue(expectedConstructionCompany.Equals(controllerResponseValueCasted));
    }
    
    #endregion
}