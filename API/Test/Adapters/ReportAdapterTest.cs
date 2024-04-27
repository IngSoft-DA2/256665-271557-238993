using System.Collections;
using Adapter;
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


    #region Get Maintenance Requests By Building

    [TestMethod]
    public void GetMaintenanceReportByBuilding_ReturnsGetMaintenanceReportResponse()
    {
        IEnumerable<GetMaintenanceReportResponse> expectedAdapterResponse = new List<GetMaintenanceReportResponse>()
        {
            new GetMaintenanceReportResponse()
            {
                IdOfResourceToReport = Guid.NewGuid(),
                OpenRequests = 10,
                ClosedRequests = 5,
                OnAttendanceRequests = 8
            }
        };
        IEnumerable<Report> expectedServiceResponse = new List<Report>()
        {
            new Report
            {
                IdOfResourceToReport = expectedAdapterResponse.First().IdOfResourceToReport,
                OpenRequests = expectedAdapterResponse.First().OpenRequests,
                ClosedRequests = expectedAdapterResponse.First().ClosedRequests,
                OnAttendanceRequests = expectedAdapterResponse.First().OnAttendanceRequests
            }
        };

        _reportService.Setup(service => 
            service.GetMaintenanceReportByBuilding(It.IsAny<GetMaintenanceReportResponse>())).Returns(expectedServiceResponse);

        IEnumerable<GetMaintenanceReportResponse> adapterResponse = 
            _reportAdapter.GetMaintenanceReportByBuilding(It.IsAny<GetMaintenanceReportResponse>());

        _reportService.VerifyAll();
        
        Assert.IsTrue(adapterResponse.Equals(expectedAdapterResponse));
    }
    #endregion
}