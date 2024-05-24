using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Test.Repositories;

[TestClass]
public class SessionRepositoryTest
{
    #region Initializing Aspects

    private DbContext _dbContext;
    private SessionRepository _sessionRepository;

    [TestInitialize]
    public void TestInitialize()
    {
        _dbContext = CreateDbContext("sessionRepositoryTest");
        _dbContext.Set<Session>();
        _sessionRepository = new SessionRepository(_dbContext);
    }

    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
    }

    #endregion


    
    
    

    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Database.EnsureDeleted();
    }
}