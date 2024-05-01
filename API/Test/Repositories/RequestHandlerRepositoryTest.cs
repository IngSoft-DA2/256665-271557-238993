using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories.CustomExceptions;

namespace Test.Repositories;

[TestClass]
public class RequestHandlerRepositoryTest
{
    #region Initializing Aspects
    
    private DbContext _dbContext;
    private RequestHandlerRepository _requestHandlerRepository;

    [TestInitialize]
    public void TestInitialize()
    {
        _dbContext = CreateDbContext("RequestHandlerRepositoryTest");
        _dbContext.Set<RequestHandler>();
        _requestHandlerRepository = new RequestHandlerRepository(_dbContext);
    }

    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
    }
    
    #endregion
    
    #region Create Category

    [TestMethod]
    public void Create_CategoryIsAdded()
    {
        RequestHandler requestHandlerToAdd = new RequestHandler()
        {
            Id = Guid.NewGuid(),
            Firstname = "RequestHandler1"
        };

        _requestHandlerRepository.CreateRequestHandler(requestHandlerToAdd);
        RequestHandler requestHandlerInDb = _dbContext.Set<RequestHandler>().Find(requestHandlerToAdd.Id);

        Assert.AreEqual(requestHandlerToAdd, requestHandlerInDb);
    }

    [TestMethod]
    public void Create_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<RequestHandler>()).Throws(new Exception());

        _requestHandlerRepository = new RequestHandlerRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() =>
            _requestHandlerRepository.CreateRequestHandler(new RequestHandler()));
        _mockDbContext.VerifyAll();
    }
    
    #endregion
    
    #region Get All Categories
    
    [TestMethod]
    public void GetAllRequestHandlers_RequestHandlersAreReturn()
    {
        RequestHandler requestHandlerInDb = new RequestHandler
        {
            Id = Guid.NewGuid(),
            Firstname = "RequestHandler1",
            Password = "3423423ewrwr4",
            Email = "person@gmail.com",
            LastName = "LastName",
            Role = "RequestHandler",
        };
        RequestHandler requestHandlerInDb2 = new RequestHandler
        {
            Id = Guid.NewGuid(),
            Firstname = "RequestHandler2",
            Password = "34234wer23234",
            Email = "person@gmail.com",
            LastName = "LastName2",
            Role = "RequestHandler"
        };
        
        IEnumerable<RequestHandler> expectedRequestHandlers = new List<RequestHandler> {requestHandlerInDb, requestHandlerInDb2};

        _dbContext.Set<RequestHandler>().Add(requestHandlerInDb);
        _dbContext.Set<RequestHandler>().Add(requestHandlerInDb2);
        _dbContext.SaveChanges();
        
        IEnumerable<RequestHandler> requestHandlersResponse = _requestHandlerRepository.GetAllRequestHandlers();
        
        Assert.IsTrue(expectedRequestHandlers.SequenceEqual(requestHandlersResponse));
    }
    
    [TestMethod]
    public void GetAllRequestHandlers_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<RequestHandler>()).Throws(new Exception());
        
        _requestHandlerRepository = new RequestHandlerRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _requestHandlerRepository.GetAllRequestHandlers());
        _mockDbContext.VerifyAll();
    }
    
    #endregion
    
}