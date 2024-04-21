using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
                Name = "Michael Kent",
                Email = "michael@gmail.com",
                Password = "Michael123421!"
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

    [TestMethod]
    public void GetAllManagerRequest_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        Mock<IManagerAdapter> managerAdapter = new Mock<IManagerAdapter>(MockBehavior.Strict);
        managerAdapter.Setup(adapter => adapter.GetAllManagers()).Throws(new Exception("Unknown error"));

        ManagerController managerController = new ManagerController(managerAdapter.Object);

        IActionResult controllerResponse = managerController.GetAllManagers();
        managerAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    #endregion

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
}