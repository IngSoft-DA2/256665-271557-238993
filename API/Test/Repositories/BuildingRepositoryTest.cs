using System.Collections;
using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories.CustomExceptions;

namespace Test.Repositories;

[TestClass]
public class BuildingRepositoryTest
{
    private DbContext _dbContext;
    private BuildingRepository _buildingRepository;

    [TestInitialize]
    public void TestInitialize()
    {
        _dbContext = CreateDbContext("CategoryRepositoryTest");
        _dbContext.Set<Building>();
        _buildingRepository = new BuildingRepository(_dbContext);
    }

    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
    }

    [TestMethod]
    public void GetAllBuildings_BuildingsAreReturn()
    {
        Building buildingInDb = new Building
        {
            Id = Guid.NewGuid(),
            Name = "Building 1",
            Address = "Address 1",
            Location = new Location
            {
                Id = Guid.NewGuid(),
                Latitude = 1.23,
                Longitude = 6.56
            },
            CommonExpenses = 100,
            ConstructionCompany = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Construction Company 1",
            },
            ManagerId = Guid.NewGuid(),
            Flats = new List<Flat>()
        };

        IEnumerable<Building> buildingsInDb = new List<Building> { buildingInDb };

        _dbContext.Set<Building>().Add(buildingInDb);
        _dbContext.SaveChanges();

        IEnumerable<Building> buildingsReturn = _buildingRepository.GetAllBuildings(buildingInDb.ManagerId);
        Assert.IsTrue(buildingsReturn.SequenceEqual(buildingsInDb));
    }
    
    [TestMethod]
    public void GetAllBuildings_UnknownExceptionThrown()
    {
        Mock<DbContext> dbContextMock = new Mock<DbContext>();
        dbContextMock.Setup(dbContext => dbContext.Set<Building>()).Throws(new Exception("Unknown exception"));

        BuildingRepository buildingRepository = new BuildingRepository(dbContextMock.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => buildingRepository.GetAllBuildings(Guid.NewGuid()));
    }

    [TestMethod]
    public void GetBuildingById_ReturnsBuilding()
    {
        Guid ownerId = Guid.NewGuid();
        Guid buildingInDbId = Guid.NewGuid();
        
        Building buildingInDb = new Building
        {
            Id = buildingInDbId,
            Name = "Building 1",
            Address = "Address 1",
            Location = new Location
            {
                Id = Guid.NewGuid(),
                Latitude = 1.23,
                Longitude = 6.56
            },
            CommonExpenses = 100,
            ConstructionCompany = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Construction Company 1",
            },
            ManagerId = Guid.NewGuid(),
            Flats = new List<Flat>
            {
                new Flat
                {
                    Id = Guid.NewGuid(),
                    BuildingId = buildingInDbId,
                    Floor = 1,
                    RoomNumber = 101,
                    OwnerId = ownerId,
                    OwnerAssigned = new Owner
                    {
                        Id = ownerId,
                        Firstname = "Owner 1",
                        Lastname = "Owner 1",
                        Email = "owner@gmail.com",
                    },
                    TotalRooms = 4,
                    TotalBaths = 2,
                    HasTerrace = true
                }
            }
        };

        _dbContext.Set<Building>().Add(buildingInDb);
        _dbContext.SaveChanges();

        Building buildingReturn = _buildingRepository.GetBuildingById(buildingInDb.Id);
        Assert.AreEqual(buildingInDb, buildingReturn);
    }
    
    [TestMethod]
    public void GetBuildingById_ThrowsUnknownException()
    {
        Mock<DbContext> dbContextMock = new Mock<DbContext>();
        dbContextMock.Setup(dbContext => dbContext.Set<Building>()).Throws(new Exception("Unknown exception"));

        BuildingRepository buildingRepository = new BuildingRepository(dbContextMock.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => buildingRepository.GetBuildingById(Guid.NewGuid()));
    }

    [TestMethod]
    public void CreateBuilding_BuildingIsCreated()
    {
        Guid ownerId = Guid.NewGuid();
        Guid buildingIdToCreate = Guid.NewGuid();
        
        Building buildingToCreate = new Building
        {
            Id = buildingIdToCreate,
            Name = "Building 1",
            Address = "Address 1",
            Location = new Location
            {
                Id = Guid.NewGuid(),
                Latitude = 1.23,
                Longitude = 6.56
            },
            CommonExpenses = 100,
            ConstructionCompany = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Construction Company 1",
            },
            ManagerId = Guid.NewGuid(),
            Flats = new List<Flat>
            {
                new Flat
                {
                    Id = Guid.NewGuid(),
                    BuildingId = buildingIdToCreate,
                    Floor = 1,
                    RoomNumber = 101,
                    OwnerId = ownerId,
                    OwnerAssigned = new Owner
                    {
                        Id = ownerId,
                        Firstname = "Owner 1",
                        Lastname = "Owner 1",
                        Email = "owner@gmail.com",
                    },
                    TotalRooms = 4,
                    TotalBaths = 2,
                    HasTerrace = true
                }
            }
        };

        _buildingRepository.CreateBuilding(buildingToCreate);
        Building buildingInDb = _dbContext.Set<Building>().Find(buildingToCreate.Id);
        Assert.AreEqual(buildingToCreate, buildingInDb);
    }
    [TestMethod]
    public void CreateBuilding_ThrowsUnknownException()
    {
        Mock<DbContext> dbContextMock = new Mock<DbContext>();
        dbContextMock.Setup(dbContext => dbContext.Set<Building>()).Throws(new Exception("Unknown exception"));

        BuildingRepository buildingRepository = new BuildingRepository(dbContextMock.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => buildingRepository.CreateBuilding(new Building()));
    }
    
}