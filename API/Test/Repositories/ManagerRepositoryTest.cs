using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
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
                Buildings = new List<Building>()
            },
            new Manager
            {
                Id = Guid.NewGuid(),
                Firstname = "Manager2",
                Email = "manager2@gmail.com",
                Password = "password2",
                Buildings = new List<Building>()
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
                            RoomNumber = 101,
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
}