using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.ManagerRequests;
using WebModel.Responses.ManagerResponses;

namespace Test.Adapters;

[TestClass]
public class ManagerAdapterTest
{
    #region Initialize

    private Mock<IManagerService> _managerService;
    private Mock<IInvitationService> _invitationService;
    private ManagerAdapter _managerAdapter;

    [TestInitialize]
    public void Initialize()
    {
        _managerService = new Mock<IManagerService>(MockBehavior.Strict);
        _invitationService = new Mock<IInvitationService>(MockBehavior.Strict);
        _managerAdapter = new ManagerAdapter(_managerService.Object, _invitationService.Object);
    }

    #endregion

    #region Get All Managers

    [TestMethod]
    public void GetAllManagers_ShouldConvertFromDomainToResponse()
    {
        IEnumerable<Manager> domainResponse = new List<Manager>()
        {
            new Manager
            {
                Id = Guid.NewGuid(),
                Firstname = "Michael Kent",
                Email = "michaelKent@gmail.com",
                Password = "random238",
                Buildings = new List<Building>(),
                Requests = new List<MaintenanceRequest>()
            }
        };

        IEnumerable<GetManagerResponse> expectedAdapterResponse = new List<GetManagerResponse>()
        {
            new GetManagerResponse
            {
                Id = domainResponse.First().Id,
                Name = domainResponse.First().Firstname,
                Email = domainResponse.First().Email,
                BuildingsId = new List<Guid>(),
                MaintenanceRequestsId = new List<Guid>()
            }
        };

        _managerService.Setup(service => service.GetAllManagers()).Returns(domainResponse);

        IEnumerable<GetManagerResponse> adapterResponse = _managerAdapter.GetAllManagers();

        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }

    [TestMethod]
    public void GetAllManagers_ShouldThrowException()
    {
        _managerService.Setup(service => service.GetAllManagers()).Throws(new Exception("Something went wrong"));

        Assert.ThrowsException<Exception>(() => _managerAdapter.GetAllManagers());
    }

    #endregion

    #region Delete Manager By Id

    [TestMethod]
    public void DeleteManagerById_ShouldDeleteManager()
    {
        _managerService.Setup(service => service.DeleteManagerById(It.IsAny<Guid>()));

        _managerAdapter.DeleteManagerById(It.IsAny<Guid>());

        _managerService.Verify(service => service.DeleteManagerById(It.IsAny<Guid>()), Times.Once);
    }

    [TestMethod]
    public void DeleteManagerById_ShouldThrowObjectNotFoundAdapterException()
    {
        _managerService.Setup(service => service.DeleteManagerById(It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundServiceException());

        Assert.ThrowsException<ObjectNotFoundAdapterException>(
            () => _managerAdapter.DeleteManagerById(It.IsAny<Guid>()));

        _managerService.Verify(service => service.DeleteManagerById(It.IsAny<Guid>()), Times.Once);
    }

    [TestMethod]
    public void DeleteManagerById_ShouldThrowException()
    {
        _managerService.Setup(service => service.DeleteManagerById(It.IsAny<Guid>()))
            .Throws(new Exception("Something went wrong"));

        Assert.ThrowsException<Exception>(() => _managerAdapter.DeleteManagerById(It.IsAny<Guid>()));

        _managerService.Verify(service => service.DeleteManagerById(It.IsAny<Guid>()), Times.Once);
    }

    #endregion

    #region Create Manager

    [TestMethod]
    public void CreateManager_ShouldCreateManager()
    {
        CreateManagerRequest dummyCreateRequest = new CreateManagerRequest()
        {
            Email = "",
            Password = ";"
        };

        _managerService.Setup(managerService =>
            managerService.CreateManager(It.IsAny<Manager>(), It.IsAny<Invitation>()));
        _invitationService.Setup(invitationService => invitationService.GetInvitationById(It.IsAny<Guid>()))
            .Returns(new Invitation());

        CreateManagerResponse adapterResponse = _managerAdapter.CreateManager(dummyCreateRequest, It.IsAny<Guid>());

        Assert.IsNotNull(adapterResponse.Id);

        _managerService.VerifyAll();
        _invitationService.VerifyAll();
    }

    [TestMethod]
    public void CreateManager_ShouldThrowObjectNotFoundAdapterException()
    {
        CreateManagerRequest dummyCreateRequest = new CreateManagerRequest();

        _managerService.Setup(service => service.CreateManager(It.IsAny<Manager>(), It.IsAny<Invitation>()))
            .Throws(new ObjectNotFoundServiceException());
        _invitationService.Setup(invitationService => invitationService.GetInvitationById(It.IsAny<Guid>()))
            .Returns(new Invitation());

        Assert.ThrowsException<ObjectNotFoundAdapterException>(() =>
            _managerAdapter.CreateManager(dummyCreateRequest, It.IsAny<Guid>()));

        _managerService.VerifyAll();
        _invitationService.VerifyAll();
    }

    [TestMethod]
    public void CreateManager_ShouldThrowObjectErrorServiceException()
    {
        CreateManagerRequest dummyCreateRequest = new CreateManagerRequest();

        _managerService.Setup(service => service.CreateManager(It.IsAny<Manager>(), It.IsAny<Invitation>()))
            .Throws(new ObjectErrorServiceException("Something went wrong"));
        _invitationService.Setup(invitationService => invitationService.GetInvitationById(It.IsAny<Guid>()))
            .Returns(new Invitation());

        Assert.ThrowsException<ObjectErrorAdapterException>(() =>
            _managerAdapter.CreateManager(dummyCreateRequest, It.IsAny<Guid>()));

        _managerService.VerifyAll();
        _invitationService.VerifyAll();
    }

    [TestMethod]
    public void CreateManager_ShouldThrowUnknownAdapterException()
    {
        CreateManagerRequest dummyCreateRequest = new CreateManagerRequest();

        _managerService.Setup(service => service.CreateManager(It.IsAny<Manager>(), It.IsAny<Invitation>()))
            .Throws(new Exception("Something went wrong"));
        _invitationService.Setup(invitationService => invitationService.GetInvitationById(It.IsAny<Guid>()))
            .Returns(new Invitation());

        Assert.ThrowsException<UnknownAdapterException>(() =>
            _managerAdapter.CreateManager(dummyCreateRequest, It.IsAny<Guid>()));

        _managerService.VerifyAll();
        _invitationService.VerifyAll();
    }

    #endregion

    #region Get Manager By Id

    [TestMethod]
    public void GetManagerById_ShouldReturnManager()
    {
        Guid buildingId = Guid.NewGuid();
        Guid requestId = Guid.NewGuid();
        
        GetManagerResponse expectedResponse = new GetManagerResponse
        {
            Id = Guid.NewGuid(),
            Name = "Michael Kent",
            Email = "michael@gmail.com",
            BuildingsId = new List<Guid>(){buildingId},
            MaintenanceRequestsId = new List<Guid>(){requestId}
        };

        _managerService.Setup(service => service.GetManagerById(It.IsAny<Guid>()))
            .Returns(new Manager
            {
                Id = expectedResponse.Id,
                Firstname = expectedResponse.Name,
                Email = expectedResponse.Email,
                Buildings = new List<Building>() {new Building() {Id = buildingId}},
                Requests = new List<MaintenanceRequest>(){new MaintenanceRequest(){Id = requestId}}
            });

        GetManagerResponse adapterResponse = _managerAdapter.GetManagerById(It.IsAny<Guid>());

        Assert.IsTrue(expectedResponse.Equals(adapterResponse));

        _managerService.Verify(service => service.GetManagerById(It.IsAny<Guid>()), Times.Once);
    }
    
    [TestMethod]
    public void GetManagerById_ShouldThrowObjectNotFoundAdapterException()
    {
        _managerService.Setup(service => service.GetManagerById(It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundServiceException());

        Assert.ThrowsException<ObjectNotFoundAdapterException>(() => _managerAdapter.GetManagerById(It.IsAny<Guid>()));

        _managerService.Verify(service => service.GetManagerById(It.IsAny<Guid>()), Times.Once);
    }
    
    [TestMethod]
    public void GetManagerById_ShouldThrowException()
    {
        _managerService.Setup(service => service.GetManagerById(It.IsAny<Guid>()))
            .Throws(new Exception("Something went wrong"));

        Assert.ThrowsException<Exception>(() => _managerAdapter.GetManagerById(It.IsAny<Guid>()));

        _managerService.Verify(service => service.GetManagerById(It.IsAny<Guid>()), Times.Once);
    }
    
    #endregion
}