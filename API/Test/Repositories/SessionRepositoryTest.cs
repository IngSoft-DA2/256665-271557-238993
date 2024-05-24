﻿using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories.CustomExceptions;

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


    [TestMethod]
    public void CreateSession_SessionIsCreated()
    {
        var sessionToBeAdded = new Session()
        {
            UserId = Guid.NewGuid(),
            SessionString = Guid.NewGuid(),
            UserRole = "Admin"
        };
        
        _sessionRepository.CreateSession(sessionToBeAdded);

        Assert.AreEqual(1, _dbContext.Set<Session>().Count());
    }
    
    [TestMethod]
    public void CreateSession_ThrowsUnknownException()
    {
        Mock<DbContext> dbContextMock = new Mock<DbContext>();
        dbContextMock.Setup(dbContext => dbContext.Set<Session>()).Throws(new Exception("Unknown exception"));
        
        var sessionToBeAdded = new Session()
        {
            UserId = Guid.NewGuid(),
            SessionString = Guid.NewGuid(),
            UserRole = "Admin"
        };
        
        SessionRepository sessionRepository = new SessionRepository(dbContextMock.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => sessionRepository.CreateSession(sessionToBeAdded));
    }
    

    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Database.EnsureDeleted();
    }
}