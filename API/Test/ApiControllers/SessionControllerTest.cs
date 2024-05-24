using BuildingBuddy.API.Controllers;
using IServiceLogic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.SessionRequests;

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
        Guid sessionString = Guid.NewGuid();
        SystemUserLoginRequest userLoginModel = new SystemUserLoginRequest()
        {
            Email = "email",
            Password = "password"
        };

        _sessionService.Setup(x => x.Authenticate(userLoginModel.Email, userLoginModel.Password))
            .Returns(sessionString);

        OkObjectResult expected = new OkObjectResult(sessionString);

        IActionResult result = _sessionController.Login(userLoginModel);
        _sessionService.VerifyAll();

        OkObjectResult? resultCasted = result as OkObjectResult;
        Assert.IsNotNull(resultCasted);

        Assert.AreEqual(expected.StatusCode, resultCasted.StatusCode);
        Assert.AreEqual(expected.Value, resultCasted.Value);
    }

    #endregion
}