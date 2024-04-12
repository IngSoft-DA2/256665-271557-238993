using BuildingBuddy.API.Controllers;
using IAdapters;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.Responses;

namespace Tests.ApiControllers;

[TestClass]
public class InvitationsControllerTest
{
    private InvitationsController _invitationsController;

    [TestMethod]
    public void GetAllInvitations_ShouldReturnAllInvitations()
    {
        IEnumerable<GetInvitationResponse> expectedInvitations = new List<GetInvitationResponse>()
        {
            new GetInvitationResponse()
            {
                Id = Guid.NewGuid(),
                Firstname = "Michael",
                Email = "michael@gmail.com",
                Status = StatusEnumResponse.Pending,
                ExpirationDate = DateTime.MaxValue
            },
            new GetInvitationResponse()
            {
                Id = Guid.NewGuid(),
                Firstname = "John",
                Email = "jhon@gmail.com",
                Status = StatusEnumResponse.Accepted,
                ExpirationDate = DateTime.MaxValue
            }
        };

        Mock<IInvitationAdapter> invitationAdapter = new Mock<IInvitationAdapter>(MockBehavior.Strict);
        invitationAdapter.Setup(adapter => adapter.GetAllInvitations()).Returns(expectedInvitations.ToList());

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedInvitations);


        _invitationsController = new InvitationsController(invitationAdapter.Object);
        IActionResult controllerResponse = _invitationsController.GetAllInvitations();
        invitationAdapter.VerifyAll();

        OkObjectResult controllerResponseCasted = controllerResponse as OkObjectResult;
        List<GetInvitationResponse> controllerResponseValueCasted = 
            controllerResponseCasted.Value as List<GetInvitationResponse>;

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedInvitations.SequenceEqual(controllerResponseValueCasted));
    }

    [TestMethod]
    public void GetAllInvitationsWhenDbIsBroken_ShouldReturnA500StatusCode()
    {
        Mock<IInvitationAdapter> invitationAdapter = new Mock<IInvitationAdapter>(MockBehavior.Strict);
        invitationAdapter.Setup(adapter => adapter.GetAllInvitations()).
            Throws(new Exception("Database Broken"));
        StatusCodeResult expectedControllerResponse = new StatusCodeResult(500);
        
        _invitationsController = new InvitationsController(invitationAdapter.Object);
        IActionResult controllerResponse = _invitationsController.GetAllInvitations();
        invitationAdapter.VerifyAll();
        
        ObjectResult controllerResponseCasted = controllerResponse as ObjectResult;
        
        Assert.AreEqual(expectedControllerResponse.StatusCode,controllerResponseCasted.StatusCode);
        
    }
    
    
    
}