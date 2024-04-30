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

    [TestInitialize]
    public void Initialize()
    {
        _reportRepository = new Mock<IReportRepository>(MockBehavior.Strict);
        _reportService = new ReportService(_reportRepository.Object);
    }

    #endregion


    // #region Get maintenance report by building

    [TestMethod]
    public void GetMaintenanceReportByBuilding_ReportsAreReturned()
    {
        Guid buildingId = Guid.NewGuid();
        Guid buildingId2 = Guid.NewGuid();

        IEnumerable<MaintenanceRequest> expectedRepositoryResponse = new List<MaintenanceRequest>
        {
            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                BuildingId = buildingId,
                ClosedDate = DateTime.Now.AddDays(1),
                OpenedDate = DateTime.Now,
                RequestStatus = RequestStatusEnum.Closed,
                Category = Guid.NewGuid(),
                Description = "Bath broken",
            },
            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                BuildingId = buildingId,
                ClosedDate = DateTime.Now.AddDays(3),
                OpenedDate = DateTime.Now,
                RequestStatus = RequestStatusEnum.Closed,
                Category = Guid.NewGuid(),
                Description = "Room broken",
            },

            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                BuildingId = buildingId,
                ClosedDate = DateTime.Now.AddDays(3),
                OpenedDate = DateTime.Now,
                RequestStatus = RequestStatusEnum.Open,
                Category = Guid.NewGuid(),
                Description = "Room broken",
            },
            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                BuildingId = buildingId,
                ClosedDate = DateTime.Now.AddDays(3),
                OpenedDate = DateTime.Now,
                RequestStatus = RequestStatusEnum.Open,
                Category = Guid.NewGuid(),
                Description = "Room broken",
            },

            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                BuildingId = buildingId,
                ClosedDate = DateTime.Now.AddDays(3),
                OpenedDate = DateTime.Now,
                RequestStatus = RequestStatusEnum.Open,
                Category = Guid.NewGuid(),
                Description = "Room broken",
            },

            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                BuildingId = buildingId,
                ClosedDate = DateTime.Now.AddDays(3),
                OpenedDate = DateTime.Now,
                RequestStatus = RequestStatusEnum.InProgress,
                Category = Guid.NewGuid(),
                Description = "Room broken",
            },
            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                BuildingId = buildingId2,
                ClosedDate = DateTime.Now.AddDays(3),
                OpenedDate = DateTime.Now,
                RequestStatus = RequestStatusEnum.InProgress,
                Category = Guid.NewGuid(),
                Description = "Room broken",
            },
        };

        Report reportNode1 = new Report()
        {
            IdOfResourceToReport = buildingId,
            ClosedRequests = 2,
            OpenRequests = 3,
            OnAttendanceRequests = 1
        };
        
        Report reportNode2 = new Report()
        {
            IdOfResourceToReport =buildingId2,
            ClosedRequests = 0,
            OpenRequests = 0,
            OnAttendanceRequests = 1
        };


        IEnumerable<Report> expectedReports = new List<Report>
            {reportNode1, reportNode2};

        _reportRepository.Setup(reportRepository => reportRepository.GetMaintenanceReportByBuilding(It.IsAny<Guid>()))
            .Returns(expectedRepositoryResponse);

        IEnumerable<Report> actualResponse = _reportService.GetMaintenanceReportByBuilding(Guid.Empty);

        _reportRepository.VerifyAll();

        Assert.IsTrue(actualResponse.SequenceEqual(expectedReports));
    }

    // [TestMethod]
    // public void GetMaintenanceReportByBuilding_NoReportsAreReturned()
    // {
    //     IEnumerable<Report> expectedRepositoryResponse = new List<Report>();
    //
    //     _reportRepository.Setup(reportRepository => reportRepository.GetMaintenanceReportByBuilding(It.IsAny<Guid>()))
    //         .Returns(expectedRepositoryResponse);
    //
    //     IEnumerable<Report> actualResponse = _reportService.GetMaintenanceReportByBuilding(It.IsAny<Guid>());
    //
    //     _reportRepository.VerifyAll();
    //
    //     Assert.IsTrue(expectedRepositoryResponse.SequenceEqual(actualResponse));
    // }
    //
    // [TestMethod]
    // public void GetMaintenanceReportByBuilding_ExceptionThrown()
    // {
    //     _reportRepository.Setup(reportRepository => reportRepository.GetMaintenanceReportByBuilding(It.IsAny<Guid>()))
    //         .Throws(new Exception());
    //
    //     Assert.ThrowsException<UnknownServiceException>(() =>
    //         _reportService.GetMaintenanceReportByBuilding(It.IsAny<Guid>()));
    //
    //     _reportRepository.VerifyAll();
    // }
    //
    // #endregion
    //
    // #region Get maintenance report by request handler
    //
    // [TestMethod]
    // public void GetMaintenanceReportByRequestHandler_ReportsAreReturned()
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
    // public void GetMaintenanceReportByRequestHandler_NoReportsAreReturned()
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
    // public void GetMaintenanceReportByRequestHandler_ExceptionThrown()
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
    // #region Get maintenance report by category
    //
    // [TestMethod]
    // public void GetMaintenanceReportByCategory_ReportsAreReturned()
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