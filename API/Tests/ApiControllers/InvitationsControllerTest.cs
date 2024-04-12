using Adapter.CustomExceptions;
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
    private GetInvitationResponse _expectedInvitation;

    #region TestInitialize

    [TestInitialize]
    public void Initialize()
    {
        _invitationAdapter = new Mock<IInvitationAdapter>(MockBehavior.Strict);
        _invitationsController = new InvitationsController(_invitationAdapter.Object);
        _expectedInvitation = new GetInvitationResponse()
        {
            Id = Guid.NewGuid(),
            Firstname = "Michael",
            Email = "michael@gmail.com",
            Status = StatusEnumResponse.Pending,
            ExpirationDate = DateTime.MaxValue
        };
    }

    #endregion

    #region Get All Invitations

    [TestMethod]
    public void GetAllInvitations_ShouldReturnAllInvitations()
    {
        IEnumerable<GetInvitationResponse> expectedInvitations = new List<GetInvitationResponse>()
        {
            _expectedInvitation,
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

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetInvitationResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetInvitationResponse>;
        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedInvitations.SequenceEqual(controllerResponseValueCasted));
    }

    [TestMethod]
    public void GetAllInvitationsWhenDbIsBroken_ShouldReturnA500StatusCode()
    {
        _invitationAdapter.Setup(adapter => adapter.GetAllInvitations()).Throws(new Exception("Database Broken"));
        StatusCodeResult expectedControllerResponse = new StatusCodeResult(500);

        IActionResult controllerResponse = _invitationsController.GetAllInvitations();
        _invitationAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;

        Assert.IsNotNull(controllerResponseCasted);
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }

    #endregion

    #region Get Invitation By Id

    [TestMethod]
    public void GivenInvitationId_ShouldReturnItsInvitation()
    {
        Guid idFromRoute = _expectedInvitation.Id;
        OkObjectResult expectedControllerResponse = new OkObjectResult(_expectedInvitation);

        _invitationAdapter.Setup(adapter => adapter.GetInvitationById(It.IsAny<Guid>())).Returns(_expectedInvitation);

        IActionResult controllerResponse = _invitationsController.GetInvitationById(idFromRoute);
        _invitationAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        GetInvitationResponse? controllerValueCasted = controllerResponseCasted.Value as GetInvitationResponse;
        Assert.IsNotNull(controllerValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(_expectedInvitation.Equals(controllerValueCasted));
    }

    [TestMethod]
    public void GivenInvitationIdThatIsNotInDb_ShouldReturnNotFound()
    {
        _invitationAdapter.Setup(adapter => adapter.GetInvitationById(It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundException());

        NotFoundObjectResult expectedResponse = new NotFoundObjectResult("Invitation was not found, reload the page");

        Guid idFromRoute = _expectedInvitation.Id;
        IActionResult controllerResponse = _invitationsController.GetInvitationById(idFromRoute);
        _invitationAdapter.VerifyAll();

        NotFoundObjectResult? controllerResponseCasted = controllerResponse as NotFoundObjectResult;

        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(controllerResponseCasted.Value,expectedResponse.Value);
        Assert.AreEqual(expectedResponse.StatusCode, controllerResponseCasted.StatusCode);
    }

    #endregion
}