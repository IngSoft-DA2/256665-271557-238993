using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests;
using WebModel.Requests.InvitationRequests;
using WebModel.Responses.InvitationResponses;
using Guid = System.Guid;

namespace Test.ApiControllers;

[TestClass]
public class InvitationControllerTest
{
    #region Test Initialize

    private InvitationController _invitationController;
    private Mock<IInvitationAdapter> _invitationAdapter;
    private GetInvitationResponse _expectedInvitation;
    private Guid _idFromRoute;
    private ObjectResult _expectedControllerResponse;
    private string _email;

    [TestInitialize]
    public void Initialize()
    {
        _invitationAdapter = new Mock<IInvitationAdapter>(MockBehavior.Strict);
        _invitationController = new InvitationController(_invitationAdapter.Object);
        _expectedInvitation = new GetInvitationResponse()
        {
            Id = Guid.NewGuid(),
            Firstname = "Michael",
            Lastname = "De santa",
            Email = "michael@gmail.com",
            Status = StatusEnumResponse.Pending,
            ExpirationDate = DateTime.MaxValue
        };
        _idFromRoute = Guid.NewGuid();
        _expectedControllerResponse = new ObjectResult("Internal Server Error");
        _expectedControllerResponse.StatusCode = 500;

        _email = "someone@example.com";
    }

    #endregion

    #region Get All Invitations By Email

    [TestMethod]
    public void GetAllInvitations_200CodeIsReturned_WhenEmailIsNotEmpty()
    {
        IEnumerable<GetInvitationResponse> expectedInvitations = new List<GetInvitationResponse>()
            { _expectedInvitation };

        _invitationAdapter.Setup(adapter => adapter.GetAllInvitationsByEmail(_email)).Returns(expectedInvitations);

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedInvitations);

        IActionResult controllerResponse = _invitationController.GetInvitationsByEmail(_email);
        _invitationAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetInvitationResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetInvitationResponse>;
        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedInvitations.SequenceEqual(controllerResponseValueCasted));
    }
    

    #endregion

    #region Get All Invitations

    [TestMethod]
    public void GetAllInvitationsButWithoutEmail_OkIsReturn()
    {
        IEnumerable<GetInvitationResponse> expectedInvitationResponse =
            new List<GetInvitationResponse>() { _expectedInvitation };

        _invitationAdapter.Setup(invitationAdapter =>
            invitationAdapter.GetAllInvitations()).Returns(expectedInvitationResponse);

        IActionResult controllerResponse = _invitationController.GetAllInvitations();
        _invitationAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetInvitationResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetInvitationResponse>;
        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.IsTrue(expectedInvitationResponse.SequenceEqual(controllerResponseValueCasted));
    }

    #endregion

    #region Get Invitation By Id

    [TestMethod]
    public void GetInvitationByIdRequest_OkIsReturned()
    {
        OkObjectResult expectedControllerResponse = new OkObjectResult(_expectedInvitation);

        _invitationAdapter.Setup(adapter => adapter.GetInvitationById(_idFromRoute)).Returns(_expectedInvitation);

        IActionResult controllerResponse = _invitationController.GetInvitationById(_idFromRoute);
        _invitationAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        GetInvitationResponse? controllerValueCasted = controllerResponseCasted.Value as GetInvitationResponse;
        Assert.IsNotNull(controllerValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(_expectedInvitation.Equals(controllerValueCasted));
    }

   

    #endregion

    #region Create Invitation

    [TestMethod]
    public void CreateInvitationRequest_OkIsReturned()
    {
        CreateInvitationResponse expectedResponse = new CreateInvitationResponse
        {
            Id = Guid.NewGuid(),
            Status = StatusEnumResponse.Pending
        };

        CreatedAtActionResult expectedControllerResponse =
            new CreatedAtActionResult("CreateInvitation", "CreateInvitation"
                , expectedResponse.Id, expectedResponse);

        _invitationAdapter.Setup(adapter =>
            adapter.CreateInvitation(It.IsAny<CreateInvitationRequest>())).Returns(expectedResponse);

        IActionResult controllerResponse = _invitationController.CreateInvitation(It.IsAny<CreateInvitationRequest>());
        _invitationAdapter.VerifyAll();

        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        Assert.IsNotNull(controllerResponseCasted);

        CreateInvitationResponse? controllerResponseValue = controllerResponseCasted.Value as CreateInvitationResponse;
        Assert.IsNotNull(controllerResponseValue);

        Assert.IsTrue(expectedResponse.Equals(controllerResponseValue));
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }

    

    #endregion

    #region Update Invitation

    [TestMethod]
    public void UpdateInvitationRequest_NoContentIsReturned()
    {
        NoContentResult expectedControllerResponse = new NoContentResult();

        _invitationAdapter.Setup(adapter =>
            adapter.UpdateInvitation(It.IsAny<Guid>(), It.IsAny<UpdateInvitationRequest>()));

        IActionResult controllerResponse =
            _invitationController.UpdateInvitation(It.IsAny<Guid>(), It.IsAny<UpdateInvitationRequest>());

        _invitationAdapter.Verify(
            adapter => adapter.UpdateInvitation(It.IsAny<Guid>(), It.IsAny<UpdateInvitationRequest>()), Times.Once());

        NoContentResult? controllerResponseCasted = controllerResponse as NoContentResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }
    
    #endregion

    #region Delete Invitation

    [TestMethod]
    public void DeleteInvitationRequest_NoContentIsReturned()
    {
        NoContentResult expectedControllerResponse = new NoContentResult();

        _invitationAdapter.Setup(adapter => adapter.DeleteInvitation(It.IsAny<Guid>()));

        IActionResult controllerResponse = _invitationController.DeleteInvitation(It.IsAny<Guid>());

        _invitationAdapter.Verify(adapter => adapter.DeleteInvitation(It.IsAny<Guid>()), Times.Once());

        NoContentResult? controllerResponseCasted = controllerResponse as NoContentResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }
    
    #endregion
}