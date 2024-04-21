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
    [TestMethod]
    public void GetManagersRequest_OkIsReturned()
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
        
        Mock<IManagerAdapter> managerAdapter = new Mock<IManagerAdapter>(MockBehavior.Strict);
        managerAdapter.Setup(adapter => adapter.GetAllManagers()).Returns(expectedValue);

        ManagerController managerController = new ManagerController(managerAdapter.Object);

        IActionResult controllerResponse = managerController.GetAllManagers();
        managerAdapter.VerifyAll();
        
        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        IEnumerable<GetManagerResponse>? value = controllerResponseCasted.Value as IEnumerable<GetManagerResponse>;
        Assert.IsNotNull(value);

        Assert.AreEqual(expectedControllerResponse.StatusCode,controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedValue.SequenceEqual(value));


    }
    
}