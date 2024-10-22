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

        IEnumerable<ConstructionCompany> expectedConstructionCompanies = new List<ConstructionCompany> { constructionCompanyInDb, constructionCompanyInDb2 };

        _dbContext.Set<ConstructionCompany>().Add(constructionCompanyInDb);
        _dbContext.Set<ConstructionCompany>().Add(constructionCompanyInDb2);
        _dbContext.SaveChanges();

        IEnumerable<ConstructionCompany> constructionCompaniesResponse = _constructionCompanyRepository.GetAllConstructionCompanies();

        Assert.IsTrue(expectedConstructionCompanies.SequenceEqual(constructionCompaniesResponse));
    }

    [TestMethod]
    public void GetAllConstructionCompanies_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<ConstructionCompany>()).Throws(new Exception());

        ConstructionCompanyRepository constructionCompanyRepository = new ConstructionCompanyRepository(_mockDbContext.Object);

        Assert.ThrowsException<UnknownRepositoryException>(() => constructionCompanyRepository.GetAllConstructionCompanies());
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

        ConstructionCompany constructionCompanyResponse = _constructionCompanyRepository.GetConstructionCompanyById(constructionCompanyInDb.Id);

        Assert.AreEqual(constructionCompanyInDb, constructionCompanyResponse);
    }

    [TestMethod]
    public void GetConstructionCompanyById_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<ConstructionCompany>()).Throws(new Exception());

        ConstructionCompanyRepository constructionCompanyRepository = new ConstructionCompanyRepository(_mockDbContext.Object);

        Assert.ThrowsException<UnknownRepositoryException>(() => constructionCompanyRepository.GetConstructionCompanyById(Guid.NewGuid()));
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

        ConstructionCompany constructionCompanyResponse = _constructionCompanyRepository.GetConstructionCompanyById(constructionCompanyToAdd.Id);

        Assert.AreEqual(constructionCompanyToAdd, constructionCompanyResponse);
    }

    [TestMethod]
    public void CreateConstructionCompany_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<ConstructionCompany>()).Throws(new Exception());

        ConstructionCompanyRepository constructionCompanyRepository = new ConstructionCompanyRepository(_mockDbContext.Object);

        Assert.ThrowsException<UnknownRepositoryException>(() => constructionCompanyRepository.CreateConstructionCompany(new ConstructionCompany()));
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Database.EnsureDeleted();
    }

}