using System.Collections;
using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using WebModel.Requests.ReportRequests;
using WebModel.Responses.ReportResponses;

namespace Test.Adapters;

[TestClass]
public class ReportAdapterTest
{
    #region Initialize
    
    private Mock<IReportService> _reportService;
    private ReportAdapter _reportAdapter;
    
    [TestInitialize]
    public void Initialize()
    {
        _reportService = new Mock<IReportService>(MockBehavior.Strict);
        _reportAdapter = new ReportAdapter(_reportService.Object);
    }

    #endregion
    
    #region Get Maintenance Reports By Building

    [TestMethod]
    public void GetMaintenanceReportByBuilding_ReturnsGetMaintenanceReportResponse()
    {
        IEnumerable<GetMaintenanceReportByCategoryResponse> expectedAdapterResponse = new List<GetMaintenanceReportByCategoryResponse>()
        {
            new GetMaintenanceReportByCategoryResponse()
            {
                CategoryId = Guid.NewGuid(),
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
            service.GetMaintenanceReportByBuilding(It.IsAny<GetMaintenanceReportByCategoryResponse>())).Returns(expectedServiceResponse);

        IEnumerable<GetMaintenanceReportByCategoryResponse> adapterResponse = 
            _reportAdapter.GetMaintenanceReportByBuilding(It.IsAny<GetMaintenanceReportByCategoryResponse>());

        _reportService.VerifyAll();

        Assert.AreEqual(expectedAdapterResponse.Count(), adapterResponse.Count());
        Assert.IsTrue(expectedAdapterResponse.Equals(adapterResponse));
    }
    
    [TestMethod]
    public void GetMaintenanceReportByBuilding_ThrowsUnknownAdapterException()
    {
        _reportService.Setup(service => 
            service.GetMaintenanceReportByBuilding(It.IsAny<GetMaintenanceReportByCategoryResponse>())).Throws<Exception>();

        Assert.ThrowsException<UnknownAdapterException>(() => 
            _reportAdapter.GetMaintenanceReportByBuilding(It.IsAny<GetMaintenanceReportByCategoryResponse>()));
    }
    #endregion

    #region Get Maintenance Reports By Request Handler

    [TestMethod]
    public void GetMaintenanceReportByRequestHandler_ReturnsGetMaintenanceReportResponse()
    {
        IEnumerable<GetMaintenanceReportByRequestHandlerResponse> expectedAdapterResponse = new List<GetMaintenanceReportByRequestHandlerResponse>()
        {
            new GetMaintenanceReportByRequestHandlerResponse()
            {
                RequestHandlerId = Guid.NewGuid(),
                OpenRequests = 10,
                ClosedRequests = 5,
                OnAttendanceRequests = 0,
                AverageTimeToCloseRequest = 4
            }
        };
        IEnumerable<RequestHandlerReport> expectedServiceResponse = new List<RequestHandlerReport>()
        {
            new RequestHandlerReport
            {
                RequestHandlerId = expectedAdapterResponse.First().RequestHandlerId,
                OpenRequests = expectedAdapterResponse.First().OpenRequests,
                ClosedRequests = expectedAdapterResponse.First().ClosedRequests,
                OnAttendanceRequests = expectedAdapterResponse.First().OnAttendanceRequests,
                AvgTimeToCloseRequest = expectedAdapterResponse.First().AverageTimeToCloseRequest
            }
        };

        _reportService.Setup(service => 
            service.GetMaintenanceReportByRequestHandler(It.IsAny<GetMaintenanceReportByRequestHandlerResponse>())).Returns(expectedServiceResponse);

        IEnumerable<GetMaintenanceReportByRequestHandlerResponse> adapterResponse = 
            _reportAdapter.GetMaintenanceReportByRequestHandler(It.IsAny<GetMaintenanceReportByRequestHandlerResponse>());

        _reportService.VerifyAll();

        Assert.AreEqual(expectedAdapterResponse.Count(), adapterResponse.Count());
        Assert.IsTrue(expectedAdapterResponse.Equals(adapterResponse));
    }
    
    
    #endregion
    
    #region Get Maintenance Reports By Category 
    
    [TestMethod]
    public void GetMaintenanceReportByCategory_ReturnsGetMaintenanceReportResponse()
    {
        IEnumerable<GetMaintenanceReportByCategoryResponse> expectedAdapterResponse = new List<GetMaintenanceReportByCategoryResponse>()
        {
            new GetMaintenanceReportByCategoryResponse()
            {
                CategoryId = Guid.NewGuid(),
                OpenRequests = 10,
                ClosedRequests = 5,
                OnAttendanceRequests = 0,
            }
        };
        IEnumerable<Report> expectedServiceResponse = new List<Report>()
        {
            new Report
            {
                IdOfResourceToReport = expectedAdapterResponse.First().CategoryId,
                OpenRequests = expectedAdapterResponse.First().OpenRequests,
                ClosedRequests = expectedAdapterResponse.First().ClosedRequests,
                OnAttendanceRequests = expectedAdapterResponse.First().OnAttendanceRequests,
            }
        };

        _reportService.Setup(service => 
            service.GetMaintenanceReportByCategory(It.IsAny<GetMaintenanceReportByCategoryResponse>())).Returns(expectedServiceResponse);

        IEnumerable<GetMaintenanceReportByCategoryResponse> adapterResponse = 
            _reportAdapter.GetMaintenanceReportByCategory(It.IsAny<GetMaintenanceReportByCategoryResponse>());

        _reportService.VerifyAll();

        Assert.AreEqual(expectedAdapterResponse.Count(), adapterResponse.Count());
        Assert.IsTrue(expectedAdapterResponse.Equals(adapterResponse));
    }
    
    [TestMethod]
    public void GetMaintenanceReportByCategory_ThrowsUnknownAdapterException()
    {
        _reportService.Setup(service => 
            service.GetMaintenanceReportByCategory(It.IsAny<GetMaintenanceReportByCategoryResponse>())).Throws<Exception>();

        Assert.ThrowsException<UnknownAdapterException>(() => 
            _reportAdapter.GetMaintenanceReportByCategory(It.IsAny<GetMaintenanceReportByCategoryResponse>()));
    }
    
    #endregion
}