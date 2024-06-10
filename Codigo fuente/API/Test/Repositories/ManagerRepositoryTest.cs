using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories.CustomExceptions;

namespace Test.Repositories;

[TestClass]
public class ManagerRepositoryTest
{
    private DbContext _dbContext;
    private ManagerRepository _managerRepository;

    [TestInitialize]
    public void TestInitialize()
    {
        _dbContext = CreateDbContext("ManagerRepositoryTest");
        _dbContext.Set<Category>();
        _managerRepository = new ManagerRepository(_dbContext);
    }

    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
    }

    [TestMethod]
    public void GetAllManagers_ManagersAreReturn()
    {
        IEnumerable<Manager> managersInDb = new List<Manager>
        {
            new Manager
            {
                Id = Guid.NewGuid(),
                Firstname = "Manager1",
                Email = "manager1@gmail.com",
                Password = "password1",
                Role = SystemUserRoleEnum.Manager,
                Buildings = new List<Building>(),
                Requests = new List<MaintenanceRequest>()
            },
            new Manager
            {
                Id = Guid.NewGuid(),
                Firstname = "Manager2",
                Email = "manager2@gmail.com",
                Password = "password2",
                Role = SystemUserRoleEnum.Manager,
                Buildings = new List<Building>(),
                Requests = new List<MaintenanceRequest>()
            }
        };

        _dbContext.Set<Manager>().Add(managersInDb.ElementAt(0));
        _dbContext.Set<Manager>().Add(managersInDb.ElementAt(1));
        _dbContext.SaveChanges();

        IEnumerable<Manager> managersReturn = _managerRepository.GetAllManagers();
        Assert.IsTrue(managersInDb.SequenceEqual(managersReturn));
    }

    [TestMethod]
    public void GetAllManagers_ThrowsUnknownException()
    {

        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<Manager>()).Throws(new Exception());

        _managerRepository = new ManagerRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _managerRepository.GetAllManagers());
        _mockDbContext.VerifyAll();
    }

    [TestMethod]
    public void GetManagerById_ReturnsManager()
    {
        Manager managerInDb = new Manager
        {
            Id = Guid.NewGuid(),
            Role = SystemUserRoleEnum.Manager,
            Firstname = "Manager1",
            Email = "manager@gmail.com",
            Password = "managerPassword",
            Buildings = new List<Building>
            {
                new Building
                {
                    Id = Guid.NewGuid(),
                    Name = "Building1",
                    Address = "Address1",
                    Location = new Location
                    {
                        Id = Guid.NewGuid(),
                        Latitude = 1,
                        Longitude = 1
                    },
                    ConstructionCompany = new ConstructionCompany
                    {
                        Id = Guid.NewGuid(),
                        Name = "ConstructionCompany1",
                    },
                    CommonExpenses = 100,
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
                                Firstname = "Owner1",
                                Lastname = "Owner1",
                                Email = "owner@gmail,com",
                            },
                            TotalRooms = 4,
                            TotalBaths = 2,
                            HasTerrace = true
                        }
                    }
                }
            }
        };
        _dbContext.Set<Manager>().Add(managerInDb);
        _dbContext.SaveChanges();

        Manager managerReturn = _managerRepository.GetManagerById(managerInDb.Id);
        Assert.IsTrue(managerInDb.Equals(managerReturn));
    }

    [TestMethod]
    public void GetManagerById_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<Manager>()).Throws(new Exception());

        _managerRepository = new ManagerRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _managerRepository.GetManagerById(Guid.NewGuid()));
        _mockDbContext.VerifyAll();
    }

    [TestMethod]
    public void CreateManager_ReturnsManager()
    {
        Manager managerToCreate = new Manager
        {
            Id = Guid.NewGuid(),
            Role = SystemUserRoleEnum.Manager,
            Firstname = "Manager1",
            Email = "manager@gmail.com",
            Password = "managerPassword",
        };

        _managerRepository.CreateManager(managerToCreate);
        Manager managerInDb = _dbContext.Set<Manager>().Find(managerToCreate.Id);
        Assert.IsTrue(managerToCreate.Equals(managerInDb));
    }

    [TestMethod]
    public void CreateManager_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<Manager>()).Throws(new Exception());

        _managerRepository = new ManagerRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _managerRepository.CreateManager(new Manager()));
        _mockDbContext.VerifyAll();
    }

    [TestMethod]
    public void DeleteManager_ManagerIsDeleted()
    {
        Manager managerToDelete = new Manager
        {
            Id = Guid.NewGuid(),
            Role = SystemUserRoleEnum.Manager,
            Firstname = "Manager1",
            Email = "manager@gmail.com",
            Password = "managerPassword",
            Buildings = new List<Building>
            {
                new Building
                {
                    Id = Guid.NewGuid(),
                    Name = "Building1",
                    Address = "Address1",
                    Location = new Location
                    {
                        Id = Guid.NewGuid(),
                        Latitude = 1,
                        Longitude = 1
                    },
                    ConstructionCompany = new ConstructionCompany
                    {
                        Id = Guid.NewGuid(),
                        Name = "ConstructionCompany1",
                    },
                    CommonExpenses = 100,
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
                                Firstname = "Owner1",
                                Lastname = "Owner1",
                                Email = "owner@gmail,com",
                            },
                            TotalRooms = 4,
                            TotalBaths = 2,
                            HasTerrace = true
                        }
                    }
                }
            }
        };

        _dbContext.Set<Manager>().Add(managerToDelete);
        _dbContext.SaveChanges();

        _managerRepository.DeleteManager(managerToDelete);
        Manager managerInDb = _dbContext.Set<Manager>().Find(managerToDelete.Id);
        Assert.IsNull(managerInDb);
    }
    [TestMethod]
    public void DeleteManager_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<Manager>()).Throws(new Exception());

        _managerRepository = new ManagerRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _managerRepository.DeleteManager(new Manager()));
        _mockDbContext.VerifyAll();
    }


    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Database.EnsureDeleted();
    }

}