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
}