using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Test.Repositories;

[TestClass]
public class ReportRepositoryTest
{
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


    [TestMethod]
    public void GetMaintenanceReportByBuilding_ReturnsMaintenanceRequestsWhenBuildingFromQueryIsSetted()
    {
        MaintenanceRequest maintenanceRequestInDb2 = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            Manager = new Manager()
            {
                Id = Guid.NewGuid(),
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password",
                Buildings = new List<Building>()
            },
            RequestHandler = new RequestHandler()
            {
                Id = Guid.NewGuid(),
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
                    Lastname = "Lastname"
                }
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
                Firstname = "FirstName",
                Email = "Email",
                Password = "Password",
                Buildings = new List<Building>()
            },
            RequestHandler = new RequestHandler()
            {
                Id = Guid.NewGuid(),
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
                    Lastname = "Lastname"
                }
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
}