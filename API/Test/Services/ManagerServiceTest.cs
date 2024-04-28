using Domain;
using IRepository;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]

public class ManagerServiceTest
{
    
    private Mock<IManagerRepository> _managerRepository;
    private ManagerService _managerService;
    
    [TestInitialize]
    public void Initialize()
    {
        _managerRepository = new Mock<IManagerRepository>();
        _managerService = new ManagerService(_managerRepository.Object);
    }
    
    [TestMethod]
    public void GetAllManagers_ShouldReturnsAllManagers()
    {
        IEnumerable<Manager> expectedManagers = new List<Manager>
        {
            new Manager { Id = Guid.NewGuid(), Firstname = "Manager 1" },
            new Manager { Id = Guid.NewGuid(), Firstname = "Manager 2" }
        };
        
        _managerRepository.Setup(x => x.GetAllManagers()).Returns(expectedManagers);
        
        var actualManagers = _managerService.GetAllManagers();
        
        Assert.AreEqual(expectedManagers, actualManagers);
        
        _managerRepository.VerifyAll();
    }
    
    [TestMethod]
    public void GetAllManagers_ShouldThrowUnknownServiceException()
    {
        _managerRepository.Setup(x => x.GetAllManagers()).Throws(new Exception());
        
        Assert.ThrowsException<UnknownServiceException>(() => _managerService.GetAllManagers());
        
        _managerRepository.VerifyAll();
    }
}