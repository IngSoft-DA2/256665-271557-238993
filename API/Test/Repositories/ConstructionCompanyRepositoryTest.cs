using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories.CustomExceptions;

namespace Test.Repositories;

[TestClass]
public class ConstructionCompanyRepositoryTest
{
    private DbContext _dbContext;
    private ConstructionCompanyRepository _constructionCompanyRepository;

    [TestInitialize]
    public void TestInitialize()
    {
        _dbContext = CreateDbContext("ConstructionCompanyRepositoryTest");
        _dbContext.Set<ConstructionCompany>();
        _constructionCompanyRepository = new ConstructionCompanyRepository(_dbContext);
    }

    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
    }

    [TestMethod]
    public void GetAllConstructionCompanies_ConstructionCompaniesAreReturn()
    {
        ConstructionCompany constructionCompanyInDb = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "ConstructionCompany1"
        };
        ConstructionCompany constructionCompanyInDb2 = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "ConstructionCompany2"
        };

        IEnumerable<ConstructionCompany> expectedConstructionCompanies = new List<ConstructionCompany>
            { constructionCompanyInDb, constructionCompanyInDb2 };

        _dbContext.Set<ConstructionCompany>().Add(constructionCompanyInDb);
        _dbContext.Set<ConstructionCompany>().Add(constructionCompanyInDb2);
        _dbContext.SaveChanges();

        IEnumerable<ConstructionCompany> constructionCompaniesResponse =
            _constructionCompanyRepository.GetAllConstructionCompanies();

        Assert.IsTrue(expectedConstructionCompanies.SequenceEqual(constructionCompaniesResponse));
    }

    [TestMethod]
    public void GetAllConstructionCompanies_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<ConstructionCompany>()).Throws(new Exception());

        ConstructionCompanyRepository constructionCompanyRepository =
            new ConstructionCompanyRepository(_mockDbContext.Object);

        Assert.ThrowsException<UnknownRepositoryException>(() =>
            constructionCompanyRepository.GetAllConstructionCompanies());
    }

    [TestMethod]
    public void GetConstructionCompanyById_ConstructionCompanyIsReturn()
    {
        ConstructionCompany constructionCompanyInDb = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "ConstructionCompany1"
        };

        _dbContext.Set<ConstructionCompany>().Add(constructionCompanyInDb);
        _dbContext.SaveChanges();

        ConstructionCompany constructionCompanyResponse =
            _constructionCompanyRepository.GetConstructionCompanyById(constructionCompanyInDb.Id);

        Assert.AreEqual(constructionCompanyInDb, constructionCompanyResponse);
    }

    [TestMethod]
    public void GetConstructionCompanyById_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<ConstructionCompany>()).Throws(new Exception());

        ConstructionCompanyRepository constructionCompanyRepository =
            new ConstructionCompanyRepository(_mockDbContext.Object);

        Assert.ThrowsException<UnknownRepositoryException>(() =>
            constructionCompanyRepository.GetConstructionCompanyById(Guid.NewGuid()));
    }

    [TestMethod]
    public void GetConstructionCompanyByUserCreatorId_ConstructionCompanyIsReturn()
    {
        ConstructionCompany constructionCompanyInDb = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "ConstructionCompany1",
            UserCreatorId = Guid.NewGuid(),
            Buildings = new List<Building>()
        };
        _dbContext.Set<ConstructionCompany>().Add(constructionCompanyInDb);
        _dbContext.SaveChanges();
        
        ConstructionCompany constructionCompanyResponse =
            _constructionCompanyRepository.GetConstructionCompanyByUserCreatorId(constructionCompanyInDb.UserCreatorId);
        
        Assert.AreEqual(constructionCompanyInDb, constructionCompanyResponse);
    }
    
    [TestMethod]
    public void GetConstructionCompanyByUserCreatorId_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<ConstructionCompany>()).Throws(new Exception());

        ConstructionCompanyRepository constructionCompanyRepository =
            new ConstructionCompanyRepository(_mockDbContext.Object);

        Assert.ThrowsException<UnknownRepositoryException>(() =>
            constructionCompanyRepository.GetConstructionCompanyByUserCreatorId(Guid.NewGuid()));
    }

    [TestMethod]
    public void CreateConstructionCompany_ConstructionCompanyIsCreated()
    {
        ConstructionCompany constructionCompanyToAdd = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "ConstructionCompany1"
        };

        _constructionCompanyRepository.CreateConstructionCompany(constructionCompanyToAdd);

        ConstructionCompany constructionCompanyResponse =
            _constructionCompanyRepository.GetConstructionCompanyById(constructionCompanyToAdd.Id);

        Assert.AreEqual(constructionCompanyToAdd, constructionCompanyResponse);
    }

    [TestMethod]
    public void CreateConstructionCompany_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<ConstructionCompany>()).Throws(new Exception());

        ConstructionCompanyRepository constructionCompanyRepository =
            new ConstructionCompanyRepository(_mockDbContext.Object);

        Assert.ThrowsException<UnknownRepositoryException>(() =>
            constructionCompanyRepository.CreateConstructionCompany(new ConstructionCompany()));
    }


    [TestMethod]
    public void UpdateConstructionCompany_ConstructionCompanyIsUpdated()
    {
        ConstructionCompany constructionCompanyWithoutUpdates = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "Construction Company name without update",
            Buildings = new List<Building>(),
            UserCreatorId = Guid.NewGuid()
        };

        _dbContext.Set<ConstructionCompany>().Add(constructionCompanyWithoutUpdates);
        _dbContext.SaveChanges();

        ConstructionCompany constructionCompanyWithUpdates = new ConstructionCompany
        {
            Id = constructionCompanyWithoutUpdates.Id,
            Name = "Construction Company name with update",
            Buildings = new List<Building>(),
            UserCreatorId = constructionCompanyWithoutUpdates.UserCreatorId
        };

        _constructionCompanyRepository.UpdateConstructionCompany(constructionCompanyWithUpdates);

        ConstructionCompany constructionCompanyInDbUpdated =
            _constructionCompanyRepository.GetConstructionCompanyById(constructionCompanyWithoutUpdates.Id);

        Assert.AreEqual(constructionCompanyWithUpdates, constructionCompanyInDbUpdated);
    }

    [TestMethod]
    public void UpdateConstructionCompany_UnknownExceptionisThrown()
    {
        ConstructionCompany constructionCompanyWithoutUpdates = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "Construction Company name without update",
            Buildings = new List<Building>(),
            UserCreatorId = Guid.NewGuid()
        };

        _dbContext.Set<ConstructionCompany>().Add(constructionCompanyWithoutUpdates);
        _dbContext.SaveChanges();
        
        ConstructionCompany constructionCompanyWithUpdates = new ConstructionCompany
        {
            Id = constructionCompanyWithoutUpdates.Id,
            Name = "Construction Company name with update",
            Buildings = new List<Building>(),
            UserCreatorId = constructionCompanyWithoutUpdates.UserCreatorId
        };
        
        
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<ConstructionCompany>()).Throws(new Exception());

        ConstructionCompanyRepository constructionCompanyRepository =
            new ConstructionCompanyRepository(_mockDbContext.Object);

        Assert.ThrowsException<UnknownRepositoryException>(() =>
            constructionCompanyRepository.UpdateConstructionCompany(constructionCompanyWithUpdates));
        
        _mockDbContext.VerifyAll();
    }


    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Database.EnsureDeleted();
    }
}