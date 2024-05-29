using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using Domain.Enums;
using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.ConstructionCompanyAdminRequests;
using WebModel.Responses.ConstructionCompanyAdminResponses;

namespace Test.ApiControllers;

[TestClass]
public class ConstructionCompanyAdminControllerTest
{
    #region Initialize

    private Mock<IConstructionCompanyAdminAdapter> _constructionCompanyAdminAdapter;
    private ConstructionCompanyAdminController _constructionCompanyAdminController;
    private ControllerContext _controllerContext;

    [TestInitialize]
    public void Initialize()
    {
        _constructionCompanyAdminAdapter = new Mock<IConstructionCompanyAdminAdapter>(MockBehavior.Strict);
        _constructionCompanyAdminController =
            new ConstructionCompanyAdminController(_constructionCompanyAdminAdapter.Object);
        
         _controllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
        _constructionCompanyAdminController.ControllerContext = _controllerContext;
    }

    #endregion

    #region Create Construction Company Admin By Invitation

    [TestMethod]
    public void CreateConstructionCompanyAdmin_CreatedAtActionResultIsReturn()
    {
        CreateConstructionCompanyAdminResponse expectedConstructionCompanyAdminResponse = new CreateConstructionCompanyAdminResponse()
            {
                Id = Guid.NewGuid()
            };
        
        CreatedAtActionResult expectedControllerResponse = new CreatedAtActionResult("CreateConstructionCompanyAdmin",
            "CreateConstructionCompanyAdmin", expectedConstructionCompanyAdminResponse.Id,
            expectedConstructionCompanyAdminResponse);

        _constructionCompanyAdminAdapter.Setup(constructionCompanyAdminAdapter => constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(
                    It.IsAny<CreateConstructionCompanyAdminRequest>(), null))
            .Returns(expectedConstructionCompanyAdminResponse);


        CreateConstructionCompanyAdminRequest request = new CreateConstructionCompanyAdminRequest
        {
            Firstname = "Firstname",
            Lastname = "Lastname",
            Email = "email@gmail.com",
            Password = "Password",
            InvitationId = Guid.NewGuid()
        };
        
        IActionResult controllerResponse = _constructionCompanyAdminController
            .CreateConstructionCompanyAdmin(request);
        
        _constructionCompanyAdminAdapter.VerifyAll();

        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    #endregion

    #region Create Construction Company Admin By Another Construction Company Admin

    [TestMethod]
    public void CreateConstructionCompanyAdminByAnotherAdmin_ConstructionCompanyAdminIsCreated()
    {
        // Simulating that the user is a ConstructionCompanyAdmin
        _controllerContext.HttpContext.Items["UserRole"] = SystemUserRoleEnum.ConstructionCompanyAdmin.ToString();
        
        CreateConstructionCompanyAdminResponse expectedConstructionCompanyAdminResponse =
            new CreateConstructionCompanyAdminResponse()
            {
                Id = Guid.NewGuid()
            };

        CreatedAtActionResult expectedControllerResponse = new CreatedAtActionResult("CreateConstructionCompanyAdmin",
            "CreateConstructionCompanyAdmin", expectedConstructionCompanyAdminResponse.Id,
            expectedConstructionCompanyAdminResponse);

        _constructionCompanyAdminAdapter.Setup(constructionCompanyAdminAdapter =>
                constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(
                    It.IsAny<CreateConstructionCompanyAdminRequest>(), It.IsAny<SystemUserRoleEnum>()))
            .Returns(expectedConstructionCompanyAdminResponse);

        IActionResult controllerResponse =
            _constructionCompanyAdminController.CreateConstructionCompanyAdmin(It.IsAny<CreateConstructionCompanyAdminRequest>());

        _constructionCompanyAdminAdapter.VerifyAll();

        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
    
    [TestMethod]
    public void CreateConstructionCompanyAdminWithNotAllowedRole_ObjectResult403IsReturn()
    {
        // Simulating that the user is a ConstructionCompanyAdmin
        _controllerContext.HttpContext.Items["UserRole"] = SystemUserRoleEnum.Manager.ToString();
        
        ObjectResult expectedControllerResponse = new ObjectResult("Only ConstructionCompanyAdmin people is allowed.");
        expectedControllerResponse.StatusCode = StatusCodes.Status403Forbidden;
        
        IActionResult controllerResponse = _constructionCompanyAdminController.CreateConstructionCompanyAdmin(It.IsAny<CreateConstructionCompanyAdminRequest>());

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
    
    #endregion
}