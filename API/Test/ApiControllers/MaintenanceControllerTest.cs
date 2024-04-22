using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using Moq;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.MaintenanceRequests;
using WebModel.Responses.MaintenanceResponses;

namespace Test.ApiControllers;

[TestClass]
public class MaintenanceControllerTest
{
    #region Test Initialize
    private Mock<IMaintenanceAdapter> _maintenanceAdapter;
    private MaintenanceController _maintenanceController;
    private GetMaintenanceRequestResponse _expectedMaintenanceRequest;
    private Guid _idFromRoute;
    private Guid _categoryFromRoute;
    private ObjectResult _expectedControllerResponse;
        [TestInitialize]
        public void Initialize()
        {
            _maintenanceAdapter = new Mock<IMaintenanceAdapter>(MockBehavior.Strict);
            _maintenanceController = new MaintenanceController(_maintenanceAdapter.Object);
            _expectedMaintenanceRequest = new GetMaintenanceRequestResponse()
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                Description = "Repair elevator light",
                FlatId = Guid.NewGuid(),
                Category = Guid.NewGuid(),
                RequestStatus = StatusEnumMaintenanceResponse.InProgress
            };
            _idFromRoute = Guid.NewGuid();
            _expectedControllerResponse = new ObjectResult("Internal Server Error");
            _expectedControllerResponse.StatusCode = 500;
        }
        
    #endregion
    
    #region Get All Maintenance Requests

    [TestMethod]
    public void GetAllMaintenanceRequests_200CodeIsReturned()
    {
        IEnumerable<GetMaintenanceRequestResponse> expectedMaintenanceRequests =
            new List<GetMaintenanceRequestResponse>()
            {
                _expectedMaintenanceRequest,
                new GetMaintenanceRequestResponse()
                {
                    Id = Guid.NewGuid(),
                    BuildingId = Guid.NewGuid(),
                    Description = "Fix leaky faucet",
                    FlatId = Guid.NewGuid(),
                    Category = Guid.NewGuid(),
                    RequestStatus = StatusEnumMaintenanceResponse.Closed
                }
            };
        _maintenanceAdapter.Setup(adapter => adapter.GetAllMaintenanceRequests()).Returns(expectedMaintenanceRequests.ToList());
        
        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedMaintenanceRequests);
        
        IActionResult controllerResponse = _maintenanceController.GetAllMaintenanceRequests();
        
        _maintenanceAdapter.VerifyAll();
        
        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);
        
        List<GetMaintenanceRequestResponse>? controllerResponseValueCasted = 
            controllerResponseCasted.Value as List<GetMaintenanceRequestResponse>;
        Assert.IsNotNull(controllerResponseValueCasted);
        
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedMaintenanceRequests.SequenceEqual(controllerResponseValueCasted));
    }
    
    [TestMethod]
    public void GetAllMaintenanceRequests_500CodeIsReturned()
    {
        _maintenanceAdapter.Setup(adapter => adapter.GetAllMaintenanceRequests()).Throws(new Exception());
        
        IActionResult controllerResponse = _maintenanceController.GetAllMaintenanceRequests();
        
        _maintenanceAdapter.VerifyAll();
        
        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(_expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(_expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
    
    
    #endregion
    
    #region Get Maintenance Request By Category

    [TestMethod]
    public void GetMaintenanceRequestByCategory_200CodeIsReturned()
    {
        OkObjectResult expectedControllerResponse = new OkObjectResult(_expectedMaintenanceRequest);
        
        _maintenanceAdapter.Setup(adapter => adapter.GetMaintenanceRequestByCategory(_categoryFromRoute))
            .Returns(_expectedMaintenanceRequest);
        
        IActionResult controllerResponse = _maintenanceController.GetMaintenanceRequestByCategory(_categoryFromRoute);
        
        _maintenanceAdapter.VerifyAll();
        
        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);
        
        GetMaintenanceRequestResponse? controllerResponseValueCasted = 
            controllerResponseCasted.Value as GetMaintenanceRequestResponse;
        Assert.IsNotNull(controllerResponseValueCasted);
        
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(_expectedMaintenanceRequest.Equals(controllerResponseValueCasted));
    }

    [TestMethod]
    public void GetMaintenanceRequestByCategory_404CodeIsReturned()
    {
        NotFoundObjectResult expectedControllerResponse = new NotFoundObjectResult("Maintenance request was not found, reload the page");
        
        _maintenanceAdapter.Setup(adapter => adapter.GetMaintenanceRequestByCategory(_categoryFromRoute))
            .Throws(new ObjectNotFoundException());
        
        IActionResult controllerResponse = _maintenanceController.GetMaintenanceRequestByCategory(_categoryFromRoute);
        
        _maintenanceAdapter.VerifyAll();
        
        NotFoundObjectResult? controllerResponseCasted = controllerResponse as NotFoundObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(controllerResponseCasted.Value, expectedControllerResponse.Value);
        Assert.AreEqual(controllerResponseCasted.StatusCode, expectedControllerResponse.StatusCode);
    }
    
    [TestMethod]
    public void GetMaintenanceRequestByCategory_500CodeIsReturned()
    {
        _maintenanceAdapter.Setup(adapter => adapter.GetMaintenanceRequestByCategory(It.IsAny<Guid>()))
            .Throws(new Exception("Database Broken"));
        
        IActionResult controllerResponse = _maintenanceController.GetMaintenanceRequestByCategory(It.IsAny<Guid>());
        
        _maintenanceAdapter.VerifyAll();
        
        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(_expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(_expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
    #endregion
    
    #region Request Maintenance
    
    [TestMethod]
    public void RequestMaintenance_200CodeIsReturned()
    {
        CreateRequestMaintenanceResponse expectedResponse = new CreateRequestMaintenanceResponse()
        {
            Id = Guid.NewGuid(),
            BuildingId = Guid.NewGuid(),
            Description = "Repair elevator light",
            FlatId = Guid.NewGuid(),
            Category = Guid.NewGuid(),
            RequestStatus = StatusEnumMaintenanceResponse.Open
        };
        
        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedResponse);
        
        _maintenanceAdapter.Setup(adapter => 
                adapter.CreateMaintenanceRequest(It.IsAny<CreateRequestMaintenanceRequest>())).Returns(expectedResponse);
        
        IActionResult controllerResponse = _maintenanceController.CreateMaintenanceRequest(It.IsAny<CreateRequestMaintenanceRequest>());
        _maintenanceAdapter.VerifyAll();
        
        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);
        
        CreateRequestMaintenanceResponse? controllerResponseValueCasted = 
            controllerResponseCasted.Value as CreateRequestMaintenanceResponse;
        Assert.IsNotNull(controllerResponseValueCasted);
        
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedResponse.Equals(controllerResponseValueCasted));
    }
    
    [TestMethod]
    public void RequestMaintenance_400CodeIsReturned()
    {
        BadRequestObjectResult expectedControllerResponse = new BadRequestObjectResult("Bad Request");
        
        _maintenanceAdapter.Setup(adapter => 
                adapter.CreateMaintenanceRequest(It.IsAny<CreateRequestMaintenanceRequest>())).Throws(new ObjectErrorException("Bad Request"));
        
        IActionResult controllerResponse = _maintenanceController.CreateMaintenanceRequest(It.IsAny<CreateRequestMaintenanceRequest>());
        
        _maintenanceAdapter.VerifyAll();
        
        BadRequestObjectResult? controllerResponseCasted = controllerResponse as BadRequestObjectResult;
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
    
    [TestMethod]
    public void RequestMaintenance_500CodeIsReturned()
    {
        _maintenanceAdapter.Setup(adapter => 
                adapter.CreateMaintenanceRequest(It.IsAny<CreateRequestMaintenanceRequest>())).Throws(new Exception());
        
        IActionResult controllerResponse = _maintenanceController.CreateMaintenanceRequest(It.IsAny<CreateRequestMaintenanceRequest>());
        
        _maintenanceAdapter.VerifyAll();
        
        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(_expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(_expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
    
    #endregion
    
    #region Assign Maintenance Request

    [TestMethod]
    public void AssignMaintenanceRequest_200CodeIsReturned()
    {
        AssignMaintenanceRequestResponse expectedResponse = new AssignMaintenanceRequestResponse()
        {
            Id = Guid.NewGuid(),
            WorkerId = Guid.NewGuid()
        };
        
        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedResponse);
        
        _maintenanceAdapter.Setup(adapter => 
            adapter.AssignMaintenanceRequest(It.IsAny<AssignMaintenanceRequestRequest>())).Returns(expectedResponse);
        
        IActionResult controllerResponse = _maintenanceController.AssignMaintenanceRequest(It.IsAny<AssignMaintenanceRequestRequest>());
        
        _maintenanceAdapter.VerifyAll();
        
        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);
        
        AssignMaintenanceRequestResponse? controllerResponseValueCasted = 
            controllerResponseCasted.Value as AssignMaintenanceRequestResponse;
        Assert.IsNotNull(controllerResponseValueCasted);
        
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedResponse.Equals(controllerResponseValueCasted));
    }
    [TestMethod]
    public void AssignMaintenanceRequest_400CodeIsReturned()
    {
        BadRequestObjectResult expectedControllerResponse = new BadRequestObjectResult("Bad Request");
        
        _maintenanceAdapter.Setup(adapter => 
            adapter.AssignMaintenanceRequest(It.IsAny<AssignMaintenanceRequestRequest>())).Throws(new ObjectErrorException("Bad Request"));
        
        IActionResult controllerResponse = _maintenanceController.AssignMaintenanceRequest(It.IsAny<AssignMaintenanceRequestRequest>());
        
        _maintenanceAdapter.VerifyAll();
        
        BadRequestObjectResult? controllerResponseCasted = controllerResponse as BadRequestObjectResult;
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
    [TestMethod]
    public void AssignMaintenanceRequest_404CodeIsReturned()
    {
        NotFoundObjectResult expectedControllerResponse = new NotFoundObjectResult("Maintenance request was not found, reload the page");
        
        _maintenanceAdapter.Setup(adapter => 
            adapter.AssignMaintenanceRequest(It.IsAny<AssignMaintenanceRequestRequest>())).Throws(new ObjectNotFoundException());
        
        IActionResult controllerResponse = _maintenanceController.AssignMaintenanceRequest(It.IsAny<AssignMaintenanceRequestRequest>());
        
        _maintenanceAdapter.VerifyAll();
        
        NotFoundObjectResult? controllerResponseCasted = controllerResponse as NotFoundObjectResult;
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    



    #endregion

    

}