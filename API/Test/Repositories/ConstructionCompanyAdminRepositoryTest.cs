using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

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

        ConstructionCompanyAdmin constructionCompanyAdminFoundInDb = _dbContext.Set<ConstructionCompanyAdmin>().Find(constructionCompanyAdmin.Id);
        Assert.IsNotNull(constructionCompanyAdminFoundInDb);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Database.EnsureDeleted();
    }
}