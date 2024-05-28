using Azure.Core;
using Domain;
using Domain.Enums;
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
    private Mock<IInvitationService> _invitationService;
    private ManagerService _managerService;

    [TestInitialize]
    public void Initialize()
    {
        _managerRepository = new Mock<IManagerRepository>(MockBehavior.Strict);
        _invitationService = new Mock<IInvitationService>(MockBehavior.Strict);

        _managerService = new ManagerService(_managerRepository.Object, _invitationService.Object);
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

    #region Get Manager By Id

    [TestMethod]
    public void GetManagerById_ShouldReturnManager()
    {
        Manager expectedManager = new Manager
        {
            Id = Guid.NewGuid(),
            Firstname = "Manager",
            Email = "manager@gmail.com",
            Password = "123456789",
            Buildings = new List<Building>(),
            Requests = new List<MaintenanceRequest>(),
            Role = SystemUserRoleEnum.Manager
        };

        _managerRepository.Setup(managerRepository =>
            managerRepository.GetManagerById(expectedManager.Id)).Returns(expectedManager);

        Manager actualManager = _managerService.GetManagerById(expectedManager.Id);
        
        _managerRepository.VerifyAll();
        Assert.IsTrue(expectedManager.Equals(actualManager));

       
    }
    
    [TestMethod]
    public void GetManagerById_ThrowsObjectNotFoundServiceException()
    {
        Guid managerId = Guid.NewGuid();
        Manager manager = null;

        _managerRepository.Setup(repo => repo.GetManagerById(managerId)).Returns(manager);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() => _managerService.GetManagerById(managerId));

        _managerRepository.VerifyAll();
    }
    
    [TestMethod]
    
    public void GetManagerById_ThrowsUnknownServiceException()
    {
        Guid managerId = Guid.NewGuid();

        _managerRepository.Setup(repo => repo.GetManagerById(managerId)).Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() => _managerService.GetManagerById(managerId));

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
            Email = "person@gmail.com",
            Password = "password",
            Buildings = new List<Building>()
        };

        Invitation dummyInvitation = new Invitation
        {
            Firstname = manager.Firstname,
            Email = manager.Email
        };

        _managerRepository.Setup(managerRepository => managerRepository.GetAllManagers()).Returns(new List<Manager>());
        _managerRepository.Setup(managerRepository => managerRepository.CreateManager(manager));
        _invitationService.Setup(invitationService =>
            invitationService.UpdateInvitation(It.IsAny<Guid>(), It.IsAny<Invitation>()));

        _managerService.CreateManager(manager, dummyInvitation);

        _managerRepository.VerifyAll();
        _invitationService.Verify(invitationRepository => invitationRepository.
            UpdateInvitation(It.IsAny<Guid>(), It.IsAny<Invitation>()), Times.Once);
    }
    
    [TestMethod]
    public void GivenEmptyEmailOnCreate_ShouldThrowException()
    {
        Manager manager = new Manager { Id = Guid.NewGuid(), Firstname = "Manager", Email = "" };

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _managerService.CreateManager(manager, It.IsAny<Invitation>()));
    }

    [TestMethod]
    public void GivenInvalidEmailOnCreate_ShouldThrowException()
    {
        Manager manager = new Manager { Id = Guid.NewGuid(), Firstname = "Manager", Email = "invalidemail" };

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _managerService.CreateManager(manager, It.IsAny<Invitation>()));
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

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _managerService.CreateManager(manager, It.IsAny<Invitation>()));
    }

    [TestMethod]
    public void GivenPasswordLessThan8CharactersOnCreate_ShouldThrowException()
    {
        Manager manager = new Manager
        {
            Id = Guid.NewGuid(),
            Firstname = "Manager",
            Email = "person@gmail.com",
            Password = "1230",
        };

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _managerService.CreateManager(manager, It.IsAny<Invitation>()));
    }

    [TestMethod]
    public void GivenManagerToCreate_BuildingListIsInitialized()
    {
        Manager manager = new Manager
        {
            Id = Guid.NewGuid(),
            Role = SystemUserRoleEnum.Manager,
            Firstname = "Manager",
            Email = "person@gmail.com",
            Password = "123456789"
        };
        Assert.IsNotNull(manager.Buildings);
    }
    
    [TestMethod]
    public void GivenRepeatedEmailOnCreate_ShouldThrowObjectRepeteadException()
    {
        Manager manager = new Manager
        {
            Id = Guid.NewGuid(),
            Role = SystemUserRoleEnum.Manager,
            Firstname = "Manager",
            Email = "persona@gmail.com",
            Password = "12345678910",
            Buildings = new List<Building>()
        };

        _managerRepository.Setup(x => x.GetAllManagers()).Returns(new List<Manager> { manager });

        Assert.ThrowsException<ObjectRepeatedServiceException>(() =>
            _managerService.CreateManager(manager, It.IsAny<Invitation>()));

        _managerRepository.VerifyAll();
    }

    [TestMethod]
    public void CreateManager_ShouldThrowUnknownServiceException()
    {
        Manager manager = new Manager
        {
            Id = Guid.NewGuid(),
            Email = "persona@gmail.com",
            Password = "12345678910",
            Buildings = new List<Building>()
        };

        Invitation dummyInvitation = new Invitation
        {
            Firstname = "managerFirstname",
            Email = "persona@gmail.com"
        };
        
        _managerRepository.Setup(managerRepository =>
            managerRepository.GetAllManagers()).Returns(new List<Manager>());
        
        _managerRepository.Setup(managerRepository =>
            managerRepository.CreateManager(manager)).Throws(new Exception());

        _invitationService.Setup(invitationService => invitationService.UpdateInvitation
            (It.IsAny<Guid>(), It.IsAny<Invitation>()));

        Assert.ThrowsException<UnknownServiceException>(() =>
            _managerService.CreateManager(manager, dummyInvitation));

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