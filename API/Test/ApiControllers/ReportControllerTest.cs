using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Responses.ReportResponses;

namespace Test.ApiControllers;

[TestClass]
public class ReportControllerTest
{
    #region Initizing aspects
    
    private Mock<IReportAdapter> _reportAdapter;
    private ReportController _reportController;
    private Guid _sampleBuildingId;
    private Guid _sampleRequestHandlerId;
    private Guid _sampleCategoryId;

    [TestInitialize]
    public void Initialize()
    {
        _reportAdapter = new Mock<IReportAdapter>(MockBehavior.Strict);
        _reportController = new ReportController(_reportAdapter.Object);
        _sampleBuildingId = Guid.NewGuid();
        _sampleRequestHandlerId = Guid.NewGuid();
        _sampleCategoryId = Guid.NewGuid();
    }
    
    #endregion
    
    #region GetMaintenanceRequestsByBuilding

    [TestMethod]
    public void GetMaintenanceRequestsByBuilding_OkIsReturned()
    {
        IEnumerable<GetMaintenanceReportByBuildingResponse> expectedResponseValue = new List<GetMaintenanceReportByBuildingResponse>()
        {
            new GetMaintenanceReportByBuildingResponse()
            {
                BuildingId = _sampleBuildingId,
                OpenRequests = 10,
                ClosedRequests = 5,
                OnAttendanceRequests = 8
            }
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedResponseValue);

        _reportAdapter.Setup(adapter => 
            adapter.GetMaintenanceReportByBuilding(It.IsAny<Guid>(),It.IsAny<Guid>())).Returns(expectedResponseValue);

        IActionResult controllerResponse =
            _reportController.GetMaintenanceRequestsByBuilding(It.IsAny<Guid>(), _sampleBuildingId);

        _reportAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetMaintenanceReportByBuildingResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetMaintenanceReportByBuildingResponse>;

        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(expectedResponseValue));
    }
    
    [TestMethod]
    public void GetMaintenanceRequestsByBuilding_NoReportsAreReturned()
    {
        IEnumerable<GetMaintenanceReportByBuildingResponse> expectedResponseValue = new List<GetMaintenanceReportByBuildingResponse>();

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedResponseValue);

        _reportAdapter.Setup(adapter => adapter.GetMaintenanceReportByBuilding(It.IsAny<Guid>(),It.IsAny<Guid>())).Returns(expectedResponseValue);

        IActionResult controllerResponse = _reportController.GetMaintenanceRequestsByBuilding(It.IsAny<Guid>(), _sampleBuildingId);

        _reportAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetMaintenanceReportByBuildingResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetMaintenanceReportByBuildingResponse>;

        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(expectedResponseValue));
    }

    [TestMethod]
    public void GetMaintenanceRequestsByBuilding_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        _reportAdapter.Setup(adapter => adapter.GetMaintenanceReportByBuilding(It.IsAny<Guid>(),It.IsAny<Guid>())).Throws(new Exception("Something went wrong"));

        IActionResult controllerResponse = _reportController.GetMaintenanceRequestsByBuilding(It.IsAny<Guid>(),_sampleBuildingId);

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
        IEnumerable<GetMaintenanceReportByRequestHandlerResponse> expectedResponseValue = new List<GetMaintenanceReportByRequestHandlerResponse>()
        {
            new GetMaintenanceReportByRequestHandlerResponse()
            {
                RequestHandlerId = _sampleRequestHandlerId,
                OpenRequests = 10,
                ClosedRequests = 5,
                OnAttendanceRequests = 8,
                AverageTimeToCloseRequest = TimeSpan.FromDays(3)
            }
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedResponseValue);

        _reportAdapter.Setup(adapter => adapter.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>(),It.IsAny<Guid>(),It.IsAny<Guid>())).Returns(expectedResponseValue);

        IActionResult controllerResponse = _reportController.GetMaintenanceRequestsByRequestHandler(_sampleRequestHandlerId,It.IsAny<Guid>(), It.IsAny<Guid>());

        _reportAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetMaintenanceReportByRequestHandlerResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetMaintenanceReportByRequestHandlerResponse>;

        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(expectedResponseValue));
    }
    
    [TestMethod]
    public void GetMaintenanceRequestsByRequestHandler_NoReportsAreReturned()
    {
        IEnumerable<GetMaintenanceReportByRequestHandlerResponse> expectedResponseValue = new List<GetMaintenanceReportByRequestHandlerResponse>();

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedResponseValue);

        _reportAdapter.Setup(adapter => adapter.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>(),It.IsAny<Guid>(),It.IsAny<Guid>())).Returns(expectedResponseValue);

        IActionResult controllerResponse = _reportController.GetMaintenanceRequestsByRequestHandler(_sampleRequestHandlerId, It.IsAny<Guid>(), It.IsAny<Guid>());

        _reportAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetMaintenanceReportByRequestHandlerResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetMaintenanceReportByRequestHandlerResponse>;

        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(expectedResponseValue));
    }
    
    [TestMethod]
    public void GetMaintenanceRequestsByRequestHandler_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        _reportAdapter.Setup(adapter => adapter.GetMaintenanceReportByRequestHandler(It.IsAny<Guid>(), It.IsAny<Guid>(),It.IsAny<Guid>())).Throws(new Exception("Something went wrong"));

        IActionResult controllerResponse = _reportController.GetMaintenanceRequestsByRequestHandler(_sampleRequestHandlerId,It.IsAny<Guid>(), It.IsAny<Guid>());

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
        IEnumerable<GetMaintenanceReportByCategoryResponse> expectedResponseValue = new List<GetMaintenanceReportByCategoryResponse>()
        {
            new GetMaintenanceReportByCategoryResponse()
            {
                CategoryId = _sampleCategoryId,
                OpenRequests = 10,
                ClosedRequests = 5,
                OnAttendanceRequests = 8
            }
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedResponseValue);

        _reportAdapter
            .Setup(adapter => adapter.GetMaintenanceReportByCategory(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Returns(expectedResponseValue);

        IActionResult controllerResponse =
            _reportController.GetMaintenanceRequestsByCategory(It.IsAny<Guid>(), _sampleCategoryId);

        _reportAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetMaintenanceReportByCategoryResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetMaintenanceReportByCategoryResponse>;

        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(expectedResponseValue));
    }
    
    [TestMethod]
    public void GetMaintenanceRequestsByCategory_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        _reportAdapter.Setup(adapter => adapter.GetMaintenanceReportByCategory(It.IsAny<Guid>(), It.IsAny<Guid>())).Throws(new Exception("Something went wrong"));

        IActionResult controllerResponse = _reportController.GetMaintenanceRequestsByCategory(It.IsAny<Guid>(), _sampleCategoryId);

        _reportAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
    
    #endregion

}