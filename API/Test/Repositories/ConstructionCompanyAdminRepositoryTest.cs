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
    #region Initializr

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

    #endregion


    #region Create Construction Company Admin

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

        ConstructionCompanyAdminRepository constructionCompanyAdminRepository =
            new ConstructionCompanyAdminRepository(_mockDbContext.Object);

        Assert.ThrowsException<UnknownRepositoryException>(() =>
            constructionCompanyAdminRepository.CreateConstructionCompanyAdmin(It.IsAny<ConstructionCompanyAdmin>()));

        _mockDbContext.VerifyAll();
    }

    #endregion

    #region Get All Construction Company Admins

    [TestMethod]
    public void GetAllConstructionCompanyAdmins_AllConstructionCompanyAdminsAreReturn()
    {
        ConstructionCompanyAdmin constructionCompanyAdmin1 = new ConstructionCompanyAdmin
        {
            Id = Guid.NewGuid(),
            Firstname = "constructionCompanyAdminFirstname1",
            Lastname = "constructionCompanyAdminLastname1",
            Email = "constructionCompanyAdminEmail1",
            Password = "constructionCompanyAdminPassword1",
            ConstructionCompany = null,
            Role = SystemUserRoleEnum.ConstructionCompanyAdmin
        };

        ConstructionCompanyAdmin constructionCompanyAdmin2 = new ConstructionCompanyAdmin
        {
            Id = Guid.NewGuid(),
            Firstname = "constructionCompanyAdminFirstname2",
            Lastname = "constructionCompanyAdminLastname2",
            Email = "constructionCompanyAdminEmail2",
            Password = "constructionCompanyAdminPassword2",
            ConstructionCompany = null,
            Role = SystemUserRoleEnum.ConstructionCompanyAdmin
        };

        _dbContext.Set<ConstructionCompanyAdmin>().Add(constructionCompanyAdmin1);
        _dbContext.Set<ConstructionCompanyAdmin>().Add(constructionCompanyAdmin2);
        _dbContext.SaveChanges();

        IEnumerable<ConstructionCompanyAdmin> constructionCompanyAdmins =
            _constructionCompanyAdminRepository.GetAllConstructionCompanyAdmins();

        Assert.AreEqual(2, constructionCompanyAdmins.Count());
        
        Assert.IsTrue(constructionCompanyAdmins.First().Equals(constructionCompanyAdmin1));
        Assert.IsTrue(constructionCompanyAdmins.Last().Equals(constructionCompanyAdmin2));
    }
    
    [TestMethod]
    public void GetAllConstructionCompanyAdmins_ThrowsUnknownRepositoryException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(mockDbContext => mockDbContext.Set<ConstructionCompanyAdmin>()).Throws(new Exception());

        ConstructionCompanyAdminRepository constructionCompanyAdminRepository =
            new ConstructionCompanyAdminRepository(_mockDbContext.Object);

        Assert.ThrowsException<UnknownRepositoryException>(() =>
            constructionCompanyAdminRepository.GetAllConstructionCompanyAdmins());

        _mockDbContext.VerifyAll();
    }
    
    

    #endregion

    #region Cleanup

    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Database.EnsureDeleted();
    }

    #endregion
}