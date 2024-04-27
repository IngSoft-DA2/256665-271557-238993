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
    #region Initizing aspects
    
    private Mock<IReportAdapter> _reportAdapter;
    private ReportController _reportController;

    [TestInitialize]
    public void Initialize()
    {
        _reportAdapter = new Mock<IReportAdapter>(MockBehavior.Strict);
        _reportController = new ReportController(_reportAdapter.Object);
    }
    
    #endregion
    
    #region GetMaintenanceRequestsByCategory

    [TestMethod]
    public void GetMaintenanceRequestsByBuilding_OkIsReturned()
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

        _reportAdapter.Setup(adapter => 
            adapter.GetMaintenanceRequestsByBuilding(It.IsAny<GetMaintenanceReportByBuildingRequest>())).Returns(expectedResponseValue);

        IActionResult controllerResponse =
            _reportController.GetMaintenanceRequestsByBuilding(It.IsAny<GetMaintenanceReportByBuildingRequest>());

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
    public void GetMaintenanceRequestsByBuilding_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        _reportAdapter.Setup(adapter => 
            adapter.GetMaintenanceRequestsByBuilding(It.IsAny<GetMaintenanceReportByBuildingRequest>())).Throws(new Exception("Something went wrong"));

        IActionResult controllerResponse = _reportController.GetMaintenanceRequestsByBuilding(It.IsAny<GetMaintenanceReportByBuildingRequest>());

        _reportAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
    
    #endregion

    #region GetMaintenanceRequestsByRequestHandler
    
    [TestMethod]
    public void GetMaintenanceRequestsByRequestHandler_OkIsReturned()
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
            .Setup(adapter => adapter.GetMaintenanceRequestsByRequestHandler(It.IsAny<GetMaintenanceReportRequest>()))
            .Returns(expectedResponseValue);

        IActionResult controllerResponse =
            _reportController.GetMaintenanceRequestsByRequestHandler(It.IsAny<GetMaintenanceReportRequest>());

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
    public void GetMaintenanceRequestsByRequestHandler_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        _reportAdapter.Setup(adapter => adapter.GetMaintenanceRequestsByRequestHandler(It.IsAny<GetMaintenanceReportRequest>())).Throws(new Exception("Something went wrong"));

        IActionResult controllerResponse = _reportController.GetMaintenanceRequestsByRequestHandler(It.IsAny<GetMaintenanceReportRequest>());

        _reportAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
    
    #endregion
    
    #region GetMaintenanceRequestsByCategory
    
    [TestMethod]
    public void GetMaintenanceRequestsByCategory_OkIsReturned()
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
            .Setup(adapter => adapter.GetMaintenanceRequestsByCategory(It.IsAny<Guid>(), It.IsAny<GetMaintenanceReportRequest>()))
            .Returns(expectedResponseValue);

        IActionResult controllerResponse =
            _reportController.GetMaintenanceRequestsByCategory(It.IsAny<Guid>(), It.IsAny<GetMaintenanceReportRequest>());

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
    public void GetMaintenanceRequestsByCategory_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        _reportAdapter.Setup(adapter => adapter.GetMaintenanceRequestsByCategory(It.IsAny<Guid>(),
            It.IsAny<GetMaintenanceReportRequest>())).Throws(new Exception("Something went wrong"));

        IActionResult controllerResponse = _reportController.GetMaintenanceRequestsByCategory(It.IsAny<Guid>(),
            It.IsAny<GetMaintenanceReportRequest>());

        _reportAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
    
    #endregion

    #region Get Maintenance Report By Building
    
    [TestMethod]
    public void GetMaintenanceReportByBuilding_OkIsReturned()
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

        _reportAdapter.Setup(adapter => adapter.GetMaintenanceRequestsByBuilding(
            It.IsAny<GetMaintenanceReportByBuildingRequest>())).Returns(expectedResponseValue);

        IActionResult controllerResponse =
            _reportController.GetMaintenanceRequestsByBuilding(It.IsAny<GetMaintenanceReportByBuildingRequest>());

        _reportAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetMaintenanceReportResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetMaintenanceReportResponse>;

        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(expectedResponseValue));
    }
    
    
    
    #endregion
}