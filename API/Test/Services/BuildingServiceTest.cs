using Domain;
using IRepository;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class BuildingServiceTest
{
    private Mock<IBuildingRepository> _buildingRepository;
    private BuildingService _buildingService;
    private Building _genericBuilding;

    [TestInitialize]
    public void Initialize()
    {
        _buildingRepository = new Mock<IBuildingRepository>(MockBehavior.Strict);
        _buildingService = new BuildingService(_buildingRepository.Object);

        _genericBuilding = new Building
        {
            Id = Guid.NewGuid(),
            Name = "Building 1",
            ConstructionCompany = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Construction Company 1"
            },
            ManagerId = Guid.NewGuid(),
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
                    RoomNumber = 1,
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
    }

    [TestMethod]
    public void GetAllBuildingsTest_ReturnsAllBuildingsCorrectly()
    {
        IEnumerable<Building> expectedBuildings = new List<Building> { _genericBuilding };

        _buildingRepository.Setup(repo => repo.GetAllBuildings()).Returns(expectedBuildings);

        IEnumerable<Building> serviceResponse = _buildingService.GetAllBuildings();

        Assert.IsTrue(expectedBuildings.SequenceEqual(serviceResponse));

        _buildingRepository.VerifyAll();
    }

    [TestMethod]
    public void GetAllBuildingsTest_ThrowsUnknownServiceException()
    {
        _buildingRepository.Setup(repo => repo.GetAllBuildings()).Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() => _buildingService.GetAllBuildings());

        _buildingRepository.VerifyAll();
    }

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

    [TestMethod]
    public void GivenCorrectBuilding_CreatesBuildingCorrectly()
    {
        _buildingRepository.Setup(repo => repo.CreateBuilding(_genericBuilding));

        _buildingRepository.Setup(repo => repo.GetAllBuildings()).Returns(new List<Building>());

        _buildingService.CreateBuilding(_genericBuilding);

        _buildingRepository.Verify(repo => repo.CreateBuilding(_genericBuilding), Times.Once);
    }

    [TestMethod]
    public void GivenBuildingWithNameEmpty_ThrowsInvalidBuildingException()
    {
        _genericBuilding.Name = "";

        Assert.ThrowsException<ObjectErrorServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));
    }

    [TestMethod]
    public void GivenBuildingWithAddressEmpty_ThrowsInvalidBuildingException()
    {
        _genericBuilding.Address = "";

        Assert.ThrowsException<ObjectErrorServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));
    }

    [TestMethod]
    public void GivenBuildingWithLocationNull_ThrowsInvalidBuildingException()
    {
        _genericBuilding.Location = null;

        Assert.ThrowsException<ObjectErrorServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));
    }

    [TestMethod]
    public void GivenBuildingWithConstructionCompanyNull_ThrowsInvalidBuildingException()
    {
        _genericBuilding.ConstructionCompany = null;

        Assert.ThrowsException<ObjectErrorServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));
    }

    [TestMethod]
    public void GivenBuildingWithCommonExpensesNegative_ThrowsInvalidBuildingException()
    {
        _genericBuilding.CommonExpenses = -1;

        Assert.ThrowsException<ObjectErrorServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));
    }

    [TestMethod]
    public void GivenBuildingWithManagerIdEmpty_ThrowsInvalidBuildingException()
    {
        _genericBuilding.ManagerId = Guid.Empty;

        Assert.ThrowsException<ObjectErrorServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));
    }

    [TestMethod]
    public void GivenBuildingWithIdEmpty_ThrowsInvalidBuildingException()
    {
        _genericBuilding.Id = Guid.Empty;

        Assert.ThrowsException<ObjectErrorServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));
    }

    [TestMethod]
    [ExpectedException(typeof(ObjectRepeatedServiceException))]
    public void GivenBuildingWithRepeatedName_ThrowsObjectRepeatedServiceException()
    {
        _buildingRepository.Setup(repo => repo.GetAllBuildings()).Returns(new List<Building> { _genericBuilding });

        _buildingService.CreateBuilding(_genericBuilding);

        _buildingRepository.Verify(repo => repo.GetAllBuildings(), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ObjectRepeatedServiceException))]
    public void GivenBuildingWithRepeatedLocation_ThrowsObjectRepeatedServiceException()
    {
        _genericBuilding.Name = "Building 2";

        _buildingRepository.Setup(repo => repo.GetAllBuildings()).Returns(new List<Building> { _genericBuilding });

        _buildingService.CreateBuilding(_genericBuilding);

        _buildingRepository.Verify(repo => repo.GetAllBuildings(), Times.Once);
    }
    
    [TestMethod]
    public void CreateBuilding_ThrowsObjectRepeatedServiceException()
    {
        _buildingRepository.Setup(repo => repo.GetAllBuildings()).Returns(new List<Building> { _genericBuilding });

        Assert.ThrowsException<ObjectRepeatedServiceException>(() => _buildingService.CreateBuilding(_genericBuilding));

        _buildingRepository.Verify(repo => repo.GetAllBuildings(), Times.Once);
    }

}