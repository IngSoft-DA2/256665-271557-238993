using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Responses.OwnerResponses;

namespace Test.ApiControllers;

[TestClass]
public class OwnerControllerTest
{
    [TestMethod]
    public void GetOwners_OkIsReturned()
    {
        IEnumerable<OwnerResponse> expectedOwners = new List<OwnerResponse>()
        {
            new OwnerResponse()
            {
                Id = Guid.NewGuid(),
                Name = "myOwner",
                Lastname = "myLastName",
                Email = "email@email.com",
            }
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedOwners);
        
        Mock<IOwnerAdapter> ownerAdapter = new Mock<IOwnerAdapter>(MockBehavior.Strict);
        ownerAdapter.Setup(adapter => adapter.GetOwners()).Returns(expectedOwners);
        
        OwnerController ownerController = new OwnerController(ownerAdapter.Object);
        IActionResult controllerResponse = ownerController.GetOwners();

        ownerAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<OwnerResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<OwnerResponse>;
        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(expectedOwners));
    }
}