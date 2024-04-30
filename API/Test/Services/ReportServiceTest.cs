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

        _expectedRepositoryResponse = new List<MaintenanceRequest>
        {
            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                BuildingId = _buildingId,
                ClosedDate =  new DateTime(2029, 12, 31),
                OpenedDate = new DateTime(2029, 12, 29),
                RequestStatus = RequestStatusEnum.Closed,
                Category = _categoryId1,
                Description = "Bath broken",
                RequestHandlerId = _requestHandlerId
            },
            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                BuildingId = _buildingId,
                ClosedDate =  new DateTime(2029, 12, 31),
                OpenedDate = new DateTime(2029, 12, 29),
                RequestStatus = RequestStatusEnum.Closed,
                Category = _categoryId1,
                Description = "Room broken",
                RequestHandlerId = _requestHandlerId
            },

            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                BuildingId = _buildingId,
                ClosedDate =  new DateTime(2029, 12, 31),
                OpenedDate = new DateTime(2029, 12, 29),
                RequestStatus = RequestStatusEnum.Open,
                Category = _categoryId1,
                Description = "Room broken",
                RequestHandlerId = _requestHandlerId
            },
            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                BuildingId = _buildingId,
                ClosedDate =  new DateTime(2029, 12, 31),
                OpenedDate = new DateTime(2029, 12, 29),
                RequestStatus = RequestStatusEnum.Open,
                Category = _categoryId1,
                Description = "Room broken",
                RequestHandlerId = _requestHandlerId
            },

            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                BuildingId = _buildingId,
                ClosedDate =  new DateTime(2029, 12, 31),
                OpenedDate = new DateTime(2029, 12, 29),
                RequestStatus = RequestStatusEnum.Open,
                Category = _categoryId1,
                Description = "Room broken",
                RequestHandlerId = _requestHandlerId
            },

            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                BuildingId = _buildingId,
                ClosedDate =  new DateTime(2029, 12, 31),
                OpenedDate = new DateTime(2029, 12, 29),
                RequestStatus = RequestStatusEnum.InProgress,
                Category = _categoryId1,
                Description = "Room broken",
                RequestHandlerId = _requestHandlerId
            },
            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                BuildingId = _buildingId2,
                ClosedDate =  new DateTime(2029, 12, 31),
                OpenedDate = new DateTime(2029, 12, 29),
                RequestStatus = RequestStatusEnum.InProgress,
                Category = _categoryId2,
                Description = "Room broken",
                RequestHandlerId = _requestHandlerId2
            },
        };
    }

    #endregion
    

    [TestMethod]
    public void GetMaintenanceReportByBuilding_ReportsAreReturned()
    {
        _reportRepository.Setup(reportRepository => reportRepository.GetMaintenanceReportByBuilding(It.IsAny<Guid>()))
            .Returns(_expectedRepositoryResponse);

        IEnumerable<Report> actualResponse = _reportService.GetMaintenanceReportByBuilding(Guid.Empty);

        _reportRepository.VerifyAll();

        Assert.IsTrue(actualResponse.SequenceEqual(_expectedReports));
    }
    
    [TestMethod]
    public void GetMaintenanceReportByBuilding_ExceptionThrown()
    {
        _reportRepository.Setup(reportRepository => reportRepository.GetMaintenanceReportByBuilding(It.IsAny<Guid>()))
            .Throws(new Exception());
    
        Assert.ThrowsException<UnknownServiceException>(() =>
            _reportService.GetMaintenanceReportByBuilding(It.IsAny<Guid>()));
    
        _reportRepository.VerifyAll();
    }
    
    
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
                reportRepository.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>()))
            .Returns(_expectedRepositoryResponse);
    
        IEnumerable<RequestHandlerReport> actualResponse =
            _reportService.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>());
    
        _reportRepository.VerifyAll();
    
        Assert.IsTrue(expectedReportResponse.SequenceEqual(actualResponse));
    }
    
    [TestMethod]
    public void GetMaintenanceReportByRequestHandler_ExceptionThrown()
    {
        _reportRepository.Setup(reportRepository =>
                reportRepository.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>()))
            .Throws(new Exception());
    
        Assert.ThrowsException<UnknownServiceException>(() =>
            _reportService.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>()));
    
        _reportRepository.VerifyAll();
    }

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
        
        _reportRepository.Setup(reportRepository => reportRepository.GetMaintenanceReportByCategory(It.IsAny<Guid>()))
            .Returns(_expectedRepositoryResponse);
    
        IEnumerable<Report> actualResponse = _reportService.GetMaintenanceReportByCategory(It.IsAny<Guid>());
    
        _reportRepository.VerifyAll();
    
        Assert.IsTrue(_expectedReports.SequenceEqual(actualResponse));
    }
    //
    // [TestMethod]
    // public void GetMaintenanceReportByCategory_NoReportsAreReturned()
    // {
    //     IEnumerable<Report> expectedRepositoryResponse = new List<Report>();
    //
    //     _reportRepository.Setup(reportRepository => reportRepository.GetMaintenanceReportByCategory(It.IsAny<Guid>()))
    //         .Returns(expectedRepositoryResponse);
    //
    //     IEnumerable<Report> actualResponse = _reportService.GetMaintenanceReportByCategory(It.IsAny<Guid>());
    //
    //     _reportRepository.VerifyAll();
    //
    //     Assert.IsTrue(expectedRepositoryResponse.SequenceEqual(actualResponse));
    // }
    //
    // [TestMethod]
    // public void GetMaintenanceReportByCategory_ExceptionThrown()
    // {
    //     _reportRepository.Setup(reportRepository => reportRepository.GetMaintenanceReportByCategory(It.IsAny<Guid>()))
    //         .Throws(new Exception());
    //
    //     Assert.ThrowsException<UnknownServiceException>(() =>
    //         _reportService.GetMaintenanceReportByCategory(It.IsAny<Guid>()));
    //
    //     _reportRepository.VerifyAll();
    // }
    //
    // #endregion
    //
    // #region Get all maintenance requests by building
    //
    // [TestMethod]
    // public void GetAllMaintenanceRequestsByBuilding_ReportsAreReturned()
    // {
    //     IEnumerable<Report> expectedRepositoryResponse = new List<Report>
    //     {
    //         new Report()
    //         {
    //             IdOfResourceToReport = Guid.NewGuid(),
    //             OnAttendanceRequests = 1,
    //             OpenRequests = 23,
    //             ClosedRequests = 10,
    //         },
    //         new Report()
    //         {
    //             IdOfResourceToReport = Guid.NewGuid(),
    //             OnAttendanceRequests = 2,
    //             OpenRequests = 20,
    //             ClosedRequests = 12,
    //         }
    //     };
    //
    //     _reportRepository.Setup(reportRepository => reportRepository.GetAllMaintenanceRequestsByBuilding())
    //         .Returns(expectedRepositoryResponse);
    //
    //     IEnumerable<Report> actualResponse = _reportService.GetAllMaintenanceRequestsByBuilding();
    //
    //     _reportRepository.VerifyAll();
    //
    //     Assert.IsTrue(expectedRepositoryResponse.SequenceEqual(actualResponse));
    // }
    //
    // [TestMethod]
    // public void GetAllMaintenanceRequestsByBuilding_NoReportsAreReturned()
    // {
    //     IEnumerable<Report> expectedRepositoryResponse = new List<Report>();
    //
    //     _reportRepository.Setup(reportRepository => reportRepository.GetAllMaintenanceRequestsByBuilding())
    //         .Returns(expectedRepositoryResponse);
    //
    //     IEnumerable<Report> actualResponse = _reportService.GetAllMaintenanceRequestsByBuilding();
    //
    //     _reportRepository.VerifyAll();
    //
    //     Assert.IsTrue(expectedRepositoryResponse.SequenceEqual(actualResponse));
    // }
    //
    // [TestMethod]
    // public void GetAllMaintenanceRequestsByBuilding_ExceptionThrown()
    // {
    //     _reportRepository.Setup(reportRepository => reportRepository.GetAllMaintenanceRequestsByBuilding())
    //         .Throws(new Exception());
    //
    //     Assert.ThrowsException<UnknownServiceException>(() => _reportService.GetAllMaintenanceRequestsByBuilding());
    //
    //     _reportRepository.VerifyAll();
    // }
    //
    // #endregion
    //
    // #region Get all maintenance requests by request handler
    //
    // [TestMethod]
    // public void GetAllMaintenanceRequestsByRequestHandler_ReportsAreReturned()
    // {
    //     IEnumerable<RequestHandlerReport> expectedRepositoryResponse = new List<RequestHandlerReport>
    //     {
    //         new RequestHandlerReport()
    //         {
    //             IdOfResourceToReport = Guid.NewGuid(),
    //             OnAttendanceRequests = 1,
    //             OpenRequests = 23,
    //             ClosedRequests = 10,
    //         },
    //         new RequestHandlerReport()
    //         {
    //             IdOfResourceToReport = Guid.NewGuid(),
    //             OnAttendanceRequests = 2,
    //             OpenRequests = 20,
    //             ClosedRequests = 12,
    //         }
    //     };
    //
    //     _reportRepository.Setup(reportRepository =>
    //             reportRepository.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>()))
    //         .Returns(expectedRepositoryResponse);
    //
    //     IEnumerable<RequestHandlerReport> actualResponse =
    //         _reportService.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>());
    //
    //     _reportRepository.VerifyAll();
    //
    //     Assert.IsTrue(expectedRepositoryResponse.SequenceEqual(actualResponse));
    // }
    //
    // [TestMethod]
    // public void GetAllMaintenanceRequestsByRequestHandler_NoReportsAreReturned()
    // {
    //     IEnumerable<RequestHandlerReport> expectedRepositoryResponse = new List<RequestHandlerReport>();
    //
    //     _reportRepository.Setup(reportRepository =>
    //             reportRepository.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>()))
    //         .Returns(expectedRepositoryResponse);
    //
    //     IEnumerable<RequestHandlerReport> actualResponse =
    //         _reportService.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>());
    //
    //     _reportRepository.VerifyAll();
    //
    //     Assert.IsTrue(expectedRepositoryResponse.SequenceEqual(actualResponse));
    // }
    //
    // [TestMethod]
    // public void GetAllMaintenanceRequestsByRequestHandler_ExceptionThrown()
    // {
    //     _reportRepository.Setup(reportRepository =>
    //             reportRepository.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>()))
    //         .Throws(new Exception());
    //
    //     Assert.ThrowsException<UnknownServiceException>(() =>
    //         _reportService.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>()));
    //
    //     _reportRepository.VerifyAll();
    // }
    //
    // #endregion
    //
    // #region Get all maintenance requests by category
    //
    // [TestMethod]
    // public void GetAllMaintenanceRequestsByCategory_ReportsAreReturned()
    // {
    //     IEnumerable<Report> expectedRepositoryResponse = new List<Report>
    //     {
    //         new Report()
    //         {
    //             IdOfResourceToReport = Guid.NewGuid(),
    //             OnAttendanceRequests = 1,
    //             OpenRequests = 23,
    //             ClosedRequests = 10,
    //         },
    //         new Report()
    //         {
    //             IdOfResourceToReport = Guid.NewGuid(),
    //             OnAttendanceRequests = 2,
    //             OpenRequests = 20,
    //             ClosedRequests = 12,
    //         }
    //     };
    //
    //     _reportRepository.Setup(reportRepository => reportRepository.GetAllMaintenanceRequestsByBuilding())
    //         .Returns(expectedRepositoryResponse);
    //
    //     IEnumerable<Report> actualResponse = _reportService.GetAllMaintenanceRequestsByBuilding();
    //
    //     _reportRepository.VerifyAll();
    //
    //     Assert.IsTrue(expectedRepositoryResponse.SequenceEqual(actualResponse));
    // }
    //
    // [TestMethod]
    // public void GetAllMaintenanceRequestsByCategory_NoReportsAreReturned()
    // {
    //     IEnumerable<Report> expectedRepositoryResponse = new List<Report>();
    //
    //     _reportRepository.Setup(reportRepository => reportRepository.GetAllMaintenanceRequestsByBuilding())
    //         .Returns(expectedRepositoryResponse);
    //
    //     IEnumerable<Report> actualResponse = _reportService.GetAllMaintenanceRequestsByBuilding();
    //
    //     _reportRepository.VerifyAll();
    //
    //     Assert.IsTrue(expectedRepositoryResponse.SequenceEqual(actualResponse));
    // }
    //
    // [TestMethod]
    // public void GetAllMaintenanceRequestsByCategory_ExceptionThrown()
    // {
    //     _reportRepository.Setup(reportRepository => reportRepository.GetAllMaintenanceRequestsByBuilding())
    //         .Throws(new Exception());
    //
    //     Assert.ThrowsException<UnknownServiceException>(() => _reportService.GetAllMaintenanceRequestsByBuilding());
    //
    //     _reportRepository.VerifyAll();
    // }
    //
    // #endregion
}