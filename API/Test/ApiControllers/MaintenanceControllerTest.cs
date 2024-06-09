using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using Domain;
using Moq;
using IAdapter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.MaintenanceRequests;
using WebModel.Responses.MaintenanceResponses;

namespace Test.ApiControllers;

[TestClass]
public class MaintenanceControllerTest
{
    #region Test Initialize

    private Mock<IMaintenanceRequestAdapter> _maintenanceAdapter;
    private MaintenanceController _maintenanceController;
    private GetMaintenanceRequestResponse _expectedMaintenanceRequest;
    private Guid _idFromRoute;
    private Guid _categoryFromRoute;
    private ObjectResult _expectedControllerResponse;

    [TestInitialize]
    public void Initialize()
    {
        _maintenanceAdapter = new Mock<IMaintenanceRequestAdapter>(MockBehavior.Strict);
        _maintenanceController = new MaintenanceController(_maintenanceAdapter.Object);
        _expectedMaintenanceRequest = new GetMaintenanceRequestResponse()
        {
            Id = Guid.NewGuid(),
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
                    Description = "Fix leaky faucet",
                    FlatId = Guid.NewGuid(),
                    Category = Guid.NewGuid(),
                    RequestStatus = StatusEnumMaintenanceResponse.Closed
                }
            };
        _maintenanceAdapter.Setup(adapter => adapter.GetAllMaintenanceRequests(It.IsAny<Guid>(),It.IsAny<Guid>()))
            .Returns(expectedMaintenanceRequests.ToList());

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedMaintenanceRequests);

        IActionResult controllerResponse = _maintenanceController.GetAllMaintenanceRequests(It.IsAny<Guid>(), It.IsAny<Guid>());

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

    #region Get Maintenance Request By Category

    [TestMethod]
    public void GetMaintenanceRequestByCategory_200CodeIsReturned()
    {
        IEnumerable<GetMaintenanceRequestResponse> expectedMaintenanceRequestsResponse =
            new List<GetMaintenanceRequestResponse>()
            {
                _expectedMaintenanceRequest
            };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedMaintenanceRequestsResponse);

        _maintenanceAdapter.Setup(adapter => adapter.GetMaintenanceRequestByCategory(_categoryFromRoute))
            .Returns(expectedMaintenanceRequestsResponse);

        IActionResult controllerResponse = _maintenanceController.GetMaintenanceRequestByCategory(_categoryFromRoute);

        _maintenanceAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        IEnumerable<GetMaintenanceRequestResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as IEnumerable<GetMaintenanceRequestResponse>;
        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedMaintenanceRequestsResponse.SequenceEqual(controllerResponseValueCasted));
    }

    #endregion

    #region Request Maintenance

    [TestMethod]
    public void RequestMaintenance_201CodeIsReturned()
    {
        CreateRequestMaintenanceRequest dummyRequest = new CreateRequestMaintenanceRequest()
        {
            Description = "Repair elevator light",
            Category = Guid.NewGuid(),
            FlatId = Guid.NewGuid(),
            ManagerId = Guid.NewGuid()
        };
        CreateRequestMaintenanceResponse expectedResponse = new CreateRequestMaintenanceResponse()
        {
            Id = Guid.NewGuid(),
        };
        
        CreatedAtActionResult expectedControllerResponse = new CreatedAtActionResult("CreateMaintenanceRequest",
            "CreateMaintenanceRequest", expectedResponse.Id, expectedResponse);

        _maintenanceAdapter.Setup(adapter =>
            adapter.CreateMaintenanceRequest(It.IsAny<CreateRequestMaintenanceRequest>())).Returns(expectedResponse);

        IActionResult controllerResponse = _maintenanceController.CreateMaintenanceRequest(dummyRequest);
        _maintenanceAdapter.VerifyAll();

        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        Assert.IsNotNull(controllerResponseCasted);

        CreateRequestMaintenanceResponse? controllerResponseValueCasted =
            controllerResponseCasted.Value as CreateRequestMaintenanceResponse;
        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedResponse.Equals(controllerResponseValueCasted));
    }

    #endregion

    #region Assign Maintenance Request

    [TestMethod]
    public void AssignMaintenanceRequest_204CodeIsReturned()
    {
        NoContentResult expectedControllerResponse = new NoContentResult();

        _maintenanceAdapter.Setup(adapter =>
            adapter.AssignMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<Guid>()));

        IActionResult controllerResponse =
            _maintenanceController.AssignMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<Guid>());

        _maintenanceAdapter.VerifyAll();

        NoContentResult? controllerResponseCasted = controllerResponse as NoContentResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }

    #endregion

    #region Get Maintenance Request by Request Handler

    [TestMethod]
    public void GetMaintenanceRequestByRequestHandler_200CodeIsReturned()
    {
        IEnumerable<GetMaintenanceRequestResponse> expectedMaintenanceRequests =
            new List<GetMaintenanceRequestResponse>()
            {
                _expectedMaintenanceRequest,
                new GetMaintenanceRequestResponse()
                {
                    Id = Guid.NewGuid(),
                    Description = "Fix leaky faucet",
                    FlatId = Guid.NewGuid(),
                    Category = Guid.NewGuid(),
                    RequestStatus = StatusEnumMaintenanceResponse.Closed
                }
            };

        _maintenanceAdapter.Setup(adapter =>
                adapter.GetMaintenanceRequestsByRequestHandler(It.IsAny<Guid>()))
            .Returns(expectedMaintenanceRequests.ToList());

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedMaintenanceRequests);

        IActionResult controllerResponse =
            _maintenanceController.GetMaintenanceRequestByRequestHandler(It.IsAny<Guid>());

        _maintenanceAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetMaintenanceRequestResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetMaintenanceRequestResponse>;
        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }

    #endregion

    #region Update Maintenance Request Status

    [TestMethod]
    public void UpdateMaintenanceRequestStatus_ReturnsNoContent()
    {
        UpdateMaintenanceRequestStatusRequest dummyRequest = new UpdateMaintenanceRequestStatusRequest()
        {
            RequestStatus = StatusEnumMaintenanceRequest.Closed
        };
        Guid id = Guid.NewGuid();

        _maintenanceAdapter.Setup(adapter =>
            adapter.UpdateMaintenanceRequestStatus(id, dummyRequest));

        IActionResult controllerResponse = _maintenanceController.UpdateMaintenanceRequestStatus(id, dummyRequest);

        _maintenanceAdapter.VerifyAll();

        NoContentResult controllerResponseCasted = (NoContentResult)controllerResponse;
        Assert.AreEqual(204, controllerResponseCasted.StatusCode);
    }


    [TestMethod]
    public void GetMaintenanceRequestById_200CodeIsReturned()
    {
        OkObjectResult expectedControllerResponse = new OkObjectResult(_expectedMaintenanceRequest);
        expectedControllerResponse.Value = _expectedMaintenanceRequest;

        _maintenanceAdapter.Setup(adapter => adapter.GetMaintenanceRequestById(_idFromRoute))
            .Returns(_expectedMaintenanceRequest);

        IActionResult controllerResponse = _maintenanceController.GetMaintenanceRequestById(_idFromRoute);

        _maintenanceAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        GetMaintenanceRequestResponse? controllerResponseValueCasted =
            controllerResponseCasted.Value as GetMaintenanceRequestResponse;
        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(_expectedMaintenanceRequest.Equals(controllerResponseValueCasted));
    }

    #endregion
}