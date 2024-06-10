using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories.CustomExceptions;

namespace Test.Repositories;

[TestClass]
public class ReportRepositoryTest
{
    #region Initializing Aspects

    private DbContext _dbContext;
    private ReportRepository _reportRepository;

    [TestInitialize]
    public void TestInitialize()
    {
        _dbContext = CreateDbContext("ReportRepositoryTest");
        _dbContext.Set<MaintenanceRequest>();
        _reportRepository = new ReportRepository(_dbContext);
    }

    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
    }

    #endregion

    #region Get Maintenance Report By Building

    [TestMethod]
    public void GetMaintenanceReportByBuilding_ReturnsMaintenanceRequestsWhenBuildingFromQueryIsSet()
    {
        MaintenanceRequest maintenanceRequestInDb2 = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            Manager = new Manager()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.Manager,
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password",
                Buildings = new List<Building>()
            },
            RequestHandler = new RequestHandler()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.RequestHandler,
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password",
                LastName = "LastName"
            },
            Category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category"
            },
            Description = "Description",
            OpenedDate = DateTime.Now,
            ClosedDate = DateTime.Now,
            RequestStatus = RequestStatusEnum.Open,
            Flat = new Flat
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                OwnerAssigned = new Owner()
                {
                    Id = Guid.NewGuid(),
                    Firstname = "FirstName",
                    Email = "Email",
                    Flats = new List<Flat>(),
                    Lastname = "Lastname",
                },
                RoomNumber = "301"
            }
        };

        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests =
            new List<MaintenanceRequest> { maintenanceRequestInDb2 };

        _dbContext.Set<MaintenanceRequest>().Add(maintenanceRequestInDb2);
        _dbContext.SaveChanges();

        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse =
            _reportRepository.GetMaintenanceReportByBuilding(maintenanceRequestInDb2.ManagerId,
                maintenanceRequestInDb2.Flat.BuildingId);

        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }

    [TestMethod]
    public void GetMaintenanceReportByBuilding_ReturnsMaintenanceRequestsWhenNoBuildingComesFromQuery()
    {
        MaintenanceRequest maintenanceRequestInDb2 = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            Manager = new Manager()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.Manager,
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password",
                Buildings = new List<Building>()
            },
            RequestHandler = new RequestHandler()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.RequestHandler,
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password",
                LastName = "LastName",
            },
            Category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category"
            },
            Description = "Description",
            OpenedDate = DateTime.Now,
            ClosedDate = DateTime.Now,
            RequestStatus = RequestStatusEnum.Open,
            Flat = new Flat
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                OwnerAssigned = new Owner()
                {
                    Id = Guid.NewGuid(),
                    Firstname = "FirstName",
                    Email = "Email",
                    Flats = new List<Flat>(),
                    Lastname = "Lastname",
                },
                RoomNumber = "301"
            }
        };

        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests =
            new List<MaintenanceRequest> { maintenanceRequestInDb2 };

        _dbContext.Set<MaintenanceRequest>().Add(maintenanceRequestInDb2);
        _dbContext.SaveChanges();

        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse =
            _reportRepository.GetMaintenanceReportByBuilding(maintenanceRequestInDb2.ManagerId, Guid.Empty);

        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }

    [TestMethod]
    public void GetMaintenanceReportByBuilding_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<MaintenanceRequest>()).Throws(new Exception());

        _reportRepository = new ReportRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() =>
            _reportRepository.GetMaintenanceReportByBuilding(Guid.NewGuid(), Guid.NewGuid()));
        _mockDbContext.VerifyAll();
    }

    #endregion

    #region Get Maintenance Report By Request Handler

    [TestMethod]
    public void GetMaintenanceReportByRequestHandler_ReturnsMaintenanceRequestsWhenRequestHandlerFromQueryIsSet()
    {
        MaintenanceRequest maintenanceRequestInDb2 = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            Manager = new Manager()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.Manager,
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password",
                Buildings = new List<Building>()
            },
            RequestHandler = new RequestHandler()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.RequestHandler,
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password",
                LastName = "LastName",
            },
            Category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category"
            },
            Description = "Description",
            OpenedDate = DateTime.Now,
            ClosedDate = DateTime.Now,
            RequestStatus = RequestStatusEnum.Open,
            Flat = new Flat
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                OwnerAssigned = new Owner()
                {
                    Id = Guid.NewGuid(),
                    Firstname = "FirstName",
                    Email = "Email",
                    Flats = new List<Flat>(),
                    Lastname = "Lastname"
                },
                RoomNumber = "301"
            }
        };

        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests =
            new List<MaintenanceRequest> { maintenanceRequestInDb2 };

        _dbContext.Set<MaintenanceRequest>().Add(maintenanceRequestInDb2);
        _dbContext.SaveChanges();

        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse =
            _reportRepository.GetMaintenanceReportByRequestHandler(maintenanceRequestInDb2.RequestHandlerId,
                maintenanceRequestInDb2.Flat.BuildingId, maintenanceRequestInDb2.ManagerId);

        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }

    [TestMethod]
    public void GetMaintenanceReportByRequestHandler_ReturnsMaintenanceRequestsWhenNoRequestHandlerComesFromQuery()
    {
        MaintenanceRequest maintenanceRequestInDb2 = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            Manager = new Manager()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.Manager,
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password",
                Buildings = new List<Building>()
            },
            RequestHandler = new RequestHandler()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.RequestHandler,
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password",
                LastName = "LastName",
            },
            Category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category"
            },
            Description = "Description",
            OpenedDate = DateTime.Now,
            ClosedDate = DateTime.Now,
            RequestStatus = RequestStatusEnum.Open,
            Flat = new Flat
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                OwnerAssigned = new Owner()
                {
                    Id = Guid.NewGuid(),
                    Firstname = "FirstName",
                    Email = "Email",
                    Flats = new List<Flat>(),
                    Lastname = "Lastname"
                },
                RoomNumber = "301"
            }
        };

        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests =
            new List<MaintenanceRequest> { maintenanceRequestInDb2 };

        _dbContext.Set<MaintenanceRequest>().Add(maintenanceRequestInDb2);
        _dbContext.SaveChanges();

        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse =
            _reportRepository.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>(), maintenanceRequestInDb2.Flat.BuildingId,
                maintenanceRequestInDb2.ManagerId);

        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }

    [TestMethod]
    public void GetMaintenanceReportByRequestHandler_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<MaintenanceRequest>()).Throws(new Exception());

        _reportRepository = new ReportRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() =>
            _reportRepository.GetMaintenanceReportByRequestHandler(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()));
        _mockDbContext.VerifyAll();
    }

    #endregion

    #region Get Maintenance Report By Category

    [TestMethod]
    public void GetMaintenanceReportByCategory_ReturnsMaintenanceRequestsWhenCategoryFromQueryIsSet()
    {
        MaintenanceRequest maintenanceRequestInDb2 = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            Manager = new Manager()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.Manager,
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password",
                Buildings = new List<Building>(),
            },
            RequestHandler = new RequestHandler()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.RequestHandler,
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password",
                LastName = "LastName",
            },
            Category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category"
            },
            Description = "Description",
            OpenedDate = DateTime.Now,
            ClosedDate = DateTime.Now,
            RequestStatus = RequestStatusEnum.Open,
            Flat = new Flat
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                OwnerAssigned = new Owner()
                {
                    Id = Guid.NewGuid(),
                    Firstname = "FirstName",
                    Email = "Email",
                    Flats = new List<Flat>(),
                    Lastname = "Lastname"
                },
                RoomNumber = "301"
            }
        };

        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests =
            new List<MaintenanceRequest> { maintenanceRequestInDb2 };

        _dbContext.Set<MaintenanceRequest>().Add(maintenanceRequestInDb2);
        _dbContext.SaveChanges();

        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse =
            _reportRepository.GetMaintenanceReportByCategory(maintenanceRequestInDb2.Flat.BuildingId,
                maintenanceRequestInDb2.CategoryId);

        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }

    [TestMethod]
    public void GetMaintenanceReportByCategory_ReturnsMaintenanceRequestsWhenNoCategoryComesFromQuery()
    {
        MaintenanceRequest maintenanceRequestInDb2 = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            Manager = new Manager()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.Manager,
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password",
                Buildings = new List<Building>(),
            },
            RequestHandler = new RequestHandler()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.RequestHandler,
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password",
                LastName = "LastName",
            },
            Category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category"
            },
            Description = "Description",
            OpenedDate = DateTime.Now,
            ClosedDate = DateTime.Now,
            RequestStatus = RequestStatusEnum.Open,
            Flat = new Flat
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                OwnerAssigned = new Owner()
                {
                    Id = Guid.NewGuid(),
                    Firstname = "FirstName",
                    Email = "Email",
                    Flats = new List<Flat>(),
                    Lastname = "Lastname"
                },
                RoomNumber = "301"
            }
        };

        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests =
            new List<MaintenanceRequest> { maintenanceRequestInDb2 };

        _dbContext.Set<MaintenanceRequest>().Add(maintenanceRequestInDb2);
        _dbContext.SaveChanges();

        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse =
            _reportRepository.GetMaintenanceReportByCategory(maintenanceRequestInDb2.Flat.BuildingId, Guid.Empty);

        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }

    [TestMethod]
    public void GetMaintenanceReportByCategory_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<MaintenanceRequest>()).Throws(new Exception());

        _reportRepository = new ReportRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() =>
            _reportRepository.GetMaintenanceReportByCategory(Guid.NewGuid(), Guid.NewGuid()));
        _mockDbContext.VerifyAll();
    }

    #endregion

    #region Get Flat Requests Report By Building

    [TestMethod]
    public void GetFlatRequestsReportByBuilding_ReturnsMaintenanceRequestsWhenBuildingFromQueryIsSet()
    {
        MaintenanceRequest maintenanceRequestInDb2 = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            Manager = new Manager()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.Manager,
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password",
                Buildings = new List<Building>()
            },
            RequestHandler = new RequestHandler()
            {
                Id = Guid.NewGuid(),
                Role = SystemUserRoleEnum.RequestHandler,
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password",
                LastName = "LastName"
            },
            Category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category"
            },
            Description = "Description",
            OpenedDate = DateTime.Now,
            ClosedDate = DateTime.Now,
            RequestStatus = RequestStatusEnum.Open,
            Flat = new Flat
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                OwnerAssigned = new Owner()
                {
                    Id = Guid.NewGuid(),
                    Firstname = "FirstName",
                    Email = "Email",
                    Flats = new List<Flat>(),
                    Lastname = "Lastname",
                },
                RoomNumber = "301"
            }
        };

        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests =
            new List<MaintenanceRequest> { maintenanceRequestInDb2 };

        _dbContext.Set<MaintenanceRequest>().Add(maintenanceRequestInDb2);
        _dbContext.SaveChanges();

        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse =
            _reportRepository.GetFlatRequestsReportByBuilding(maintenanceRequestInDb2.Flat.BuildingId);

        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }

    [TestMethod]
    public void GetFlatRequestsReportByBuilding_ReturnsEmptyListWhenNoBuildingComesFromQuery()
    {
        IEnumerable<MaintenanceRequest> expectedMaintenanceRequests = new List<MaintenanceRequest>();

        IEnumerable<MaintenanceRequest> maintenanceRequestsResponse =
            _reportRepository.GetFlatRequestsReportByBuilding(Guid.Empty);

        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(maintenanceRequestsResponse));
    }
    
    [TestMethod]
    public void GetFlatRequestsReportByBuilding_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(m => m.Set<MaintenanceRequest>()).Throws(new Exception());

        _reportRepository = new ReportRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() =>
            _reportRepository.GetFlatRequestsReportByBuilding(Guid.NewGuid()));
        _mockDbContext.VerifyAll();
    }

    #endregion

    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Database.EnsureDeleted();
    }
}