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
    private Mock<IInvitationAdapter> _invitationAdapter;


    [TestInitialize]
    public void Initialize()
    {
        _invitationAdapter = new Mock<IInvitationAdapter>(MockBehavior.Strict);
        _invitationsController = new InvitationsController(_invitationAdapter.Object);
    }
    
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

        _invitationAdapter.Setup(adapter => adapter.GetAllInvitations()).Returns(expectedInvitations.ToList());

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedInvitations);


        IActionResult controllerResponse = _invitationsController.GetAllInvitations();
        _invitationAdapter.VerifyAll();

        OkObjectResult controllerResponseCasted = controllerResponse as OkObjectResult;
        List<GetInvitationResponse> controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetInvitationResponse>;

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedInvitations.SequenceEqual(controllerResponseValueCasted));
    }

    [TestMethod]
    public void GetAllInvitationsWhenDbIsBroken_ShouldReturnA500StatusCode()
    {
        _invitationAdapter.Setup(adapter => adapter.GetAllInvitations()).
            Throws(new Exception("Database Broken"));
        StatusCodeResult expectedControllerResponse = new StatusCodeResult(500);
        
        IActionResult controllerResponse = _invitationsController.GetAllInvitations();
        _invitationAdapter.VerifyAll();

        ObjectResult controllerResponseCasted = controllerResponse as ObjectResult;

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }
}