using Domain;
using IRepository;
using Moq;
using ServiceLogic;

namespace Test.Services;

[TestClass]
public class SessionServiceTest
{
    #region Test Initialization
    
    private Mock<ISessionRepository> _sessionRepository;
    private SessionService _sessionService;
    private Guid _sampleUserGuid;
    
    [TestInitialize]
    public void Initialize()
    {
        _sessionRepository = new Mock<ISessionRepository>(MockBehavior.Strict);
        _sessionService = new SessionService(_sessionRepository.Object);
        _sampleUserGuid = Guid.NewGuid();
    }
    

    

    #endregion
    
    #region IsValidSessionString
    
    [TestMethod]
    public void IsValidSessionString_ValidSessionString_ReturnsTrue()
    {
        Guid token = Guid.NewGuid();
        _sessionRepository.Setup(x => x.SessionExists(token)).Returns(true);
        
        bool result = _sessionService.IsValidToken(token);
        
        _sessionRepository.VerifyAll();
        Assert.IsTrue(result);
    }
    
    [TestMethod]
    public void IsValidSessionString_InvalidSessionString_ReturnsFalse()
    {
        Guid token = Guid.NewGuid();
        _sessionRepository.Setup(x => x.SessionExists(token)).Returns(false);
        
        bool result = _sessionService.IsValidToken(token);
        
        _sessionRepository.VerifyAll();
        Assert.IsFalse(result);
    }
    
    #endregion
    
    #region GetUserBySessionString
    
    [TestMethod]
    public void GetUserBySessionString_ValidSessionString_ReturnsUser()
    {
        Guid sessionRepoExpectedResponse = _sampleUserGuid;
        _sessionRepository.Setup(x => x.GetUserBySessionString(_sampleUserGuid)).Returns(sessionRepoExpectedResponse);
        
        object? result = _sessionService.GetUserBySessionString(_sampleUserGuid);
        
        _sessionRepository.VerifyAll();
        Assert.IsNotNull(result);
        Assert.AreEqual(sessionRepoExpectedResponse, result);
    }
    
    [TestMethod]
    public void GetUserBySessionString_InvalidSessionString_ReturnsNull()
    {
        _sessionRepository.Setup(x => x.GetUserBySessionString(_sampleUserGuid)).Returns((object?)null);
        
        object? result = _sessionService.GetUserBySessionString(_sampleUserGuid);
        
        _sessionRepository.VerifyAll();
        Assert.IsNull(result);
    }
    
    #endregion
}