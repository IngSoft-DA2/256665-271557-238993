using System.Security.Authentication;
using DataAccess.Repositories;
using Domain;
using IDataAccess;
using IRepository;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class SessionServiceTest
{
    #region Test Initialization

    private Mock<ISessionRepository> _sessionRepository;
    private Mock<IAdministratorRepository> _administratorRepository;
    private Mock<IManagerRepository> _managerRepository;
    private Mock<IRequestHandlerRepository> _requestHandlerRepository;
    private SessionService _sessionService;
    private Guid _sampleUserGuid;

    [TestInitialize]
    public void Initialize()
    {
        _administratorRepository = new Mock<IAdministratorRepository>(MockBehavior.Strict);
        _managerRepository = new Mock<IManagerRepository>(MockBehavior.Strict);
        _requestHandlerRepository = new Mock<IRequestHandlerRepository>(MockBehavior.Strict);
        _sessionRepository = new Mock<ISessionRepository>(MockBehavior.Strict);

        _sessionService = new SessionService(_sessionRepository.Object, _managerRepository.Object,
            _administratorRepository.Object, _requestHandlerRepository.Object);
    }

    #endregion

    #region IsValidSessionString

    [TestMethod]
    public void IsValidSessionString_SessionStringIsReturn()
    {
        Guid sessionString = Guid.NewGuid();
        Session dummySession = new Session();

        _sessionRepository.Setup(sessionRepository => sessionRepository.GetSessionBySessionString(It.IsAny<Guid>()))
            .Returns(dummySession);

        bool result = _sessionService.IsSessionValid(sessionString);
        _sessionRepository.VerifyAll();
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValidSessionString_InvalidSessionString_ReturnsFalse()
    {
        Guid sessionString = Guid.NewGuid();
        Session dummySession = null;

        _sessionRepository.Setup(sessionRepository => sessionRepository.GetSessionBySessionString(It.IsAny<Guid>()))
            .Returns(dummySession);

        bool result = _sessionService.IsSessionValid(sessionString);

        _sessionRepository.VerifyAll();
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValidSessionString_UnknownExceptionIsThrown()
    {
        Guid sessionString = Guid.NewGuid();
        Session dummySession = null;

        _sessionRepository.Setup(sessionRepository => sessionRepository.GetSessionBySessionString(It.IsAny<Guid>()))
            .Throws<Exception>();

        Assert.ThrowsException<UnknownServiceException>(() => _sessionService.IsSessionValid(sessionString));
        _sessionRepository.VerifyAll();
    }

    #endregion

    #region GetUserRoleBySessionString

    [TestMethod]
    public void GetUserRoleBySessionString_RolesAreReturn()
    {
        Session dummySession = new Session();
        dummySession.UserRole = "Administrator";

        _sessionRepository.Setup(_sessionRepository => _sessionRepository.GetSessionBySessionString(It.IsAny<Guid>()))
            .Returns(dummySession);

        string role = _sessionService.GetUserRoleBySessionString(It.IsAny<Guid>());
        _sessionRepository.VerifyAll();

        Assert.AreEqual(dummySession.UserRole, role);
    }

    [TestMethod]
    public void GetUserRoleBySessionString_UnknownExceptionIsThrown()
    {
        Session dummySession = new Session();
        dummySession.UserRole = "Administrator";

        _sessionRepository.Setup(_sessionRepository => _sessionRepository.GetSessionBySessionString(It.IsAny<Guid>()))
            .Throws<Exception>();

        Assert.ThrowsException<UnknownServiceException>(() =>
            _sessionService.GetUserRoleBySessionString(It.IsAny<Guid>()));
        _sessionRepository.VerifyAll();
    }

    #endregion

    [TestMethod]
    public void Authenticate_UserIsAuthenticated()
    {
        Administrator user = new Administrator()
        {
            Id = Guid.NewGuid(),
            Email = "example@gmail.com",
            Password = "Password2003",
            Role = "Administrator"
        };

        Session dummySession = new Session();
        dummySession.UserId = _sampleUserGuid;

        _administratorRepository.Setup(administratorRepository => administratorRepository.GetAllAdministrators())
            .Returns(new List<Administrator> { user });

        _managerRepository.Setup(managerRepository => managerRepository.GetAllManagers())
            .Returns(new List<Manager>());

        _requestHandlerRepository.Setup(requestHandlerRepository => requestHandlerRepository.GetAllRequestHandlers())
            .Returns(new List<RequestHandler>());

        _sessionRepository.Setup(sessionRepository => sessionRepository.CreateSession(It.IsAny<Session>()));


        Guid sessionStringObtained = _sessionService.Authenticate(user.Email, user.Password);
        _administratorRepository.VerifyAll();
        _managerRepository.VerifyAll();
        _requestHandlerRepository.VerifyAll();
        _sessionRepository.VerifyAll();

        Assert.IsNotNull(sessionStringObtained);
    }

    [TestMethod]
    public void Authenticate_UserIsNotAuthenticated()
    {
        Session dummySession = null;

        _administratorRepository.Setup(administratorRepository => administratorRepository.GetAllAdministrators())
            .Returns(new List<Administrator>());

        _managerRepository.Setup(managerRepository => managerRepository.GetAllManagers())
            .Returns(new List<Manager>());

        _requestHandlerRepository.Setup(requestHandlerRepository => requestHandlerRepository.GetAllRequestHandlers())
            .Returns(new List<RequestHandler>());

        _sessionRepository.Setup(sessionRepository => sessionRepository.GetSessionBySessionString(It.IsAny<Guid>()))
            .Returns(dummySession);

        Assert.ThrowsException<InvalidCredentialException>(() =>
            _sessionService.Authenticate(It.IsAny<string>(), It.IsAny<string>()));
    }

    [TestMethod]
    public void Authenticate_UnknownExceptionIsThrown()
    {
        Session dummySession = null;

        _administratorRepository.Setup(administratorRepository => administratorRepository.GetAllAdministrators())
            .Throws(new Exception());

        _managerRepository.Setup(managerRepository => managerRepository.GetAllManagers())
            .Returns(new List<Manager>());

        _requestHandlerRepository.Setup(requestHandlerRepository => requestHandlerRepository.GetAllRequestHandlers())
            .Returns(new List<RequestHandler>());

        _sessionRepository.Setup(sessionRepository => sessionRepository.GetSessionBySessionString(It.IsAny<Guid>()))
            .Returns(dummySession);

        Assert.ThrowsException<UnknownServiceException>(() =>
            _sessionService.Authenticate(It.IsAny<string>(), It.IsAny<string>()));
    }
}