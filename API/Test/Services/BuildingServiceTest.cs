using Domain;
using IRepository;
using IServiceLogic;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class BuildingServiceTest
{
    #region Initializing Aspects

    private Mock<IBuildingRepository> _buildingRepository;
    private Mock<ISessionService> _sessionService;
    private BuildingService _buildingService;
    private Building _genericBuilding;
    private ConstructionCompany _constructionCompany;

    [TestInitialize]
    public void Initialize()
    {
        _buildingRepository = new Mock<IBuildingRepository>(MockBehavior.Strict);
        _sessionService = new Mock<ISessionService>(MockBehavior.Strict);
        _buildingService = new BuildingService(_buildingRepository.Object, _sessionService.Object);

       Guid managerId = Guid.NewGuid();
        _genericBuilding = new Building
        {
            Id = Guid.NewGuid(),
            Name = "Building 1",
            ManagerId = managerId,
            Manager = new Manager
            {
                Id = managerId,
                Firstname = "ManagerName",
                Email = "manager@gmail.com",
                Password = "password",
                Buildings = new List<Building>(),
                Requests = new List<MaintenanceRequest>(),
                Role = Domain.Enums.SystemUserRoleEnum.Manager
            },
            ConstructionCompany = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Construction Company 1"
            },
           
            Address = "Address 1",
            CommonExpenses = 100,
            Location = new Location
            {
                Latitude = 1.0,
                Longitude = 1.0
            },
            Flats = new List<Flat>
            {
                new Flat
                {
                    Id = Guid.NewGuid(),
                    RoomNumber = "1",
                    OwnerAssigned = new Owner()
                    {
                        Id = Guid.NewGuid(),
                        Firstname = "OwnerName",
                        Lastname = "LastName",
                        Email = "owner@gmail.com"
                    },
                    BuildingId = Guid.NewGuid(),
                    TotalRooms = 3,
                    TotalBaths = 2,
                    Floor = 2,
                    HasTerrace = false
                }
            }
        };

        _constructionCompany = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "Construction Company Updated"
        };
    }

    #endregion

    #region Get All Buildings

    [TestMethod]
    public void GetAllBuildingsTest_ReturnsAllBuildingsCorrectly()
    {
        IEnumerable<Building> expectedBuildings = new List<Building> { _genericBuilding };

        _buildingRepository.Setup(repo => repo.GetAllBuildings(It.IsAny<Guid>())).Returns(expectedBuildings);

        IEnumerable<Building> serviceResponse = _buildingService.GetAllBuildings(It.IsAny<Guid>());

        Assert.IsTrue(expectedBuildings.SequenceEqual(serviceResponse));

        _buildingRepository.VerifyAll();
    }

    [TestMethod]
    public void GetAllBuildingsTest_ThrowsUnknownServiceException()
    {
        _buildingRepository.Setup(repo => repo.GetAllBuildings(It.IsAny<Guid>())).Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() => _buildingService.GetAllBuildings(It.IsAny<Guid>()));

        _buildingRepository.VerifyAll();
    }

    #endregion

    #region Get Building By Id

    [TestMethod]
    public void GetBuildingByIdTest_ReturnsBuildingCorrectly()
    {
        _buildingRepository.Setup(repo => repo.GetBuildingById(It.IsAny<Guid>())).Returns(_genericBuilding);

        Building serviceResponse = _buildingService.GetBuildingById(Guid.NewGuid());

        Assert.AreEqual(_genericBuilding, serviceResponse);

        _buildingRepository.VerifyAll();
    }

    [TestMethod]
    public void GetBuildingByIdTest_ThrowsUnknownServiceException()
    {
        _buildingRepository.Setup(repo => repo.GetBuildingById(It.IsAny<Guid>())).Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() => _buildingService.GetBuildingById(Guid.NewGuid()));

        _buildingRepository.VerifyAll();
    }

    [TestMethod]
    public void GivenBuildingThatDoesNotExist_ThrowsObjectNotFoundServiceException()
    {
        _buildingRepository.Setup(repo => repo.GetBuildingById(It.IsAny<Guid>())).Returns((Building)null);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() => _buildingService.GetBuildingById(Guid.NewGuid()));

        _buildingRepository.VerifyAll();
    }

    #endregion

    #region Create Building

    [TestMethod]
    public void GivenCorrectBuilding_CreatesBuildingCorrectly()
    {
        _buildingRepository.Setup(repo => repo.CreateBuilding(_genericBuilding));
        _buildingRepository.Setup(repo => repo.GetAllBuildings(It.IsAny<Guid>())).Returns(new List<Building>());
        _sessionService.Setup(sessionService => sessionService.IsUserAuthenticated(It.IsAny<string>())).Returns(true);

        _buildingService.CreateBuilding(_genericBuilding);

        _buildingRepository.Verify(repo => repo.CreateBuilding(_genericBuilding), Times.Once);
        _sessionService.VerifyAll();
    }

    [TestMethod]
    public void GivenBuildingWithNameEmptyaOnCreate_ThrowsInvalidBuildingException()
    {
        _genericBuilding.Name = "";

        Assert.ThrowsException<ObjectErrorServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));
    }

    [TestMethod]
    public void GivenBuildingWithAddressEmptyOnCreate_ThrowsInvalidBuildingException()
    {
        _genericBuilding.Address = "";

        Assert.ThrowsException<ObjectErrorServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));
    }

    [TestMethod]
    public void GivenBuildingWithLocationNullOnCreate_ThrowsInvalidBuildingException()
    {
        _genericBuilding.Location = null;

        Assert.ThrowsException<ObjectErrorServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));
    }

    [TestMethod]
    public void GivenBuildingWithConstructionCompanyNullOnCreate_ThrowsInvalidBuildingException()
    {
        _genericBuilding.ConstructionCompany = null;

        Assert.ThrowsException<ObjectErrorServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));
    }

    [TestMethod]
    public void GivenBuildingWithCommonExpensesNegativeOnCreate_ThrowsInvalidBuildingException()
    {
        _genericBuilding.CommonExpenses = -1;

        Assert.ThrowsException<ObjectErrorServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));
    }

    [TestMethod]
    public void GivenBuildingWithManagerIdEmptyOnCreate_ThrowsInvalidBuildingException()
    {
        _genericBuilding.ManagerId = Guid.Empty;

        Assert.ThrowsException<ObjectErrorServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));
    }

    [TestMethod]
    public void GivenBuildingWithIdEmptyOnCreate_ThrowsInvalidBuildingException()
    {
        _genericBuilding.Id = Guid.Empty;

        Assert.ThrowsException<ObjectErrorServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));
    }

    [TestMethod]
    public void GivenBuildingWithFlatsNullOnCreate_ThrowsInvalidBuildingException()
    {
        _genericBuilding.Flats = null;

        Assert.ThrowsException<ObjectErrorServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));
    }

    [TestMethod]
    public void GivenBuildingWithRepeatedNameOnCreate_ThrowsObjectRepeatedServiceException()
    {
        _buildingRepository.Setup(repo => repo.GetAllBuildings(It.IsAny<Guid>()))
            .Returns(new List<Building> { _genericBuilding });

        Assert.ThrowsException<ObjectRepeatedServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));
        _buildingRepository.VerifyAll();
    }

    [TestMethod]
    public void GivenBuildingWithRepeatedLocationOnCreate_ThrowsObjectRepeatedServiceException()
    {
        _buildingRepository.Setup(repo => repo.GetAllBuildings(It.IsAny<Guid>()))
            .Returns(new List<Building> { _genericBuilding });

        Building buildingWithRepeatedLocation = new Building
        {
            Id = Guid.NewGuid(),
            Name = "Building 4",
            ConstructionCompany = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Construction Company 1"
            },
            ManagerId = Guid.NewGuid(),
            Address = "Address 1",
            CommonExpenses = 100,
            Location = _genericBuilding.Location,
            Flats = new List<Flat>
            {
                new Flat
                {
                    Id = Guid.NewGuid(),
                    RoomNumber = "1",
                    OwnerAssigned = new Owner()
                    {
                        Id = Guid.NewGuid(),
                        Firstname = "OwnerName",
                        Lastname = "LastName",
                        Email = "owner@gmail.com"
                    },
                    BuildingId = Guid.NewGuid(),
                    TotalRooms = 3,
                    TotalBaths = 2,
                    Floor = 2,
                    HasTerrace = false
                }
            }
        };

        Assert.ThrowsException<ObjectRepeatedServiceException>(() =>
            _buildingService.CreateBuilding(buildingWithRepeatedLocation));
        _buildingRepository.VerifyAll();
    }

    [TestMethod]
    public void CreateBuilding_ThrowsObjectRepeatedServiceException()
    {
        _buildingRepository.Setup(repo => repo.GetAllBuildings(It.IsAny<Guid>()))
            .Returns(new List<Building> { _genericBuilding });

        Assert.ThrowsException<ObjectRepeatedServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));

        _buildingRepository.Verify(repo => repo.GetAllBuildings(It.IsAny<Guid>()), Times.Once);
    }

    [TestMethod]
    public void CreateBuilding_ThrowsUnknownServiceException()
    {
        _buildingRepository.Setup(repo => repo.GetAllBuildings(It.IsAny<Guid>())).Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));

        _buildingRepository.Verify(repo => repo.GetAllBuildings(It.IsAny<Guid>()), Times.Once);
    }

    #endregion

    #region Update Building

    [TestMethod]
    public void UpdateBuildingTest_UpdatesBuildingCorrectly()
    {
        Building buildingWithUpdates = new Building
        {
            Id = _genericBuilding.Id,
            CommonExpenses = 2000000,
            ConstructionCompany = _constructionCompany
        };

        _buildingRepository.Setup(repo => repo.GetBuildingById(It.IsAny<Guid>())).Returns(_genericBuilding);

        _buildingRepository.Setup(repo => repo.UpdateBuilding(It.IsAny<Building>()));

        _buildingService.UpdateBuilding(buildingWithUpdates);

        _buildingRepository.Verify(repo => repo.GetBuildingById(It.IsAny<Guid>()), Times.Once);
        _buildingRepository.Verify(repo => repo.UpdateBuilding(It.IsAny<Building>()), Times.Once);
    }

    [TestMethod]
    public void GivenUpdate_WithErrorOnProperties_ThrowsInvalidBuildingException()
    {
        Building buildingWithUpdates = new Building
        {
            Id = _genericBuilding.Id,
            CommonExpenses = -1,
            ConstructionCompany = _constructionCompany
        };

        _buildingRepository.Setup(repo => repo.GetBuildingById(It.IsAny<Guid>())).Returns(_genericBuilding);

        Assert.ThrowsException<ObjectErrorServiceException>(() => _buildingService.UpdateBuilding(buildingWithUpdates));

        _buildingRepository.Verify(repo => repo.GetBuildingById(It.IsAny<Guid>()), Times.Once);
    }

    [TestMethod]
    public void GivenUpdateBuilding_ThrowsObjectRepeatedServiceException()
    {
        _buildingRepository.Setup(repo => repo.GetBuildingById(It.IsAny<Guid>())).Returns(_genericBuilding);

        Assert.ThrowsException<ObjectRepeatedServiceException>(() => _buildingService.UpdateBuilding(_genericBuilding));

        _buildingRepository.Verify(repo => repo.GetBuildingById(It.IsAny<Guid>()), Times.Once);
    }

    [TestMethod]
    public void GivenUpdateBuilding_ThrowsUnknownServiceException()
    {
        _buildingRepository.Setup(repo => repo.GetBuildingById(It.IsAny<Guid>())).Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() => _buildingService.UpdateBuilding(_genericBuilding));

        _buildingRepository.Verify(repo => repo.GetBuildingById(It.IsAny<Guid>()), Times.Once);
    }

    #endregion

    #region Delete Building

    [TestMethod]
    public void DeleteBuildingTest_DeletesBuildingCorrectly()
    {
        _buildingRepository.Setup(repo => repo.DeleteBuilding(It.IsAny<Building>()));

        _buildingRepository.Setup(repo => repo.GetBuildingById(It.IsAny<Guid>())).Returns(_genericBuilding);

        _buildingService.DeleteBuilding(It.IsAny<Guid>());

        _buildingRepository.Verify(repo => repo.DeleteBuilding(It.IsAny<Building>()), Times.Once);
        _buildingRepository.Verify(repo => repo.GetBuildingById(It.IsAny<Guid>()), Times.Once);
    }

    [TestMethod]
    public void DeleteBuildingTest_ThrowsObjectNotFoundServiceException()
    {
        Building _dummyBuilding = null;
        _buildingRepository.Setup(repo => repo.GetBuildingById(It.IsAny<Guid>())).Returns(_dummyBuilding);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() => _buildingService.DeleteBuilding(It.IsAny<Guid>()));

        _buildingRepository.VerifyAll();
    }

    [TestMethod]
    public void DeleteBuildingTest_ThrowsUnknownServiceException()
    {
        _buildingRepository.Setup(repo => repo.GetBuildingById(It.IsAny<Guid>())).Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() => _buildingService.DeleteBuilding(It.IsAny<Guid>()));

        _buildingRepository.VerifyAll();
    }

    #endregion
}