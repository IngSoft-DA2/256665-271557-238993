using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.AdministratorRequests;
using WebModel.Responses.AdministratorResponses;

namespace Test.ApiControllers;

[TestClass]
public class AdministratorControllerTest
{
    #region Initialize

    private Mock<IAdministratorAdapter> _administratorAdapter;
    private AdministratorController _administratorController;

    [TestInitialize]
    public void Initialize()
    {
        _administratorAdapter = new Mock<IAdministratorAdapter>(MockBehavior.Strict);
        _administratorController = new AdministratorController(_administratorAdapter.Object);
    }

    #endregion

    #region Create Administrator

    [TestMethod]
    public void CreateAdministratorRequest_CreatedAtActionIsReturned()
    {
        CreateAdministratorResponse expectedValue = new CreateAdministratorResponse
        {
            Id = Guid.NewGuid()
        };

        CreatedAtActionResult expectedControllerResponse =
            new CreatedAtActionResult("CreateAdministrator", "CreateAdministrator"
                , expectedValue.Id, expectedValue);

        _administratorAdapter.Setup(adapter => adapter.CreateAdministrator(It.IsAny<CreateAdministratorRequest>()))
            .Returns(expectedValue);

        IActionResult controllerResponse =
            _administratorController.CreateAdministrator(It.IsAny<CreateAdministratorRequest>());
        _administratorAdapter.VerifyAll();

        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        Assert.IsNotNull(controllerResponseCasted);

        CreateAdministratorResponse? value = controllerResponseCasted.Value as CreateAdministratorResponse;
        Assert.IsNotNull(value);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedValue.Id, value.Id);
    }

    [TestMethod]
    public void CreateAdministratorRequest_BadRequestIsReturned()
    {
        BadRequestObjectResult expectedControllerResponse = new BadRequestObjectResult("Specific error message");

        _administratorAdapter.Setup(adapter => adapter.CreateAdministrator(It.IsAny<CreateAdministratorRequest>()))
            .Throws(new ObjectErrorAdapterException("Specific error message"));


        IActionResult controllerResponse =
            _administratorController.CreateAdministrator(It.IsAny<CreateAdministratorRequest>());
        _administratorAdapter.VerifyAll();

        BadRequestObjectResult? controllerResponseCasted = controllerResponse as BadRequestObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    [TestMethod]
    public void CreateAdministratorRequest_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");

        _administratorAdapter.Setup(adapter => adapter.CreateAdministrator(It.IsAny<CreateAdministratorRequest>()))
            .Throws(new Exception("Unknown Error"));

        IActionResult controllerResponse =
            _administratorController.CreateAdministrator(It.IsAny<CreateAdministratorRequest>());
        _administratorAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(500, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    #endregion
}