using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories.CustomExceptions;

namespace Test.Repositories;

[TestClass]
public class ConstructionCompanyAdminRepositoryTest
{
    private DbContext _dbContext;
    private ConstructionCompanyAdminRepository _constructionCompanyAdminRepository;

    [TestInitialize]
    public void TestInitialize()
    {
        _dbContext = CreateDbContext("constructionCompanyAdminRepositoryTest");
        _dbContext.Set<ConstructionCompanyAdmin>();
        _constructionCompanyAdminRepository = new ConstructionCompanyAdminRepository(_dbContext);
    }

    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
    }


    [TestMethod]
    public void CreateConstructionCompanyAdmin_ConstructionCompanyAdminIsAddedToDb()
    {
        ConstructionCompanyAdmin constructionCompanyAdmin = new ConstructionCompanyAdmin
        {
            Id = Guid.NewGuid(),
            Firstname = "constructionCompanyAdminFirstname",
            Lastname = "constructionCompanyAdminLastname",
            Email = "constructionCompanyAdminEmail",
            Password = "constructionCompanyAdminPassword",
            ConstructionCompany = null,
            Role = SystemUserRoleEnum.ConstructionCompanyAdmin
        };

        _constructionCompanyAdminRepository.CreateConstructionCompanyAdmin(constructionCompanyAdmin);

        ConstructionCompanyAdmin constructionCompanyAdminFoundInDb =
            _dbContext.Set<ConstructionCompanyAdmin>().Find(constructionCompanyAdmin.Id);
        Assert.IsNotNull(constructionCompanyAdminFoundInDb);
    }

    [TestMethod]
    public void CreateConstructionCompanyAdmin_ThrowsUnknownRepositoryException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(mockDbContext => mockDbContext.Set<ConstructionCompanyAdmin>()).Throws(new Exception());

        ConstructionCompanyAdminRepository constructionCompanyAdminRepository = new ConstructionCompanyAdminRepository(_mockDbContext.Object);
        
        Assert.ThrowsException<UnknownRepositoryException>(() =>
            constructionCompanyAdminRepository.CreateConstructionCompanyAdmin(It.IsAny<ConstructionCompanyAdmin>()));
        
        _mockDbContext.VerifyAll();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Database.EnsureDeleted();
    }
}