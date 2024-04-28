using Domain;
using IRepository;
using Moq;
using ServiceLogic;

namespace Test.Services;

[TestClass]

public class ManagerServiceTest
{
    [TestMethod]
    public void GetAllManagers_ShouldReturnsAllManagers()
    {
        var managerRepository = new Mock<IManagerRepository>();
        var managerService = new ManagerService(managerRepository.Object);
        var expectedManagers = new List<Manager>
        {
            new Manager { Id = Guid.NewGuid(), Firstname = "Manager 1" },
            new Manager { Id = Guid.NewGuid(), Firstname = "Manager 2" }
        };
        
        managerRepository.Setup(x => x.GetAllManagers()).Returns(expectedManagers);
        
        var actualManagers = managerService.GetAllManagers();
        
        Assert.AreEqual(expectedManagers, actualManagers);
    }
}