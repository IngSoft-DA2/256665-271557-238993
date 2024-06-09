using System.Security.Authentication;
using Domain;
using Domain.Enums;
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
    private Mock<IConstructionCompanyAdminRepository> _constructionCompanyAdminRepository;
    private SessionService _sessionService;
    private Guid _sampleUserGuid;

    [TestInitialize]
    public void Initialize()
    {
        _administratorRepository = new Mock<IAdministratorRepository>(MockBehavior.Strict);
        _managerRepository = new Mock<IManagerRepository>(MockBehavior.Strict);
        _requestHandlerRepository = new Mock<IRequestHandlerRepository>(MockBehavior.Strict);
        _sessionRepository = new Mock<ISessionRepository>(MockBehavior.Strict);
        _constructionCompanyAdminRepository = new Mock<IConstructionCompanyAdminRepository>(MockBehavior.Strict);

        _sessionService = new SessionService(_sessionRepository.Object, _managerRepository.Object,
            _administratorRepository.Object, _requestHandlerRepository.Object,
            _constructionCompanyAdminRepository.Object);
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
        dummySession.UserRole = SystemUserRoleEnum.Admin;

        _sessionRepository.Setup(_sessionRepository => _sessionRepository.GetSessionBySessionString(It.IsAny<Guid>()))
            .Returns(dummySession);

        SystemUserRoleEnum role = _sessionService.GetUserRoleBySessionString(It.IsAny<Guid>());
        _sessionRepository.VerifyAll();

        Assert.AreEqual(dummySession.UserRole, role);
    }

    [TestMethod]
    public void GetUserRoleBySessionString_SessionStringIsNotFound()
    {
        Session dummySession = null;

        _sessionRepository.Setup(_sessionRepository => _sessionRepository.GetSessionBySessionString(It.IsAny<Guid>()))
            .Returns(dummySession);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() =>
            _sessionService.GetUserRoleBySessionString(It.IsAny<Guid>()));
        _sessionRepository.VerifyAll();
    }

    [TestMethod]
    public void GetUserRoleBySessionString_UnknownExceptionIsThrown()
    {
        Session dummySession = new Session();
        dummySession.UserRole = SystemUserRoleEnum.Admin;

        _sessionRepository.Setup(_sessionRepository => _sessionRepository.GetSessionBySessionString(It.IsAny<Guid>()))
            .Throws<Exception>();

        Assert.ThrowsException<UnknownServiceException>(() =>
            _sessionService.GetUserRoleBySessionString(It.IsAny<Guid>()));
        _sessionRepository.VerifyAll();
    }

    #endregion

    #region Authenticate user

    [TestMethod]
    public void Authenticate_UserIsAuthenticated()
    {
        Administrator user = new Administrator()
        {
            Id = Guid.NewGuid(),
            Email = "example@gmail.com",
            Password = "Password2003",
            Role = SystemUserRoleEnum.Admin
        };

        Manager manager = new Manager();
        RequestHandler requestHandler = new RequestHandler();
        ConstructionCompanyAdmin constructionCompanyAdmin = new ConstructionCompanyAdmin();
        Session dummySession = new Session();
        
        dummySession.UserId = _sampleUserGuid;

        _administratorRepository.Setup(administratorRepository => administratorRepository.GetAllAdministrators())
            .Returns(new List<Administrator> { user });

        _managerRepository.Setup(managerRepository => managerRepository.GetAllManagers())
            .Returns(new List<Manager> { manager });

        _requestHandlerRepository.Setup(requestHandlerRepository => requestHandlerRepository.GetAllRequestHandlers())
            .Returns(new List<RequestHandler> { requestHandler });

        _constructionCompanyAdminRepository.Setup(constructionCompanyAdminRepository =>
                constructionCompanyAdminRepository.GetAllConstructionCompanyAdmins())
            .Returns(new List<ConstructionCompanyAdmin> {constructionCompanyAdmin});

        _sessionRepository.Setup(sessionRepository => sessionRepository.CreateSession(It.IsAny<Session>()));


        Session sessionForUserObtained = _sessionService.Authenticate(user.Email, user.Password);
        _administratorRepository.VerifyAll();
        _managerRepository.VerifyAll();
        _requestHandlerRepository.VerifyAll();
        _constructionCompanyAdminRepository.VerifyAll();
        _sessionRepository.VerifyAll();

        Assert.IsNotNull(sessionForUserObtained);
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

        _constructionCompanyAdminRepository.Setup(constructionCompanyAdminRepository =>
                constructionCompanyAdminRepository.GetAllConstructionCompanyAdmins())
            .Returns(new List<ConstructionCompanyAdmin>());

        _sessionRepository.Setup(sessionRepository => sessionRepository.GetSessionBySessionString(It.IsAny<Guid>()))
            .Returns(dummySession);

        Assert.ThrowsException<InvalidCredentialException>(() =>
            _sessionService.Authenticate(It.IsAny<string>(), It.IsAny<string>()));

        _administratorRepository.VerifyAll();
        _managerRepository.VerifyAll();
        _requestHandlerRepository.VerifyAll();
        _constructionCompanyAdminRepository.VerifyAll();
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

        Assert.ThrowsException<UnknownServiceException>(() =>
            _sessionService.Authenticate(It.IsAny<string>(), It.IsAny<string>()));

        _administratorRepository.VerifyAll();
        _managerRepository.VerifyAll();
        _requestHandlerRepository.VerifyAll();
        _sessionRepository.VerifyAll();
    }

    [TestMethod]
    public void CheckIfUserIsAuthenticated_ReturnsTrue()
    {
        Manager manager = new Manager();
        manager.Email = "example@gmail.com";

        _administratorRepository.Setup(administratorRepository => administratorRepository.GetAllAdministrators())
            .Returns(new List<Administrator>());

        _managerRepository.Setup(managerRepository => managerRepository.GetAllManagers())
            .Returns(new List<Manager> { manager });

        _requestHandlerRepository.Setup(requestHandlerRepository => requestHandlerRepository.GetAllRequestHandlers())
            .Returns(new List<RequestHandler>());

        _constructionCompanyAdminRepository.Setup(constructionCompanyAdminRepository =>
                constructionCompanyAdminRepository.GetAllConstructionCompanyAdmins())
            .Returns(new List<ConstructionCompanyAdmin>());

        Assert.IsTrue(_sessionService.IsUserAuthenticated("example@gmail.com"));

        _administratorRepository.VerifyAll();
        _managerRepository.VerifyAll();
        _requestHandlerRepository.VerifyAll();
        _constructionCompanyAdminRepository.VerifyAll();
    }

    [TestMethod]
    public void CheckIfUserIsAuthenticated_ReturnsUnknownServiceException()
    {
        Manager manager = new Manager();
        manager.Email = "example@gmail.com";

        _requestHandlerRepository.Setup(requestHandlerRepository => requestHandlerRepository.GetAllRequestHandlers())
            .Throws<Exception>();

        Assert.ThrowsException<UnknownServiceException>(() => _sessionService.IsUserAuthenticated("example@gmail.com"));

        _requestHandlerRepository.VerifyAll();
    }
    
    #endregion
    
    #region Logout user

    [TestMethod]
    public void Logout_UserIsLoggedOut()
    {
        Session dummySession = new Session();
        dummySession.UserId = _sampleUserGuid;

        _sessionRepository.Setup(sessionRepository => sessionRepository.GetSessionBySessionString(It.IsAny<Guid>()))
            .Returns(dummySession);

        _sessionRepository.Setup(sessionRepository => sessionRepository.DeleteSession(It.IsAny<Session>()));

        _sessionService.Logout(dummySession.UserId);
        _sessionRepository.VerifyAll();
    }

    [TestMethod]
    public void Logout_UserIsNotFound()
    {
        Session dummySession = null;

        _sessionRepository.Setup(sessionRepository => sessionRepository.GetSessionBySessionString(It.IsAny<Guid>()))
            .Returns(dummySession);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() => _sessionService.Logout(It.IsAny<Guid>()));
        _sessionRepository.VerifyAll();
    }

    [TestMethod]
    public void Logout_UnknownExceptionIsThrown()
    {
        Session dummySession = new Session();
        dummySession.UserId = _sampleUserGuid;

        _sessionRepository.Setup(sessionRepository => sessionRepository.GetSessionBySessionString(It.IsAny<Guid>()))
            .Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() => _sessionService.Logout(It.IsAny<Guid>()));
        _sessionRepository.VerifyAll();
    }

    #endregion
}