using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.OwnerRequests;
using WebModel.Responses.OwnerResponses;

namespace Test.ApiControllers;

[TestClass]
public class OwnerControllerTest
{
    
    private Mock<IOwnerAdapter> _ownerAdapter;
    private OwnerController _ownerController;
    
    [TestInitialize]
    public void Initialize()
    {
        _ownerAdapter = new Mock<IOwnerAdapter>(MockBehavior.Strict);
        _ownerController = new OwnerController(_ownerAdapter.Object);
    }
    
    
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
        
        _ownerAdapter.Setup(adapter => adapter.GetOwners()).Returns(expectedOwners);
        
        IActionResult controllerResponse = _ownerController.GetOwners();

        _ownerAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<OwnerResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<OwnerResponse>;
        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(expectedOwners));
    }
    
    [TestMethod]
    public void GetOwners_InternalServerErrorIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;
        
        _ownerAdapter.Setup(adapter => adapter.GetOwners()).Throws(new Exception("Something went wrong"));
        
        IActionResult controllerResponse = _ownerController.GetOwners();

        _ownerAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    [TestMethod]
    public void CreateOwner_CreatedAtActionIsReturned()
    {
        CreateOwnerResponse expectedOwner = new CreateOwnerResponse()
        {
            Id = Guid.NewGuid()
        };
        
        CreatedAtActionResult expectedControllerResponse =
            new CreatedAtActionResult("CreateOwner", "CreateOwner"
                , expectedOwner.Id, expectedOwner);
        
        _ownerAdapter.Setup(adapter => adapter.CreateOwner(It.IsAny<CreateOwnerRequest>())).Returns(expectedOwner);
        
        IActionResult controllerResponse = _ownerController.CreateOwner(It.IsAny<CreateOwnerRequest>());
        
        _ownerAdapter.VerifyAll();
        
        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);


    }

}