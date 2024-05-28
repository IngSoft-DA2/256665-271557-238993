using Domain;
using Domain.Enums;
using IRepository;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class ReportServiceTest
{
    #region Test initialize

    private Mock<IReportRepository> _reportRepository;
    private ReportService _reportService;

    private Guid _buildingId;
    private Guid _buildingId2;
    private Guid _flatId;
    private Guid _flatId2;

    private Guid _categoryId1;
    private Guid _categoryId2;

    private List<MaintenanceRequest> _expectedRepositoryResponse;
    private Guid _requestHandlerId;
    private Guid _requestHandlerId2;
    private List<Report> _expectedReports;

    [TestInitialize]
    public void Initialize()
    {
        _reportRepository = new Mock<IReportRepository>(MockBehavior.Strict);
        _reportService = new ReportService(_reportRepository.Object);

        _categoryId1 = Guid.NewGuid();
        _categoryId2 = Guid.NewGuid();

        _buildingId = Guid.NewGuid();
        _buildingId2 = Guid.NewGuid();

        _requestHandlerId = Guid.NewGuid();
        _requestHandlerId2 = Guid.NewGuid();

        Report reportNode1 = new Report()
        {
            IdOfResourceToReport = _buildingId,
            ClosedRequests = 2,
            OpenRequests = 3,
            OnAttendanceRequests = 1
        };

        Report reportNode2 = new Report()
        {
            IdOfResourceToReport = _buildingId2,
            ClosedRequests = 0,
            OpenRequests = 0,
            OnAttendanceRequests = 1
        };

        _expectedReports = new List<Report>
            { reportNode1, reportNode2 };

        _flatId = Guid.NewGuid();
        _flatId2 = Guid.NewGuid();

        _expectedRepositoryResponse = new List<MaintenanceRequest>
        {
            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                Flat = new Flat()
                {
                    Id = _flatId,
                    BuildingId = _buildingId,
                    OwnerAssigned = new Owner()
                    {
                        Firstname = "Someone"
                    },
                    RoomNumber = "3B"
                },
                ClosedDate = new DateTime(2029, 12, 31),
                OpenedDate = new DateTime(2029, 12, 29),
                RequestStatus = RequestStatusEnum.Closed,
                CategoryId = _categoryId1,
                Description = "Bath broken",
                RequestHandlerId = _requestHandlerId,
                FlatId = _flatId
            },
            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                Flat = new Flat()
                {
                    Id = _flatId,
                    BuildingId = _buildingId,
                    OwnerAssigned = new Owner()
                    {
                        Firstname = "Someone"
                    },
                    RoomNumber = "3B"
                },
                ClosedDate = new DateTime(2029, 12, 31),
                OpenedDate = new DateTime(2029, 12, 29),
                RequestStatus = RequestStatusEnum.Closed,
                CategoryId = _categoryId1,
                Description = "Room broken",
                RequestHandlerId = _requestHandlerId,
                FlatId = _flatId
            },

            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                Flat = new Flat()
                {
                    Id = _flatId,
                    BuildingId = _buildingId,
                    OwnerAssigned = new Owner()
                    {
                        Firstname = "Someone"
                    },
                    RoomNumber = "3B"
                },
                ClosedDate = new DateTime(2029, 12, 31),
                OpenedDate = new DateTime(2029, 12, 29),
                RequestStatus = RequestStatusEnum.Open,
                CategoryId = _categoryId1,
                Description = "Room broken",
                RequestHandlerId = _requestHandlerId,
                FlatId = _flatId
            },
            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                Flat = new Flat()
                {
                    Id = _flatId,
                    BuildingId = _buildingId,
                    OwnerAssigned = new Owner()
                    {
                        Firstname = "Someone"
                    },
                    RoomNumber = "3B"
                },
                ClosedDate = new DateTime(2029, 12, 31),
                OpenedDate = new DateTime(2029, 12, 29),
                RequestStatus = RequestStatusEnum.Open,
                CategoryId = _categoryId1,
                Description = "Room broken",
                RequestHandlerId = _requestHandlerId,
                FlatId = _flatId
            },

            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                Flat = new Flat()
                {
                    Id = _flatId,
                    BuildingId = _buildingId,
                    OwnerAssigned = new Owner()
                    {
                        Firstname = "Someone"
                    },
                    RoomNumber = "3B"
                },
                ClosedDate = new DateTime(2029, 12, 31),
                OpenedDate = new DateTime(2029, 12, 29),
                RequestStatus = RequestStatusEnum.Open,
                CategoryId = _categoryId1,
                Description = "Room broken",
                RequestHandlerId = _requestHandlerId,
                FlatId = _flatId
            },

            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                Flat = new Flat()
                {
                    Id = _flatId,
                    BuildingId = _buildingId,
                    OwnerAssigned = new Owner()
                    {
                        Firstname = "Someone"
                    },
                    RoomNumber = "3B"
                },
                ClosedDate = new DateTime(2029, 12, 31),
                OpenedDate = new DateTime(2029, 12, 29),
                RequestStatus = RequestStatusEnum.InProgress,
                CategoryId = _categoryId1,
                Description = "Room broken",
                RequestHandlerId = _requestHandlerId,
                FlatId = _flatId
            },
            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                Flat = new Flat()
                {
                    Id = _flatId2,
                    BuildingId = _buildingId2,
                    OwnerAssigned = new Owner()
                    {
                        Firstname = "SomeoneTwo"
                    },
                    RoomNumber = "3C"
                },
                ClosedDate = new DateTime(2029, 12, 31),
                OpenedDate = new DateTime(2029, 12, 29),
                RequestStatus = RequestStatusEnum.InProgress,
                CategoryId = _categoryId2,
                Description = "Room broken",
                RequestHandlerId = _requestHandlerId2,
                FlatId = _flatId2
            },
        };
    }

    #endregion

    #region Get maintenance report by building

    [TestMethod]
    public void GetMaintenanceReportByBuilding_ReportsAreReturned()
    {
        _reportRepository.Setup(reportRepository =>
                reportRepository.GetMaintenanceReportByBuilding(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Returns(_expectedRepositoryResponse);

        IEnumerable<Report> actualResponse =
            _reportService.GetMaintenanceReportByBuilding(It.IsAny<Guid>(), Guid.Empty);

        _reportRepository.VerifyAll();

        Assert.IsTrue(actualResponse.SequenceEqual(_expectedReports));
    }

    [TestMethod]
    public void GetMaintenanceReportByBuilding_ExceptionThrown()
    {
        _reportRepository.Setup(reportRepository =>
                reportRepository.GetMaintenanceReportByBuilding(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _reportService.GetMaintenanceReportByBuilding(It.IsAny<Guid>(), It.IsAny<Guid>()));

        _reportRepository.VerifyAll();
    }

    #endregion

    #region Get maintenance report by request handler

    [TestMethod]
    public void GetMaintenanceReportByRequestHandler_ReportsAreReturned()
    {
        IEnumerable<RequestHandlerReport> expectedReportResponse = new List<RequestHandlerReport>
        {
            new RequestHandlerReport()
            {
                IdOfResourceToReport = _requestHandlerId,
                OnAttendanceRequests = 1,
                OpenRequests = 3,
                ClosedRequests = 2,
                AvgTimeToCloseRequest = TimeSpan.FromDays(2),
                TotalTime = TimeSpan.FromDays(4)
            },
            new RequestHandlerReport()
            {
                IdOfResourceToReport = _requestHandlerId2,
                OnAttendanceRequests = 1,
                OpenRequests = 0,
                ClosedRequests = 0,
                AvgTimeToCloseRequest = TimeSpan.Zero,
                TotalTime = TimeSpan.Zero
            }
        };

        _reportRepository.Setup(reportRepository =>
                reportRepository.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>(), It.IsAny<Guid>(),
                    It.IsAny<Guid>()))
            .Returns(_expectedRepositoryResponse);

        IEnumerable<RequestHandlerReport> actualResponse =
            _reportService.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>());

        _reportRepository.VerifyAll();

        Assert.IsTrue(expectedReportResponse.SequenceEqual(actualResponse));
    }

    [TestMethod]
    public void GetMaintenanceReportByRequestHandler_ExceptionThrown()
    {
        _reportRepository.Setup(reportRepository =>
                reportRepository.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>(), It.IsAny<Guid>(),
                    It.IsAny<Guid>()))
            .Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _reportService.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()));

        _reportRepository.VerifyAll();
    }

    #endregion

    #region Get maintenance report by category

    [TestMethod]
    public void GetMaintenanceReportByCategory_ReportsAreReturned()
    {
        Report reportNode1 = new Report()
        {
            IdOfResourceToReport = _categoryId1,
            ClosedRequests = 2,
            OpenRequests = 3,
            OnAttendanceRequests = 1
        };

        Report reportNode2 = new Report()
        {
            IdOfResourceToReport = _categoryId2,
            ClosedRequests = 0,
            OpenRequests = 0,
            OnAttendanceRequests = 1
        };

        _expectedReports = new List<Report>
            { reportNode1, reportNode2 };

        _reportRepository.Setup(reportRepository =>
                reportRepository.GetMaintenanceReportByCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Returns(_expectedRepositoryResponse);

        IEnumerable<Report> actualResponse =
            _reportService.GetMaintenanceReportByCategory(It.IsAny<Guid>(), It.IsAny<Guid>());

        _reportRepository.VerifyAll();

        Assert.IsTrue(_expectedReports.SequenceEqual(actualResponse));
    }

    [TestMethod]
    public void GetMaintenanceReportByCategory_ExceptionThrown()
    {
        _reportRepository.Setup(reportRepository =>
                reportRepository.GetMaintenanceReportByCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _reportService.GetMaintenanceReportByCategory(It.IsAny<Guid>(), It.IsAny<Guid>()));

        _reportRepository.VerifyAll();
    }

    #endregion

    #region Get flat report by building

    [TestMethod]
    public void GetFlatReportByBuilding_ReportsAreReturnedCorrectly()
    {
        IEnumerable<FlatRequestReport> expectedReportResponse = new List<FlatRequestReport>
        {
            new FlatRequestReport()
            {
                IdOfResourceToReport = _flatId,
                ClosedRequests = 2,
                OpenRequests = 3,
                OnAttendanceRequests = 1,
                BuildingId = _buildingId,
                OwnerName = "Someone",
                FlatNumber = "3B"
            },
            new FlatRequestReport()
            {
                IdOfResourceToReport = _flatId2,
                ClosedRequests = 0,
                OpenRequests = 0,
                OnAttendanceRequests = 1,
                BuildingId = _buildingId2,
                OwnerName = "SomeoneTwo",
                FlatNumber = "3C"
            }
        };

        _reportRepository.Setup(reportRepository =>
                reportRepository.GetFlatRequestsReportByBuilding(It.IsAny<Guid>()))
            .Returns(_expectedRepositoryResponse);

        IEnumerable<FlatRequestReport> actualResponse =
            _reportService.GetFlatRequestsByBuildingReport(It.IsAny<Guid>());

        _reportRepository.VerifyAll();

        Assert.IsTrue(expectedReportResponse.SequenceEqual(actualResponse));
    }
    
    [TestMethod]
    public void GetFlatReportByBuilding_ExceptionThrown()
    {
        _reportRepository.Setup(reportRepository =>
                reportRepository.GetFlatRequestsReportByBuilding(It.IsAny<Guid>()))
            .Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _reportService.GetFlatRequestsByBuildingReport(It.IsAny<Guid>()));

        _reportRepository.VerifyAll();
    }
    
    #endregion
}