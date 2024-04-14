using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests;
using WebModels.Responses;

namespace Test.ApiControllers;

[TestClass]
public class InvitationsControllerTest
{
    #region TestInitialize

    private InvitationsController _invitationsController;
    private Mock<IInvitationAdapter> _invitationAdapter;
    private GetInvitationResponse _expectedInvitation;
    private Guid _idFromRoute;
    private ObjectResult _expectedControllerResponse;

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
        _idFromRoute = Guid.NewGuid();
        _expectedControllerResponse = new ObjectResult("Internal Server Error");
        _expectedControllerResponse.StatusCode = 500;
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

        IActionResult controllerResponse = _invitationsController.GetAllInvitations();
        _invitationAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;

        Assert.IsNotNull(controllerResponseCasted);
        Assert.AreEqual(_expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(_expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    #endregion

    #region Get Invitation By Id

    [TestMethod]
    public void GivenInvitationId_ShouldReturnItsInvitation()
    {
        OkObjectResult expectedControllerResponse = new OkObjectResult(_expectedInvitation);

        _invitationAdapter.Setup(adapter => adapter.GetInvitationById(It.IsAny<Guid>())).Returns(_expectedInvitation);

        IActionResult controllerResponse = _invitationsController.GetInvitationById(_idFromRoute);
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

        IActionResult controllerResponse = _invitationsController.GetInvitationById(_idFromRoute);
        _invitationAdapter.VerifyAll();

        NotFoundObjectResult? controllerResponseCasted = controllerResponse as NotFoundObjectResult;

        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(controllerResponseCasted.Value, expectedResponse.Value);
        Assert.AreEqual(expectedResponse.StatusCode, controllerResponseCasted.StatusCode);
    }

    [TestMethod]
    public void WhenTryingToGetAnInvitationViaId_DatabaseWasBroken_SoItShouldReturn500StatusCode()
    {
        _invitationAdapter.Setup(adapter => adapter.GetInvitationById(It.IsAny<Guid>()))
            .Throws(new Exception("Database Broken"));

        IActionResult controllerResponse = _invitationsController.GetInvitationById(_idFromRoute);

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(_expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(_expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    #endregion

    [TestMethod]
    public void GivenCreateInvitationRequest_ShouldCreateTheInvitation()
    {
        CreateInvitationRequest request = new CreateInvitationRequest
        {
            Firstname = "John",
            Email = "jhon@gmail.com",
            ExpirationDate = DateTime.MaxValue
        };

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


        IActionResult controllerResponse = _invitationsController.CreateInvitation(request);
        _invitationAdapter.VerifyAll();

        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        Assert.IsNotNull(controllerResponseCasted);

        CreateInvitationResponse? controllerResponseValue = controllerResponseCasted.Value as CreateInvitationResponse;
        Assert.IsNotNull(controllerResponseValue);

        Assert.IsTrue(expectedResponse.Equals(controllerResponseValue));
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }
}