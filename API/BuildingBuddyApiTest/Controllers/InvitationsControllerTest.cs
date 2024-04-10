using System.Xml.Linq;
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
        //Arrange
        IEnumerable<Invitation> expectedInvitations = new List<Invitation>()
        {
            new Invitation()
            {
                Id = Guid.NewGuid(),
                Firstname = "Michael",
                Email = "michael@gmail.com",
                Status = StatusEnum.Pending,
                ExpirationDate = DateTime.MaxValue
            }
        };
        
        List<GetInvitationResponse> expectedResponseValue = expectedInvitations.Select(inv => new GetInvitationResponse(inv)).ToList();
        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedResponseValue);
        
        Mock<IInvitationService> invitationService = new Mock<IInvitationService>(MockBehavior.Strict);
        invitationService.Setup(service => service.GetAllInvitations()).Returns(expectedInvitations);
        
        _invitationsController = new InvitationsController(invitationService.Object);
        
        IActionResult controllerResponse= _invitationsController.GetAllInvitations();
        
        invitationService.VerifyAll();
        
        OkObjectResult controllerResponseCasted = controllerResponse as OkObjectResult;
        List<GetInvitationResponse> responseValue = controllerResponseCasted.Value as List<GetInvitationResponse>;
        
        Assert.AreEqual(expectedControllerResponse.StatusCode,controllerResponseCasted.StatusCode);
        
        Assert.AreEqual(expectedResponseValue.First().Id,responseValue.First().Id);
        Assert.AreEqual(expectedResponseValue.First().Firstname,responseValue.First().Firstname);
        Assert.AreEqual(expectedResponseValue.First().Email,responseValue.First().Email);
        Assert.AreEqual(expectedResponseValue.First().Status,responseValue.First().Status);
        Assert.AreEqual(expectedResponseValue.First().ExpirationDate,responseValue.First().ExpirationDate);




    }
}