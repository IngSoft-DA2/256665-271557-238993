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
    private Mock<IReportAdapter> _reportAdapter;
    private ReportController _reportController;

    [TestInitialize]
    public void Initialize()
    {
        _reportAdapter = new Mock<IReportAdapter>(MockBehavior.Strict);
        _reportController = new ReportController(_reportAdapter.Object);
    }

    [TestMethod]
    public void GetRequestsByBuilding_OkIsReturned()
    {
        IEnumerable<GetMaintenanceReportResponse> expectedResponseValue = new List<GetMaintenanceReportResponse>()
        {
            new GetMaintenanceReportResponse()
            {
                IdOfResourceToReport = Guid.NewGuid(),
                OpenRequests = 10,
                ClosedRequests = 5,
                OnAttendanceRequests = 8
            }
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedResponseValue);

        _reportAdapter
            .Setup(adapter => adapter.GetRequestsByBuilding(It.IsAny<Guid>(), It.IsAny<GetMaintenanceReportRequest>()))
            .Returns(expectedResponseValue);

        IActionResult controllerResponse =
            _reportController.GetRequestsByBuilding(It.IsAny<Guid>(), It.IsAny<GetMaintenanceReportRequest>());

        _reportAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetMaintenanceReportResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetMaintenanceReportResponse>;

        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(expectedResponseValue));
    }

    [TestMethod]
    public void GetRequestsByBuilding_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        _reportAdapter.Setup(adapter => adapter.GetRequestsByBuilding(It.IsAny<Guid>(),
            It.IsAny<GetMaintenanceReportRequest>())).Throws(new Exception("Something went wrong"));

        IActionResult controllerResponse = _reportController.GetRequestsByBuilding(It.IsAny<Guid>(),
            It.IsAny<GetMaintenanceReportRequest>());

        _reportAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    [TestMethod]
    public void GetRequestsByJanitor_OkIsReturned()
    {
        IEnumerable<GetMaintenanceReportResponse> expectedResponseValue = new List<GetMaintenanceReportResponse>()
        {
            new GetMaintenanceReportResponse()
            {
                IdOfResourceToReport = Guid.NewGuid(),
                OpenRequests = 10,
                ClosedRequests = 5,
                OnAttendanceRequests = 8
            }
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedResponseValue);

        _reportAdapter
            .Setup(adapter => adapter.GetRequestsByJanitor(It.IsAny<Guid>(), It.IsAny<GetMaintenanceReportRequest>()))
            .Returns(expectedResponseValue);

        IActionResult controllerResponse =
            _reportController.GetRequestsByJanitor(It.IsAny<Guid>(), It.IsAny<GetMaintenanceReportRequest>());

        _reportAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetMaintenanceReportResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetMaintenanceReportResponse>;

        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(expectedResponseValue));
    }
    
    [TestMethod]
    public void GetRequestsByJanitor_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        _reportAdapter.Setup(adapter => adapter.GetRequestsByJanitor(It.IsAny<Guid>(),
            It.IsAny<GetMaintenanceReportRequest>())).Throws(new Exception("Something went wrong"));

        IActionResult controllerResponse = _reportController.GetRequestsByJanitor(It.IsAny<Guid>(),
            It.IsAny<GetMaintenanceReportRequest>());

        _reportAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
    
    [TestMethod]
    public void GetReportsByCategory_OkIsReturned()
    {
        IEnumerable<GetMaintenanceReportResponse> expectedResponseValue = new List<GetMaintenanceReportResponse>()
        {
            new GetMaintenanceReportResponse()
            {
                IdOfResourceToReport = Guid.NewGuid(),
                OpenRequests = 10,
                ClosedRequests = 5,
                OnAttendanceRequests = 8
            }
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedResponseValue);

        _reportAdapter
            .Setup(adapter => adapter.GetRequestsByCategory(It.IsAny<Guid>(), It.IsAny<GetMaintenanceReportRequest>()))
            .Returns(expectedResponseValue);

        IActionResult controllerResponse =
            _reportController.GetRequestsByCategory(It.IsAny<Guid>(), It.IsAny<GetMaintenanceReportRequest>());

        _reportAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetMaintenanceReportResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetMaintenanceReportResponse>;

        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(expectedResponseValue));
    }
    
    [TestMethod]
    public void GetReportsByCategory_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        _reportAdapter.Setup(adapter => adapter.GetRequestsByCategory(It.IsAny<Guid>(),
            It.IsAny<GetMaintenanceReportRequest>())).Throws(new Exception("Something went wrong"));

        IActionResult controllerResponse = _reportController.GetRequestsByCategory(It.IsAny<Guid>(),
            It.IsAny<GetMaintenanceReportRequest>());

        _reportAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
}