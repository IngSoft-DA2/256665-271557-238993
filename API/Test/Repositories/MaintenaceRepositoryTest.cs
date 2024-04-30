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

    [TestInitialize]
    public void TestInitialize()
    {
        _dbContext = CreateDbContext("MaintenanceRepositoryTest");
        _dbContext.Set<MaintenanceRequest>();
        _maintenanceRequestRepository = new MaintenanceRequestRepository(_dbContext);
    }
    
    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
    }
    
    [TestMethod]
    public void GetAllMaintenanceRequests_MaintenanceRequestsAreReturn()
    {
        MaintenanceRequest maintenanceRequestInDb = new MaintenanceRequest
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
        MaintenanceRequest maintenanceRequestInDb2 = new MaintenanceRequest
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
        
        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests = new List<MaintenanceRequest> {maintenanceRequestInDb, maintenanceRequestInDb2};

        _dbContext.Set<MaintenanceRequest>().Add(maintenanceRequestInDb);
        _dbContext.Set<MaintenanceRequest>().Add(maintenanceRequestInDb2);
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
        MaintenanceRequest maintenanceRequestInDb = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            FlatId = Guid.Empty,
            BuildingId = Guid.Empty,
            ClosedDate = DateTime.Now.AddDays(1),
            OpenedDate = DateTime.Now,
            RequestHandlerId = Guid.Empty,
            RequestStatus = StatusEnum.Rejected,
            Category = categoryId,
            Description = "Bath broken",
        };
        MaintenanceRequest maintenanceRequestInDb2 = new MaintenanceRequest
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
        
        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests = new List<MaintenanceRequest> {maintenanceRequestInDb};

        _dbContext.Set<MaintenanceRequest>().Add(maintenanceRequestInDb);
        _dbContext.Set<MaintenanceRequest>().Add(maintenanceRequestInDb2);
        _dbContext.SaveChanges();
        
        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse = _maintenanceRequestRepository.GetMaintenanceRequestByCategory(categoryId);
        
        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }

    
    
}