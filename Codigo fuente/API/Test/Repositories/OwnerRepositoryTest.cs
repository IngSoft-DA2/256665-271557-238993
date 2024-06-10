using System.Collections;
using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories.CustomExceptions;

namespace Test.Repositories;

[TestClass]
public class OwnerRepositoryTest
{
    private DbContext _dbContext;
    private OwnerRepository _ownerRepository;

    [TestInitialize]
    public void TestInitialize()
    {
        _dbContext = CreateDbContext("OwnerRepositoryTest");
        _dbContext.Set<Category>();
        _ownerRepository = new OwnerRepository(_dbContext);
    }

    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
    }

    [TestMethod]
    public void GetAllOwners_OwnersAreReturn()
    {
        Owner ownerInDb = new Owner
        {
            Id = Guid.NewGuid(),
            Firstname = "Owner1",
            Lastname = "Owner1",
            Email = "owner@gmail.com",
            Flats = new List<Flat>
            {
                new Flat
                {
                    Id = Guid.NewGuid(),
                    BuildingId = Guid.NewGuid(),
                    Floor = 1,
                    RoomNumber = "101",
                    TotalRooms = 4,
                    TotalBaths = 2,
                    HasTerrace = true
                }
            }
        };

        Owner ownerInDb2 = new Owner
        {
            Id = Guid.NewGuid(),
            Firstname = "Owner2",
            Lastname = "Owner2",
            Email = "owner2@gmail.com",
            Flats = new List<Flat>
            {
                new Flat
                {
                    Id = Guid.NewGuid(),
                    BuildingId = Guid.NewGuid(),
                    Floor = 2,
                    RoomNumber = "201",
                    TotalRooms = 3,
                    TotalBaths = 1,
                    HasTerrace = false
                }
            }
        };

        _dbContext.Set<Owner>().Add(ownerInDb);
        _dbContext.Set<Owner>().Add(ownerInDb2);
        _dbContext.SaveChanges();

        IEnumerable<Owner> expectedOwners = new List<Owner> { ownerInDb, ownerInDb2 };
        IEnumerable<Owner> ownersResponse = _ownerRepository.GetAllOwners();

        Assert.IsTrue(expectedOwners.SequenceEqual(ownersResponse));
    }

    [TestMethod]
    public void GetAllOwners_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<Owner>()).Throws(new Exception());

        _ownerRepository = new OwnerRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _ownerRepository.GetAllOwners());
        _mockDbContext.VerifyAll();
    }

    [TestMethod]
    public void GetOwnerById_OwnerIsReturn()
    {
        Owner ownerInDb = new Owner
        {
            Id = Guid.NewGuid(),
            Firstname = "Owner1",
            Lastname = "Owner1",
            Email = "owner@gmail.com",
            Flats = new List<Flat>
            {
                new Flat
                {
                    Id = Guid.NewGuid(),
                    BuildingId = Guid.NewGuid(),
                    Floor = 1,
                    RoomNumber = "101",
                    TotalRooms = 4,
                    TotalBaths = 2,
                    HasTerrace = true
                },
                new Flat
                {
                    Id = Guid.NewGuid(),
                    BuildingId = Guid.NewGuid(),
                    Floor = 2,
                    RoomNumber = "201",
                    TotalRooms = 3,
                    TotalBaths = 1,
                    HasTerrace = false
                }
            }
        };

        _dbContext.Set<Owner>().Add(ownerInDb);
        _dbContext.SaveChanges();

        Owner ownerReturn = _ownerRepository.GetOwnerById(ownerInDb.Id);
        Assert.AreEqual(ownerInDb, ownerReturn);
    }

    [TestMethod]
    public void GetOwnerById_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<Owner>()).Throws(new Exception());

        _ownerRepository = new OwnerRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _ownerRepository.GetOwnerById(Guid.NewGuid()));
        _mockDbContext.VerifyAll();
    }

    [TestMethod]
    public void CreateOwner_OwnerIsCreated()
    {
        Owner ownerToCreate = new Owner
        {
            Id = Guid.NewGuid(),
            Firstname = "Owner1",
            Lastname = "Owner1",
            Email = "owner@gmail.com"
        };

        _ownerRepository.CreateOwner(ownerToCreate);

        Owner ownerReturn = _ownerRepository.GetOwnerById(ownerToCreate.Id);
        Assert.AreEqual(ownerToCreate, ownerReturn);
    }

    [TestMethod]
    public void CreateOwner_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<Owner>()).Throws(new Exception());

        _ownerRepository = new OwnerRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _ownerRepository.CreateOwner(new Owner()));
        _mockDbContext.VerifyAll();
    }

    [TestMethod]
    public void GetOwnerById_OwnerIsUpdated()
    {
        Owner ownerInDbWithoutUpd = new Owner
        {
            Id = Guid.NewGuid(),
            Firstname = "Owner1",
            Lastname = "Owner1",
            Email = "owner@gmail.com"
        };

        Owner ownerWithUpds = new Owner
        {
            Id = ownerInDbWithoutUpd.Id,
            Firstname = "Owner2",
            Lastname = "Owner2",
            Email = "ownerrr@gmail.com"
        };
        _dbContext.Set<Owner>().Add(ownerInDbWithoutUpd);
        _dbContext.SaveChanges();

        _ownerRepository.UpdateOwnerById(ownerWithUpds);
        Owner ownerReturn = _ownerRepository.GetOwnerById(ownerInDbWithoutUpd.Id);
        Assert.AreEqual(ownerWithUpds, ownerReturn);
    }

    [TestMethod]
    public void GetOwnerById_ThrowsUnknownExceptionUpdate()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<Owner>()).Throws(new Exception());

        _ownerRepository = new OwnerRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _ownerRepository.UpdateOwnerById(new Owner()));
        _mockDbContext.VerifyAll();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Database.EnsureDeleted();
    }
}