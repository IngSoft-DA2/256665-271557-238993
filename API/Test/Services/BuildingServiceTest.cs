using Domain;
using IRepository;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class BuildingServiceTest
{
    [TestMethod]
    public void GetAllBuildingsTest_ReturnsAllBuildingsCorrectly()
    {
        Mock<IBuildingRepository> mockRepository = new Mock<IBuildingRepository>(MockBehavior.Strict);

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

        mockRepository.Setup(repo => repo.GetAllBuildings()).Returns(expectedBuildings);
        BuildingService buildingService = new BuildingService(mockRepository.Object);

        IEnumerable<Building> serviceResponse = buildingService.GetAllBuildings();

        Assert.IsTrue(expectedBuildings.SequenceEqual(serviceResponse));
    }
    
    [TestMethod]
    public void GetAllBuildingsTest_ThrowsException()
    {
        Mock<IBuildingRepository> mockRepository = new Mock<IBuildingRepository>(MockBehavior.Strict);

        mockRepository.Setup(repo => repo.GetAllBuildings()).Throws(new Exception());
        BuildingService buildingService = new BuildingService(mockRepository.Object);

        Assert.ThrowsException<UnknownServiceException>(() => buildingService.GetAllBuildings());
        
        mockRepository.VerifyAll();
    }
}