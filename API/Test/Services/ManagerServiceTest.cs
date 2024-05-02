using Domain;
using IRepository;
using IServiceLogic;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class ManagerServiceTest
{
    #region Initializing Aspects
    
    private Mock<IManagerRepository> _managerRepository;
    private ManagerService _managerService;
    
    private IInvitationService _mockInvitationService;
    private InvitationService _invitationService;

    [TestInitialize]
    public void Initialize()
    {
        _managerRepository = new Mock<IManagerRepository>();
        _managerService = new ManagerService(_managerRepository.Object, _mockInvitationService);
    }
    
    #endregion
    
    #region Get All Managers

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
    
    #endregion
    
    #region Create Manager

    [TestMethod]
    public void CreateManager_ShouldCreateManager()
    {
        Manager manager = new Manager
        {
            Id = Guid.NewGuid(),
            Firstname = "Manager",
            Email = "person@gmail.com",
            Password = "password",
            Buildings = new List<Building>()
        };

        _managerRepository.Setup(service => service.CreateManager(manager));
        _managerService.CreateManager(manager, It.IsAny<Guid>());

        _managerRepository.Verify(x => x.CreateManager(manager), Times.Once);
    }

    [TestMethod]
    public void GivenEmptyNameOnCreate_ShouldThrowException()
    {
        Manager manager = new Manager { Id = Guid.NewGuid(), Firstname = "" };

        Assert.ThrowsException<ObjectErrorServiceException>(() => _managerService.CreateManager(manager, It.IsAny<Guid>()));
    }

    [TestMethod]
    public void GivenEmptyEmailOnCreate_ShouldThrowException()
    {
        Manager manager = new Manager { Id = Guid.NewGuid(), Firstname = "Manager", Email = "" };

        Assert.ThrowsException<ObjectErrorServiceException>(() => _managerService.CreateManager(manager,It.IsAny<Guid>()));
    }

    [TestMethod]
    public void GivenInvalidEmailOnCreate_ShouldThrowException()
    {
        Manager manager = new Manager { Id = Guid.NewGuid(), Firstname = "Manager", Email = "invalidemail" };

        Assert.ThrowsException<ObjectErrorServiceException>(() => _managerService.CreateManager(manager,It.IsAny<Guid>()));
    }

    [TestMethod]
    public void GivenNullPasswordOnCreate_ShouldThrowException()
    {
        Manager manager = new Manager
        {
            Id = Guid.NewGuid(),
            Firstname = "Manager",
            Email = "person@gmail.com",
            Password = ""
        };

        Assert.ThrowsException<ObjectErrorServiceException>(() => _managerService.CreateManager(manager,It.IsAny<Guid>()));
    }

    [TestMethod]
    public void GivenPasswordLessThan8CharactersOnCreate_ShouldThrowException()
    {
        Manager manager = new Manager
        {
            Id = Guid.NewGuid(),
            Firstname = "Manager",
            Email = "person@gmail.com",
            Password = "1234567"
        };

        Assert.ThrowsException<ObjectErrorServiceException>(() => _managerService.CreateManager(manager,It.IsAny<Guid>()));
    }

    [TestMethod]
    public void GivenNullBuildingsOnCreate_ShouldThrowException()
    {
        Manager manager = new Manager
        {
            Id = Guid.NewGuid(),
            Firstname = "Manager",
            Email = "person@gmail.com",
            Password = "1234567"
        };

        Assert.ThrowsException<ObjectErrorServiceException>(() => _managerService.CreateManager(manager,It.IsAny<Guid>()));
    }

    [TestMethod]
    public void GivenRepeatedEmailOnCreate_ShouldThrowException()
    {
        Manager manager = new Manager
        {
            Id = Guid.NewGuid(),
            Firstname = "Manager",
            Email = "persona@gmail.com",
            Password = "12345678",
            Buildings = new List<Building>()
        };

        _managerRepository.Setup(x => x.GetAllManagers()).Returns(new List<Manager> { manager });

        Assert.ThrowsException<ObjectRepeatedServiceException>(() => _managerService.CreateManager(manager, It.IsAny<Guid>()));

        _managerRepository.VerifyAll();
    }

    [TestMethod]
    public void CreateManager_ShouldThrowUnknownServiceException()
    {
        Manager manager = new Manager
        {
            Id = Guid.NewGuid(),
            Firstname = "Manager",
            Email = "persona@gmail.com",
            Password = "12345678",
            Buildings = new List<Building>()
        };

        _managerRepository.Setup(x => x.CreateManager(manager)).Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() => _managerService.CreateManager(manager,It.IsAny<Guid>()));

        _managerRepository.VerifyAll();
    }
    
    #endregion
    
    #region Delete Manager

    [TestMethod]
    public void DeleteManagerById_ShouldDeleteManager()
    {
        Manager manager = new Manager
        {
            Id = Guid.NewGuid(),
            Firstname = "Manager",
            Email = "",
        };
        
        _managerRepository.Setup(repo => repo.DeleteManager(manager));
        _managerRepository.Setup(repo => repo.GetManagerById(It.IsAny<Guid>())).Returns(manager);
        _managerService.DeleteManagerById(manager.Id);

        _managerRepository.Verify(x => x.DeleteManager(manager), Times.Once);
    }

    [TestMethod]
    public void DeleteManagerById_ShouldThrowObjectNotFoundServiceException()
    {
        Guid managerId = Guid.NewGuid();
        Manager manager = null;

        _managerRepository.Setup(repo => repo.GetManagerById(managerId)).Returns(manager);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() => _managerService.DeleteManagerById(managerId));

        _managerRepository.VerifyAll();
    }
    
    [TestMethod]
    public void DeleteManagerById_ShouldThrowUnknownServiceException()
    {
        Guid managerId = Guid.NewGuid();

        _managerRepository.Setup(repo => repo.GetManagerById(managerId)).Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() => _managerService.DeleteManagerById(managerId));

        _managerRepository.VerifyAll();
    }
    
    #endregion
}