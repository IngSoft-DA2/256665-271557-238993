using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.ReportRequests;
using WebModel.Responses.ReportResponses;

namespace Test.ApiControllers;

[TestClass]

public class ReportControllerTest
{

    [TestMethod]
    public void GetRequestsByBuildingTest()
    {
        IEnumerable<GetMaintenanceReportResponse> expectedResponseValue = new List<GetMaintenanceReportByBuildingResponse>()
        {
            new GetMaintenanceReportByBuildingResponse()
            {
                Building = Guid.NewGuid(),
                OpenRequests = 10,
                ClosedRequests = 5,
                OnAttendanceRequests = 8
            }
        };
        
        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedResponseValue);
            
        Mock<IReportAdapter> reportAdapter = new Mock<IReportAdapter>();
        reportAdapter.Setup(adapter => adapter.GetRequestsByBuilding(It.IsAny<Guid>(), It.IsAny<GetMaintenanceReportRequest>())).Returns(expectedResponseValue);
        
        ReportController reportController = new ReportController(reportAdapter.Object);
        IActionResult controllerResponse = reportController.GetRequestsByBuilding(It.IsAny<Guid>(), It.IsAny<GetMaintenanceReportRequest>());
        
        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);
        
        List<GetMaintenanceReportByBuildingResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetMaintenanceReportByBuildingResponse>;
        
        Assert.IsNotNull(controllerResponseValueCasted);
        
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(expectedResponseValue));
        
    }
    
}