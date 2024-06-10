using BuildingBuddy.API.Controllers;
using Domain;
using IServiceLogic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.SessionRequests;
using WebModel.Responses.LoginResponses;

namespace Test.ApiControllers;

[TestClass]
public class SessionControllerTest
{
    #region Test Initialize

    private Mock<ISessionService> _sessionService;
    private SessionController _sessionController;

    [TestInitialize]
    public void Initialize()
    {
        _sessionService = new Mock<ISessionService>(MockBehavior.Strict);
        _sessionController = new SessionController(_sessionService.Object);
    }

    #endregion

    #region Login

    [TestMethod]
    public void CreateSession()
    {
        
        Session sessionForUser = new Session
        {
            UserId = Guid.NewGuid(),
            SessionString = Guid.NewGuid(),
            UserRole = Domain.Enums.SystemUserRoleEnum.Admin
        };
        
        LoginResponse expectedResponseValue = new LoginResponse
        {
            UserId = sessionForUser.UserId,
            SessionString = sessionForUser.SessionString,
            UserRole = sessionForUser.UserRole
        };
        
        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedResponseValue);
        
        SystemUserLoginRequest userLoginModel = new SystemUserLoginRequest()
        {
            Email = "email",
            Password = "password"
        };
        
        _sessionService.Setup(x => x.Authenticate(userLoginModel.Email, userLoginModel.Password))
            .Returns(sessionForUser);
        
        IActionResult result = _sessionController.Login(userLoginModel);

        OkObjectResult? resultCasted = result as OkObjectResult;
        Assert.IsNotNull(resultCasted);
        
        Assert.AreEqual(expectedControllerResponse.StatusCode, resultCasted.StatusCode);
        Assert.IsTrue(expectedControllerResponse.Value.Equals(resultCasted.Value));
        _sessionService.VerifyAll();
    }

    #endregion

    #region Logout

    [TestMethod]
    public void Logout()
    {
        Guid sessionId = Guid.NewGuid();

        _sessionService.Setup(x => x.Logout(sessionId));

        NoContentResult expected = new NoContentResult();

        IActionResult result = _sessionController.Logout(sessionId);
        _sessionService.VerifyAll();

        NoContentResult? resultCasted = result as NoContentResult;
        Assert.IsNotNull(resultCasted);

        Assert.AreEqual(expected.StatusCode, resultCasted.StatusCode);
    }
    #endregion
}