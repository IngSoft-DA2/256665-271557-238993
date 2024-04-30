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
    #region Initialzing Aspects
    
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
            RequestStatus = RequestStatusEnum.Closed,
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
            RequestStatus = RequestStatusEnum.Closed,
            Category = Guid.NewGuid(),
            Description = "Room broken",
        };
    }
    
    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
    }
    
    #endregion
    
    #region Get All Maintenance Requests
    
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
    
    #endregion
    
    #region Get Maintenance Request By Category
    
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
    
    #endregion
    
    #region Create Maintenance Request
    
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
    
    #endregion
    
    #region Update Maintenance Request
    
    [TestMethod]
    public void UpdateMaintenanceRequest_MaintenanceRequestIsUpdated()
    {
        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb);
        _dbContext.SaveChanges();
        
        _maintenanceRequestInDb.Description = "Room fixed";
        _maintenanceRequestInDb.RequestStatus = RequestStatusEnum.Closed;
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
    
    #endregion
    
    #region Get Maintenance Request By Id
    
    [TestMethod]
    public void GetMaintenanceRequestById_MaintenanceRequestIsReturn()
    {
        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb);
        _dbContext.SaveChanges();
        
        MaintenanceRequest maintenanceRequestResponse = _maintenanceRequestRepository.GetMaintenanceRequestById(_maintenanceRequestInDb.Id);
        
        Assert.AreEqual(_maintenanceRequestInDb, maintenanceRequestResponse);
    }
    
    [TestMethod]
    public void GetMaintenanceRequestById_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<MaintenanceRequest>()).Throws(new Exception());
        
        _maintenanceRequestRepository = new MaintenanceRequestRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _maintenanceRequestRepository.GetMaintenanceRequestById(Guid.NewGuid()));
        _mockDbContext.VerifyAll();
    }
    
    #endregion
    
    #region Get Maintenance Requests By Request Handler
    
    [TestMethod]
    public void GetMaintenanceRequestsByRequestHandler_MaintenanceRequestsAreReturn()
    {
        Guid requestHandlerId = Guid.NewGuid();
        _maintenanceRequestInDb.RequestHandlerId = requestHandlerId;
        
        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests = new List<MaintenanceRequest> {_maintenanceRequestInDb};

        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb);
        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb2);
        _dbContext.SaveChanges();
        
        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse = _maintenanceRequestRepository.GetMaintenanceRequestsByRequestHandler(requestHandlerId);
        
        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }
    
    [TestMethod]
    public void GetMaintenanceRequestsByRequestHandler_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<MaintenanceRequest>()).Throws(new Exception());
        
        _maintenanceRequestRepository = new MaintenanceRequestRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _maintenanceRequestRepository.GetMaintenanceRequestsByRequestHandler(Guid.NewGuid()));
        _mockDbContext.VerifyAll();
    }
    
    #endregion
    
    
}