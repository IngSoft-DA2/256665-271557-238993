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
    
    #endregion
}