using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

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
        RequestHandler categoryInDb = _dbContext.Set<RequestHandler>().Find(requestHandlerToAdd.Id);
        
        Assert.AreEqual(requestHandlerToAdd, categoryInDb);
    }
    
}