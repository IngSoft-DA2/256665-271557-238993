using Domain;
using IRepository;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class BuildingServiceTest
{
    private Mock<IBuildingRepository> _mockRepository;
    private BuildingService _buildingService;

    [TestInitialize]
    public void Initialize()
    {
        _mockRepository = new Mock<IBuildingRepository>(MockBehavior.Strict);
        _buildingService = new BuildingService(_mockRepository.Object);
    }

    [TestMethod]
    public void GetAllBuildingsTest_ReturnsAllBuildingsCorrectly()
    {
        IEnumerable<Building> expectedBuildings = new List<Building>
        {
            new Building
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
            }
        };

        _mockRepository.Setup(repo => repo.GetAllBuildings()).Returns(expectedBuildings);

        IEnumerable<Building> serviceResponse = _buildingService.GetAllBuildings();

        Assert.IsTrue(expectedBuildings.SequenceEqual(serviceResponse));
    }

    [TestMethod]
    public void GetAllBuildingsTest_ThrowsException()
    {
        _mockRepository.Setup(repo => repo.GetAllBuildings()).Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() => _buildingService.GetAllBuildings());

        _mockRepository.VerifyAll();
    }

    [TestMethod]
    public void GetBuildingByIdTest_ReturnsBuildingCorrectly()
    {
        Building expectedBuildings = new Building
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
        
        _mockRepository.Setup(repo => repo.GetBuildingById(It.IsAny<Guid>())).Returns(expectedBuildings);
        
        Building serviceResponse = _buildingService.GetBuildingById(Guid.NewGuid());
        
        Assert.AreEqual(expectedBuildings, serviceResponse);
        
        _mockRepository.VerifyAll();
    }
}