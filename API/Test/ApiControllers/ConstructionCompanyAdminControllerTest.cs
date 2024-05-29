using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.AdministratorRequests;
using WebModel.Requests.ConstructionCompanyAdminRequests;
using WebModel.Responses.AdministratorResponses;
using WebModel.Responses.ConstructionCompanyAdminResponses;

namespace Test.ApiControllers;

[TestClass]
public class ConstructionCompanyAdminControllerTest
{
    #region Initialize

    private Mock<IConstructionCompanyAdminAdapter> _constructionCompanyAdminAdapter;
    private ConstructionCompanyAdminController _constructionCompanyAdminController;

    [TestInitialize]
    public void Initialize()
    {
        _constructionCompanyAdminAdapter = new Mock<IConstructionCompanyAdminAdapter>(MockBehavior.Strict);
        _constructionCompanyAdminController =
            new ConstructionCompanyAdminController(_constructionCompanyAdminAdapter.Object);
    }

    #endregion

    #region Create Construction Company Admin

    [TestMethod]
    public void CreateConstructionCompanyAdmin_CreatedAtActionResultIsReturn()
    {
        CreateConstructionCompanyAdminResponse expectedConstructionCompanyAdminResponse =
            new CreateConstructionCompanyAdminResponse()
            {
                Id = Guid.NewGuid()
            };

        CreatedAtActionResult expectedControllerResponse = new CreatedAtActionResult("CreateConstructionCompanyAdmin",
            "CreateConstructionCompanyAdmin", expectedConstructionCompanyAdminResponse.Id, expectedConstructionCompanyAdminResponse);
        
        _constructionCompanyAdminAdapter.Setup(constructionCompanyAdminAdapter =>
            constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(
                It.IsAny<CreateConstructionCompanyAdminRequest>(), It.IsAny<Guid>())).Returns(expectedConstructionCompanyAdminResponse);

        IActionResult controllerResponse = _constructionCompanyAdminController.CreateConstructionCompanyAdmin(
            It.IsAny<CreateConstructionCompanyAdminRequest>(),It.IsAny<Guid>());
        _constructionCompanyAdminAdapter.VerifyAll();

        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    #endregion
}