using BuildingBuddy.API.Controllers;
using Domain;
using IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModels.Responses;


namespace BuildingBuddyApiTest.Controllers;

[TestClass]
public class InvitationsControllerTest
{
    private InvitationsController _invitationsController;
    
    [TestMethod]
    public void GetAllInvitationsEndpoint_ShouldReturnAllInvitations()
    {
        IEnumerable<Invitation> expectedInvitations = new List<Invitation>()
        {
            new Invitation()
            {
                Id = Guid.NewGuid(),
                Firstname = "Michael",
                Email = "michael@gmail.com",
                Status = StatusEnum.Pending,
                ExpirationDate = DateTime.MaxValue
            },
            new Invitation()
            {
            Id = Guid.NewGuid(),
            Firstname = "John",
            Email = "jhon@gmail.com",
            Status = StatusEnum.Accepted,
            ExpirationDate = DateTime.MaxValue
        }
        };
        
        List<GetInvitationResponse> expectedResponseValue = expectedInvitations
            .Select(inv => new GetInvitationResponse(inv)).ToList();
        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedResponseValue);
        
        Mock<IInvitationService> invitationService = new Mock<IInvitationService>(MockBehavior.Strict);
        invitationService.Setup(service => service.GetAllInvitations()).Returns(expectedInvitations);
        
        _invitationsController = new InvitationsController(invitationService.Object);
        
        IActionResult controllerResponse= _invitationsController.GetAllInvitations();
        
        invitationService.VerifyAll();
        
        OkObjectResult controllerResponseCasted = controllerResponse as OkObjectResult;
        List<GetInvitationResponse> responseValue = controllerResponseCasted.Value as List<GetInvitationResponse>;
        
        Assert.AreEqual(expectedControllerResponse.StatusCode,controllerResponseCasted.StatusCode);
        
        int listWithSameLength = responseValue.Count;
        
        for (int i = 0; i < listWithSameLength; i++)
        {
            Assert.IsTrue(expectedResponseValue[i].Equals(responseValue[i]));
        }
    }
}