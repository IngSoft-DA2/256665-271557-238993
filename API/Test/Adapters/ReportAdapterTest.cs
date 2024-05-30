using System.Collections;
using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.ReportResponses;

namespace Test.Adapters;

[TestClass]
public class ReportAdapterTest
{
    #region Initialize

    private Mock<IReportService> _reportService;
    private ReportAdapter _reportAdapter;
    private Guid _sampleBuildingId;
    private Guid _sampleRequestHandlerId;
    private Guid _sampleCategoryId;

    [TestInitialize]
    public void Initialize()
    {
        _reportService = new Mock<IReportService>(MockBehavior.Strict);
        _reportAdapter = new ReportAdapter(_reportService.Object);
        _sampleBuildingId = Guid.NewGuid();
        _sampleRequestHandlerId = Guid.NewGuid();
        _sampleCategoryId = Guid.NewGuid();
    }

    #endregion

    #region Get Maintenance Reports By Building

    [TestMethod]
    public void GetMaintenanceReportByBuilding_ReturnsGetMaintenanceReportResponse()
    {
        IEnumerable<GetMaintenanceReportByBuildingResponse> expectedAdapterResponse =
            new List<GetMaintenanceReportByBuildingResponse>()
            {
                new GetMaintenanceReportByBuildingResponse()
                {
                    BuildingId = _sampleBuildingId,
                    OpenRequests = 10,
                    ClosedRequests = 5,
                    OnAttendanceRequests = 8
                }
            };
        IEnumerable<Report> expectedServiceResponse = new List<Report>()
        {
            new Report
            {
                IdOfResourceToReport = expectedAdapterResponse.First().BuildingId,
                OpenRequests = expectedAdapterResponse.First().OpenRequests,
                ClosedRequests = expectedAdapterResponse.First().ClosedRequests,
                OnAttendanceRequests = expectedAdapterResponse.First().OnAttendanceRequests
            }
        };

        _reportService.Setup(service =>
                service.GetMaintenanceReportByBuilding(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Returns(expectedServiceResponse);

        IEnumerable<GetMaintenanceReportByBuildingResponse> adapterResponse =
            _reportAdapter.GetMaintenanceReportByBuilding(It.IsAny<Guid>(), _sampleBuildingId);

        _reportService.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }

    [TestMethod]
    public void GetMaintenanceReportByBuilding_ThrowsUnknownAdapterException()
    {
        _reportService.Setup(service =>
            service.GetMaintenanceReportByBuilding(It.IsAny<Guid>(), It.IsAny<Guid>())).Throws<Exception>();

        Assert.ThrowsException<UnknownAdapterException>(() =>
            _reportAdapter.GetMaintenanceReportByBuilding(It.IsAny<Guid>(), It.IsAny<Guid>()));

        _reportService.VerifyAll();
    }

    #endregion

    #region Get Maintenance Reports By Request Handler

    [TestMethod]
    public void GetMaintenanceReportByRequestHandler_ReturnsGetMaintenanceReportResponse()
    {
        IEnumerable<GetMaintenanceReportByRequestHandlerResponse> expectedAdapterResponse =
            new List<GetMaintenanceReportByRequestHandlerResponse>()
            {
                new GetMaintenanceReportByRequestHandlerResponse
                {
                    OpenRequests = 10,
                    ClosedRequests = 5,
                    OnAttendanceRequests = 0,
                    AverageTimeToCloseRequest = TimeSpan.FromDays(3)
                }
            };
        IEnumerable<RequestHandlerReport> expectedServiceResponse = new List<RequestHandlerReport>()
        {
            new RequestHandlerReport
            {
                OpenRequests = expectedAdapterResponse.First().OpenRequests,
                ClosedRequests = expectedAdapterResponse.First().ClosedRequests,
                OnAttendanceRequests = expectedAdapterResponse.First().OnAttendanceRequests,
                AvgTimeToCloseRequest = expectedAdapterResponse.First().AverageTimeToCloseRequest
            }
        };

        _reportService.Setup(service =>
                service.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Returns(expectedServiceResponse);

        IEnumerable<GetMaintenanceReportByRequestHandlerResponse> adapterResponse =
            _reportAdapter.GetMaintenanceReportByRequestHandler(_sampleRequestHandlerId, It.IsAny<Guid>(),
                It.IsAny<Guid>());

        _reportService.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }

    [TestMethod]
    public void GetMaintenanceReportByRequestHandler_ThrowsUnknownAdapterException()
    {
        _reportService.Setup(service =>
                service.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Throws(new Exception("Internal Server Error"));

        Assert.ThrowsException<UnknownAdapterException>(() =>
            _reportAdapter.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()));

        _reportService.VerifyAll();
    }

    #endregion

    #region Get Maintenance Reports By Category

    [TestMethod]
    public void GetMaintenanceReportByCategory_ReturnsGetMaintenanceReportResponse()
    {
        IEnumerable<GetMaintenanceReportByCategoryResponse> expectedAdapterResponse =
            new List<GetMaintenanceReportByCategoryResponse>()
            {
                new GetMaintenanceReportByCategoryResponse()
                {
                    CategoryId = _sampleCategoryId,
                    OpenRequests = 10,
                    ClosedRequests = 5,
                    OnAttendanceRequests = 8
                }
            };
        IEnumerable<Report> expectedServiceResponse = new List<Report>()
        {
            new Report
            {
                IdOfResourceToReport = expectedAdapterResponse.First().CategoryId,
                OpenRequests = expectedAdapterResponse.First().OpenRequests,
                ClosedRequests = expectedAdapterResponse.First().ClosedRequests,
                OnAttendanceRequests = expectedAdapterResponse.First().OnAttendanceRequests
            }
        };

        _reportService.Setup(service =>
                service.GetMaintenanceReportByCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Returns(expectedServiceResponse);

        IEnumerable<GetMaintenanceReportByCategoryResponse> adapterResponse =
            _reportAdapter.GetMaintenanceReportByCategory(It.IsAny<Guid>(), _sampleCategoryId);

        _reportService.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }

    [TestMethod]
    public void GetMaintenanceReportByCategory_ThrowsUnknownAdapterException()
    {
        _reportService.Setup(service =>
                service.GetMaintenanceReportByCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Throws(new Exception("Internal Server Error"));

        Assert.ThrowsException<UnknownAdapterException>(() =>
            _reportAdapter.GetMaintenanceReportByCategory(It.IsAny<Guid>(), It.IsAny<Guid>()));

        _reportService.VerifyAll();
    }

    #endregion
    
    #region Get Flat Requests By Building Report

    [TestMethod]
    public void GetFlatRequestsByBuildingReport_ReturnsGetFlatRequestsReportByBuildingResponse()
    {
        IEnumerable<GetFlatRequestsReportByBuildingResponse> expectedAdapterResponse =
            new List<GetFlatRequestsReportByBuildingResponse>()
            {
                new GetFlatRequestsReportByBuildingResponse()
                {
                    BuildingId = _sampleBuildingId,
                    OpenRequests = 10,
                    ClosedRequests = 5,
                    OnAttendanceRequests = 8,
                    OwnerName = "John Wick"
                }
            };
        IEnumerable<FlatRequestReport> expectedServiceResponse = new List<FlatRequestReport>()
        {
            new FlatRequestReport
            {
                IdOfResourceToReport = expectedAdapterResponse.First().BuildingId,
                OpenRequests = expectedAdapterResponse.First().OpenRequests,
                ClosedRequests = expectedAdapterResponse.First().ClosedRequests,
                OnAttendanceRequests = expectedAdapterResponse.First().OnAttendanceRequests,
                OwnerName = expectedAdapterResponse.First().OwnerName
            }
        };

        _reportService.Setup(service =>
            service.GetFlatRequestsByBuildingReport(It.IsAny<Guid>())).Returns(expectedServiceResponse);

        IEnumerable<GetFlatRequestsReportByBuildingResponse> adapterResponse =
            _reportAdapter.GetFlatRequestsByBuildingReport(_sampleBuildingId);

        _reportService.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }
    
    [TestMethod]
    public void GetFlatRequestsByBuildingReport_ThrowsUnknownAdapterException()
    {
        _reportService.Setup(service =>
            service.GetFlatRequestsByBuildingReport(It.IsAny<Guid>())).Throws(new Exception("Internal Server Error"));

        Assert.ThrowsException<UnknownAdapterException>(() =>
            _reportAdapter.GetFlatRequestsByBuildingReport(It.IsAny<Guid>()));

        _reportService.VerifyAll();
    }
    
    #endregion
}