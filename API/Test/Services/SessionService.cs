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
        
        Assert.ThrowsException<UnknownServiceException>(() => _sessionService.GetUserRoleBySessionString(It.IsAny<Guid>()));
       _sessionRepository.VerifyAll();
    }

    #endregion

    
}