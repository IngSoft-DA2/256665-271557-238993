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

    [TestInitialize]
    public void Initialize()
    {
        _buildingRepository = new Mock<IBuildingRepository>(MockBehavior.Strict);
        _buildingService = new BuildingService(_buildingRepository.Object);
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
}