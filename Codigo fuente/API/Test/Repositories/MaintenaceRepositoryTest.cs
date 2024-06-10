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
    private RequestHandler _requestHanlder;
    private Category _category;
    private Manager _manager;

    [TestInitialize]
    public void TestInitialize()
    {
        _dbContext = CreateDbContext("MaintenanceRepositoryTest");
        _dbContext.Set<MaintenanceRequest>();
        _maintenanceRequestRepository = new MaintenanceRequestRepository(_dbContext);
        _requestHanlder = new RequestHandler()
        {
            Id = Guid.NewGuid(),
            Role = SystemUserRoleEnum.RequestHandler,
            Firstname = "FirstName",
            Email = "Email",
            LastName = "Lastname",
            Password = "Password"
        };
        _category = new Category()
        {
            Id = Guid.NewGuid(),
            Name = "Category"
        };
        _manager = new Manager()
        {
            Id = Guid.NewGuid(),
            Role = SystemUserRoleEnum.Manager,
            Firstname = "FirstName",
            Email = "Email",
            Password = "Password"
        };

        _dbContext.Set<RequestHandler>().Add(_requestHanlder);
        _dbContext.Set<Category>().Add(_category);
        _dbContext.Set<Manager>().Add(_manager);
        _dbContext.SaveChanges();

        _maintenanceRequestInDb = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            Flat = new Flat
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                RoomNumber = "302",
                OwnerAssigned = new Owner()
                {
                    Id = Guid.NewGuid(), 
                    Firstname= "Owner",
                    Lastname = "Owner",
                    Email = "some@gmail.com",
                    Flats = new List<Flat>()
                }
            },
            ClosedDate = DateTime.Now.AddDays(1),
            OpenedDate = DateTime.Now,
            RequestHandler = new RequestHandler()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.RequestHandler,
                Firstname = "FirstName",
                Email = "Email",
                LastName = "Lastname",
                Password = "Password"      
            },
            RequestStatus = RequestStatusEnum.Closed,
            Category = _category,
            CategoryId = _category.Id,
            Description = "Bath broken",
            Manager = new Manager()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.Manager,
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password"
            }

        };

        _maintenanceRequestInDb2 = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            Flat = new Flat
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                RoomNumber = "301",
                OwnerAssigned = new Owner()
                {
                    Id = Guid.NewGuid(), 
                    Firstname= "Ownerb",
                    Lastname = "Ownerb",
                    Email = "someb@gmail.com",
                    Flats = new List<Flat>()
                }
            },
            ClosedDate = DateTime.Now.AddDays(1),
            OpenedDate = DateTime.Now,
            RequestHandler = new RequestHandler()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.RequestHandler,
                Firstname = "FirstName",
                LastName = "Lastname",
                Email = "Email",
                Password = "Password"
            },
            RequestStatus = RequestStatusEnum.Open,
            Category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Category"
            },
            Description = "Bath broken",
            Manager = new Manager()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.Manager,
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password"
            }
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
    public void GetAllMaintenanceRequests_MaintenanceRequestsAreNotReturnedWhenNoQueryIsSet()
    {

        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests = new List<MaintenanceRequest>();

        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb);
        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb2);
        _dbContext.SaveChanges();

        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse = _maintenanceRequestRepository.GetAllMaintenanceRequests(null, Guid.NewGuid());

        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }

    [TestMethod]
    public void GetAllMaintenanceRequests_MaintenanceRequestsAreReturnWhenQueryIsSet()
    {
        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests = new List<MaintenanceRequest> { _maintenanceRequestInDb };

        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb);
        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb2);
        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb);
        _dbContext.SaveChanges();

        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse = _maintenanceRequestRepository.GetAllMaintenanceRequests(_maintenanceRequestInDb.ManagerId, It.IsAny<Guid>());

        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }   

    [TestMethod]
    public void GetAllMaintenanceRequests_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<MaintenanceRequest>()).Throws(new Exception());

        _maintenanceRequestRepository = new MaintenanceRequestRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _maintenanceRequestRepository.GetAllMaintenanceRequests(null, It.IsAny<Guid>()));
        _mockDbContext.VerifyAll();
    }

    [TestMethod]
    public void GivenACategoryToFilterBy_ShouldReturnMaintenanceRequestsFilteredByThatCategory()
    {
        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests = new List<MaintenanceRequest> { _maintenanceRequestInDb };

        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb);
        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb2);
        _dbContext.SaveChanges();

        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse = _maintenanceRequestRepository.GetAllMaintenanceRequests(_maintenanceRequestInDb.ManagerId, _maintenanceRequestInDb.CategoryId);

        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }

    #endregion

    #region Get Maintenance Request By Category

    [TestMethod]
    public void GetMaintenanceRequestByCategory_MaintenanceRequestsAreReturn()
    {
        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests = new List<MaintenanceRequest> { _maintenanceRequestInDb };

        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb);
        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb2);
        _dbContext.SaveChanges();

        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse = _maintenanceRequestRepository.GetMaintenanceRequestByCategory(_maintenanceRequestInDb.CategoryId);

        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }

    [TestMethod]
    public void GetMaintenanceRequestByCategory_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<MaintenanceRequest>()).Throws(new Exception());

        _maintenanceRequestRepository = new MaintenanceRequestRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() =>
            _maintenanceRequestRepository.GetMaintenanceRequestByCategory(Guid.NewGuid()));
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
        Assert.ThrowsException<UnknownRepositoryException>(() =>
            _maintenanceRequestRepository.CreateMaintenanceRequest(_maintenanceRequestInDb));
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
        Assert.ThrowsException<UnknownRepositoryException>(() =>
            _maintenanceRequestRepository.UpdateMaintenanceRequest(Guid.NewGuid(), _maintenanceRequestInDb));
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
        Assert.ThrowsException<UnknownRepositoryException>(() =>
            _maintenanceRequestRepository.GetMaintenanceRequestById(Guid.NewGuid()));
        _mockDbContext.VerifyAll();
    }

    #endregion

    #region Get Maintenance Requests By Request Handler

    [TestMethod]
    public void GetMaintenanceRequestsByRequestHandler_MaintenanceRequestsAreReturn()
    {
        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests = new List<MaintenanceRequest> { _maintenanceRequestInDb };
        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb);
        _dbContext.Set<MaintenanceRequest>().Add(_maintenanceRequestInDb2);
        _dbContext.SaveChanges();

        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse = _maintenanceRequestRepository.GetMaintenanceRequestsByRequestHandler(_maintenanceRequestInDb.RequestHandlerId);

        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }

    [TestMethod]
    public void GetMaintenanceRequestsByRequestHandler_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<MaintenanceRequest>()).Throws(new Exception());

        _maintenanceRequestRepository = new MaintenanceRequestRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() =>
            _maintenanceRequestRepository.GetMaintenanceRequestsByRequestHandler(Guid.NewGuid()));
        _mockDbContext.VerifyAll();
    }

    #endregion
    
    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Database.EnsureDeleted();
    }
}