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
        _dbContext = CreateDbContext("CategoryRepositoryTest");
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
        };
        Owner ownerInDb2 = new Owner
        {
            Id = Guid.NewGuid(),
            Firstname = "Owner2",
            Lastname = "Owner2",
            Email = "owner2@gmail.com",
        };

        IEnumerable<Flat> flatsofOwner1 = new List<Flat>
        {
            new Flat
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                Floor = 1,
                RoomNumber = 101,
                OwnerAssigned = ownerInDb,
                TotalRooms = 4,
                TotalBaths = 2,
                HasTerrace = true
            },
            new Flat
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                Floor = 2,
                RoomNumber = 201,
                OwnerAssigned = ownerInDb,
                TotalRooms = 3,
                TotalBaths = 1,
                HasTerrace = false
            }
        };

        IEnumerable<Flat> flatsofOwner2 = new List<Flat>
        {
            new Flat
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                Floor = 1,
                RoomNumber = 101,
                OwnerAssigned = ownerInDb2,
                TotalRooms = 4,
                TotalBaths = 2,
                HasTerrace = true
            }
        };
        
        ownerInDb.Flats = flatsofOwner1;
        ownerInDb2.Flats = flatsofOwner2;
        
        IEnumerable<Owner> expectedOwners = new List<Owner> {ownerInDb,ownerInDb2};

        _dbContext.Set<Owner>().Add(ownerInDb);
        _dbContext.Set<Owner>().Add(ownerInDb2);
        _dbContext.SaveChanges();
        
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
            Email = "owner@gmail.com"
        };
        
        IEnumerable<Flat> flatsofOwner1 = new List<Flat>
        {
            new Flat
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                Floor = 1,
                RoomNumber = 101,
                OwnerAssigned = ownerInDb,
                TotalRooms = 4,
                TotalBaths = 2,
                HasTerrace = true
            },
            new Flat
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                Floor = 2,
                RoomNumber = 201,
                OwnerAssigned = ownerInDb,
                TotalRooms = 3,
                TotalBaths = 1,
                HasTerrace = false
            }
        };
        
        ownerInDb.Flats = flatsofOwner1;

        _dbContext.Set<Owner>().Add(ownerInDb);
        _dbContext.SaveChanges();
        
        Owner ownerReturn = _ownerRepository.GetOwnerById(ownerInDb.Id);    
        Assert.AreEqual(ownerInDb, ownerReturn);
    }
    
}