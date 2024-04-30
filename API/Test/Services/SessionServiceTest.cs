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
    
    [TestInitialize]
    public void Initialize()
    {
        _sessionRepository = new Mock<ISessionRepository>(MockBehavior.Strict);
        _sessionService = new SessionService(_sessionRepository.Object);
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
}