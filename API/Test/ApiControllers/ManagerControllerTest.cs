using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.ManagerRequests;
using WebModel.Responses.ManagerResponses;

namespace Test.ApiControllers;

[TestClass]
public class ManagerControllerTest
{
    #region Initialize

    private Mock<IManagerAdapter> _managerAdapter;
    private ManagerController _managerController;

    [TestInitialize]
    public void Initialize()
    {
        _managerAdapter = new Mock<IManagerAdapter>(MockBehavior.Strict);
        _managerController = new ManagerController(_managerAdapter.Object);
    }

    #endregion

    #region Get All Managers

    [TestMethod]
    public void GetAllManagersRequest_OkIsReturned()
    {
        IEnumerable<GetManagerResponse> expectedValue = new List<GetManagerResponse>()
        {
            new GetManagerResponse
            {
                Id = Guid.NewGuid(),
                Name = "Michael Kent",
                Email = "michael@gmail.com",
                BuildingsId = new List<Guid>(),
                MaintenanceRequestsId = new List<Guid>()
            }
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedValue);

        _managerAdapter.Setup(adapter => adapter.GetAllManagers()).Returns(expectedValue);

        IActionResult controllerResponse = _managerController.GetAllManagers();
        _managerAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        IEnumerable<GetManagerResponse>? value = controllerResponseCasted.Value as IEnumerable<GetManagerResponse>;
        Assert.IsNotNull(value);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedValue.SequenceEqual(value));
    }

    #endregion

    #region Delete Manager By Id

    [TestMethod]
    public void DeleteManagerById_NoContentIsReturned()
    {
        NoContentResult expectedControllerResponse = new NoContentResult();

        _managerAdapter.Setup(adapter => adapter.DeleteManagerById(It.IsAny<Guid>()));

        IActionResult controllerResponse = _managerController.DeleteManagerById(Guid.NewGuid());
        _managerAdapter.Verify(
            adapter => adapter.DeleteManagerById(It.IsAny<Guid>()), Times.Once());

        NoContentResult? controllerResponseCasted = controllerResponse as NoContentResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }

    
    #endregion
    
    #region Create Manager
    
    [TestMethod]
    public void CreateManager_CreatedAtActionIsReturned()
    {
        CreateManagerResponse dummyResponse = new CreateManagerResponse();

        CreatedAtActionResult expectedControllerResponse =
            new CreatedAtActionResult("CreateManager", "CreateManager"
                , dummyResponse.Id, dummyResponse);

        _managerAdapter.Setup(adapter =>
            adapter.CreateManager(It.IsAny<CreateManagerRequest>(), It.IsAny<Guid>())).Returns(dummyResponse);

        IActionResult controllerResponse = _managerController.CreateManager(It.IsAny<CreateManagerRequest>(), It.IsAny<Guid>());
        _managerAdapter.VerifyAll();

        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        Assert.IsNotNull(controllerResponseCasted);

        CreateManagerResponse? controllerResponseValue = controllerResponseCasted.Value as CreateManagerResponse;
        Assert.IsNotNull(controllerResponseValue);

        Assert.IsTrue(dummyResponse.Equals(controllerResponseValue));
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }
    
    
    #endregion
    
    #region Get Manager By Id
    
    [TestMethod]
    public void GetManagerById_OkIsReturned()
    {
        GetManagerResponse dummyResponse = new GetManagerResponse()
        {
            Id = Guid.NewGuid(),
            Name = "Michael Kent",
            Email = "michael@gmail.com!",
            BuildingsId = new List<Guid>(),
            MaintenanceRequestsId = new List<Guid>()
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(dummyResponse);

        _managerAdapter.Setup(adapter => adapter.GetManagerById(It.IsAny<Guid>())).Returns(dummyResponse);

        IActionResult controllerResponse = _managerController.GetManagerById(It.IsAny<Guid>());
        _managerAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        GetManagerResponse? controllerResponseValue = controllerResponseCasted.Value as GetManagerResponse;
        Assert.IsNotNull(controllerResponseValue);

        Assert.IsTrue(dummyResponse.Equals(controllerResponseValue));
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }
    
    #endregion
}