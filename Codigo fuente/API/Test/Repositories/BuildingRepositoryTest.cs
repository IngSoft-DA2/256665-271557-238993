using System.Collections;
using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Domain.Enums;
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
        _dbContext = CreateDbContext("BuildingRepositoryTest");
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
                UserCreatorId = Guid.NewGuid()
            },
            Manager = new Manager
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.Manager,
                Firstname = "Manager 1",
                Email = "manager@gmail.com",
                Password = "Password"
            },
            Flats = new List<Flat>
            {
                new Flat
                {
                    Id = Guid.NewGuid(),
                    Floor = 1,
                    RoomNumber = "101",
                    OwnerAssigned = new Owner
                    {
                        Id = Guid.NewGuid(),
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

        IEnumerable<Building> buildingsInDb = new List<Building> { buildingInDb };

        _dbContext.Set<Building>().Add(buildingInDb);
        _dbContext.SaveChanges();

        IEnumerable<Building> buildingsReturn = _buildingRepository.GetAllBuildings(buildingInDb.ConstructionCompany.UserCreatorId);
        Assert.IsTrue(buildingsReturn.SequenceEqual(buildingsInDb));
    }

    [TestMethod]
    public void GivenEmptyGuid_ShouldReturnAllBuildings()
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
                UserCreatorId = Guid.NewGuid()
            },
            Manager = new Manager
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.Manager,
                Firstname = "Manager 1",
                Email = "",
                Password = "Password"

            }
        };
        
        IEnumerable<Building> buildingsInDb = new List<Building> { buildingInDb };
        
        _dbContext.Set<Building>().Add(buildingInDb);
        
        _dbContext.SaveChanges();
        
        IEnumerable<Building> buildingsReturn = _buildingRepository.GetAllBuildings(Guid.Empty);
        
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
            Manager = new Manager
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.Manager,
                Firstname = "Manager 1",
                Email = "manager@gmail.com",
                Password = "Password"
            },
            Flats = new List<Flat>
            {
                new Flat
                {
                    Id = Guid.NewGuid(),
                    Floor = 1,
                    RoomNumber = "101",
                    OwnerAssigned = new Owner
                    {
                        Id = Guid.NewGuid(),
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
        Assert.IsTrue(buildingInDb.ConstructionCompany.Buildings.Equals(buildingReturn.ConstructionCompany.Buildings));
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

        Owner OwnerAssigned = new Owner
        {
            Id = Guid.NewGuid(),
            Firstname = "Owner 1",
            Lastname = "Owner 1",
            Email = "owner@gmail.com",
            Flats = new List<Flat>()
        };

        ConstructionCompany constructionCompany = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "Construction Company 1",
            Buildings = new List<Building>()
        };

        Manager manager = new Manager
        {
            Id = Guid.NewGuid(),
            Role = SystemUserRoleEnum.Manager,
            Firstname = "Manager 1",
            Email = "a@gmail.com",
            Password = "Password",
            Buildings = new List<Building>(),
            Requests = new List<MaintenanceRequest>()
        };

        _dbContext.Set<Owner>().Add(OwnerAssigned);
        _dbContext.Set<ConstructionCompany>().Add(constructionCompany);
        _dbContext.Set<Manager>().Add(manager);
        _dbContext.SaveChanges();

        Building buildingToCreate = new Building
        {
            Id = Guid.NewGuid(),
            Name = "Building 1",
            Address = "Address 1",

            Manager = manager,
            ManagerId = manager.Id,
            CommonExpenses = 100,
            ConstructionCompany = constructionCompany,
            ConstructionCompanyId = constructionCompany.Id,

            Flats = new List<Flat>
            {
                new Flat
                {
                    Id = Guid.NewGuid(),
                    Floor = 1,
                    RoomNumber = "101",
                    OwnerAssigned = OwnerAssigned,
                    OwnerId = OwnerAssigned.Id,
                    TotalRooms = 4,
                    TotalBaths = 2,
                    HasTerrace = true
                }
            }
        };

        Location location = new Location
        {
            Id = Guid.NewGuid(),
            Latitude = 1.23,
            Longitude = 6.56,
            Building = buildingToCreate,
            BuildingId = buildingToCreate.Id
        };

        buildingToCreate.Location = location;

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

    [TestMethod]
    public void UpdateBuilding_BuildingIsUpdated()
    {
        ConstructionCompany constructionCompanyInDb = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "Construction Company 1",
        };

        Manager managerToUpdate = new Manager
        {
            Id = Guid.NewGuid(),
            Firstname = "managerToUpdFirstname",
            Email = "managerToUpd@gmail.com",
            Password = "managerToUpdPassword",
            Buildings = new List<Building>(),
            Requests = new List<MaintenanceRequest>(),
            Role = SystemUserRoleEnum.Manager
        };
        
        _dbContext.Set<Manager>().Add(managerToUpdate);
        _dbContext.Set<ConstructionCompany>().Add(constructionCompanyInDb);

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
            ConstructionCompany = constructionCompanyInDb,
            Manager = new Manager
            {
                Id = Guid.NewGuid(),
                Firstname = "Manager 1",
                Email = "manager@gmail.com",
                Password = "Password",
                Role =  SystemUserRoleEnum.Manager,
                Buildings = new List<Building>(),
                Requests = new List<MaintenanceRequest>()
            },

            Flats = new List<Flat>
            {
                new Flat
                {
                    Id = Guid.NewGuid(),
                    Floor = 1,
                    RoomNumber = "101",
                    OwnerAssigned = new Owner
                    {
                        Id = Guid.NewGuid(),
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

        Building buildingWithUpdates = new Building
        {
            Id = buildingInDb.Id,
            Name = "Building 1",
            Address = "Address 1",
            Location = buildingInDb.Location,
            CommonExpenses = 200,
            ConstructionCompanyId = constructionCompanyInDb.Id,
            ConstructionCompany = constructionCompanyInDb,
            Manager = managerToUpdate,
            ManagerId = managerToUpdate.Id,
            Flats = buildingInDb.Flats
        };

        _buildingRepository.UpdateBuilding(buildingWithUpdates);
        Building buildingInDbUpdated = _dbContext.Set<Building>().Find(buildingInDb.Id);
        Assert.IsTrue(buildingWithUpdates.Equals(buildingInDbUpdated));
    }

    [TestMethod]
    public void UpdateBuilding_ThrowsUnknownException()
    {
        Mock<DbContext> dbContextMock = new Mock<DbContext>();
        dbContextMock.Setup(dbContext => dbContext.Set<Building>()).Throws(new Exception("Unknown exception"));

        BuildingRepository buildingRepository = new BuildingRepository(dbContextMock.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => buildingRepository.UpdateBuilding(new Building()));
    }

    [TestMethod]
    public void DeleteBuilding_DeletesBuilding()
    {
        Building buildingToDelete = new Building
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
            Manager = new Manager
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.Manager,
                Firstname = "Manager 1",
                Email = "manager@gmail.com",
                Password = "Password"
            },
            Flats = new List<Flat>
            {
                new Flat
                {
                    Id = Guid.NewGuid(),
                    Floor = 1,
                    RoomNumber = "101",
                    OwnerAssigned = new Owner
                    {
                        Id = Guid.NewGuid(),
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

        _dbContext.Set<Building>().Add(buildingToDelete);
        _dbContext.SaveChanges();

        _buildingRepository.DeleteBuilding(buildingToDelete);
        Building buildingInDb = _dbContext.Set<Building>().Find(buildingToDelete.Id);
        Assert.IsNull(buildingInDb);
    }

    [TestMethod]
    public void DeleteBuilding_ThrowsUnknownException()
    {
        Mock<DbContext> dbContextMock = new Mock<DbContext>();
        dbContextMock.Setup(dbContext => dbContext.Set<Building>()).Throws(new Exception("Unknown exception"));

        BuildingRepository buildingRepository = new BuildingRepository(dbContextMock.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => buildingRepository.DeleteBuilding(new Building()));
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Database.EnsureDeleted();
    }
}