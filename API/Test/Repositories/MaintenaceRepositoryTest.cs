using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Moq;
using Repositories.CustomExceptions;

namespace Test.Repositories;

[TestClass]

public class MaintenaceRepositoryTest
{
    
    private DbContext _dbContext;
    private MaintenanceRequestRepository _maintenanceRequestRepository;
    private MaintenanceRequest _maintenanceRequestInDb;
    private MaintenanceRequest _maintenanceRequestInDb2;
    

    [TestInitialize]
    public void TestInitialize()
    {
        _dbContext = CreateDbContext("MaintenanceRepositoryTest");
        _dbContext.Set<MaintenanceRequest>();
        _maintenanceRequestRepository = new MaintenanceRequestRepository(_dbContext);
        
        _maintenanceRequestInDb = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            FlatId = Guid.Empty,
            BuildingId = Guid.Empty,
            ClosedDate = DateTime.Now.AddDays(1),
            OpenedDate = DateTime.Now,
            RequestHandlerId = Guid.Empty,
            RequestStatus = StatusEnum.Rejected,
            Category = Guid.NewGuid(),
            Description = "Bath broken",
        };
        
        _maintenanceRequestInDb2 = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            FlatId = Guid.Empty,
            BuildingId = Guid.Empty,
            ClosedDate = DateTime.Now.AddDays(3),
            OpenedDate = DateTime.Now,
            RequestHandlerId = Guid.Empty,
            RequestStatus = StatusEnum.Rejected,
            Category = Guid.NewGuid(),
            Description = "Room broken",
        };
    }
    
    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
    }
    
    [TestMethod]
    public void GetAllMaintenanceRequests_MaintenanceRequestsAreReturn()
    {
        
        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests = new List<MaintenanceRequest> {_maintenanceRequestInDb, _maintenanceRequestInDb2};

        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb);
        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb2);
        _dbContext.SaveChanges();
        
        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse = _maintenanceRequestRepository.GetAllMaintenanceRequests();
        
        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }
    
    [TestMethod]
    public void GetAllMaintenanceRequests_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<MaintenanceRequest>()).Throws(new Exception());
        
        _maintenanceRequestRepository = new MaintenanceRequestRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _maintenanceRequestRepository.GetAllMaintenanceRequests());
        _mockDbContext.VerifyAll();
    }
    
    [TestMethod]
    public void GetMaintenanceRequestByCategory_MaintenanceRequestsAreReturn()
    {
        Guid categoryId = Guid.NewGuid();
        _maintenanceRequestInDb.Category = categoryId;
        
        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests = new List<MaintenanceRequest> {_maintenanceRequestInDb};

        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb);
        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb2);
        _dbContext.SaveChanges();
        
        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse = _maintenanceRequestRepository.GetMaintenanceRequestByCategory(categoryId);
        
        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }
    
    [TestMethod]
    public void GetMaintenanceRequestByCategory_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<MaintenanceRequest>()).Throws(new Exception());
        
        _maintenanceRequestRepository = new MaintenanceRequestRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _maintenanceRequestRepository.GetMaintenanceRequestByCategory(Guid.NewGuid()));
        _mockDbContext.VerifyAll();
    }
    
    [TestMethod]
    public void CreateMaintenanceRequest_MaintenanceRequestIsCreated()
    {
        _maintenanceRequestRepository.CreateMaintenanceRequest(_maintenanceRequestInDb);
        
        MaintenanceRequest maintenanceRequestResponse = _dbContext.Set<MaintenanceRequest>().Find(_maintenanceRequestInDb.Id);
        
        Assert.AreEqual(_maintenanceRequestInDb, maintenanceRequestResponse);
    }
    
    [TestMethod]
    public void CreateMaintenanceRequest_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<MaintenanceRequest>()).Throws(new Exception());
        
        _maintenanceRequestRepository = new MaintenanceRequestRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _maintenanceRequestRepository.CreateMaintenanceRequest(_maintenanceRequestInDb));
        _mockDbContext.VerifyAll();
    }
    
    [TestMethod]
    public void UpdateMaintenanceRequest_MaintenanceRequestIsUpdated()
    {
        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb);
        _dbContext.SaveChanges();
        
        _maintenanceRequestInDb.Description = "Room fixed";
        _maintenanceRequestInDb.RequestStatus = StatusEnum.Accepted;
        _maintenanceRequestRepository.UpdateMaintenanceRequest(_maintenanceRequestInDb.Id, _maintenanceRequestInDb);
        
        MaintenanceRequest maintenanceRequestResponse = _dbContext.Set<MaintenanceRequest>().Find(_maintenanceRequestInDb.Id);
        
        Assert.AreEqual(_maintenanceRequestInDb, maintenanceRequestResponse);
    }
    
    [TestMethod]
    public void UpdateMaintenanceRequest_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<MaintenanceRequest>()).Throws(new Exception());
        
        _maintenanceRequestRepository = new MaintenanceRequestRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _maintenanceRequestRepository.UpdateMaintenanceRequest(Guid.NewGuid(), _maintenanceRequestInDb));
        _mockDbContext.VerifyAll();
    }
    
    

    
    
}