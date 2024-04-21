using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.AdministratorRequests;
using WebModel.Responses.AdministratorResponses;

namespace Test.ApiControllers;

[TestClass]
public class 
    AdministratorControllerTest
{
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
        Mock<IAdministratorAdapter> administratorAdapter = new Mock<IAdministratorAdapter>(MockBehavior.Strict);
        administratorAdapter.Setup(adapter => adapter.CreateAdministrator(It.IsAny<CreateAdministratorRequest>()))
            .Returns(expectedValue);
        AdministratorController controller = new AdministratorController(administratorAdapter.Object);

        IActionResult controllerResponse = controller.CreateAdministrator(It.IsAny<CreateAdministratorRequest>());
        administratorAdapter.VerifyAll();

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
        
        Mock<IAdministratorAdapter> administratorAdapter = new Mock<IAdministratorAdapter>(MockBehavior.Strict);
        administratorAdapter.Setup(adapter => adapter.CreateAdministrator(It.IsAny<CreateAdministratorRequest>()))
            .Throws(new ObjectErrorException("Specific error message"));
        
        AdministratorController controller = new AdministratorController(administratorAdapter.Object);
        
        IActionResult controllerResponse = controller.CreateAdministrator(It.IsAny<CreateAdministratorRequest>());
        administratorAdapter.VerifyAll();
        
        BadRequestObjectResult? controllerResponseCasted = controllerResponse as BadRequestObjectResult;
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }
}