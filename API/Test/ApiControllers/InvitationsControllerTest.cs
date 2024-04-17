using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests;
using WebModels.Responses;
using Guid = System.Guid;

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
            Lastname = "De santa",
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
    public void GetAllInvitations_200CodeIsReturned()
    {
        IEnumerable<GetInvitationResponse> expectedInvitations = new List<GetInvitationResponse>()
        {
            _expectedInvitation,
            new GetInvitationResponse()
            {
                Id = Guid.NewGuid(),
                Firstname = "John",
                Lastname = "Scott",
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
    public void GetAllInvitations_500StatusCodeIsReturned()
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
    public void GetInvitationByIdRequest_OkIsReturned()
    {
        OkObjectResult expectedControllerResponse = new OkObjectResult(_expectedInvitation);

        _invitationAdapter.Setup(adapter => adapter.GetInvitationById(_idFromRoute)).Returns(_expectedInvitation);

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
    public void GetInvitationByIdRequest_NotFoundIsReturned()
    {
        NotFoundObjectResult expectedResponse = new NotFoundObjectResult("Invitation was not found, reload the page");

        _invitationAdapter.Setup(adapter => adapter.GetInvitationById(It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundException());

        IActionResult controllerResponse = _invitationsController.GetInvitationById(It.IsAny<Guid>());
        _invitationAdapter.VerifyAll();

        NotFoundObjectResult? controllerResponseCasted = controllerResponse as NotFoundObjectResult;

        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(controllerResponseCasted.Value, expectedResponse.Value);
        Assert.AreEqual(expectedResponse.StatusCode, controllerResponseCasted.StatusCode);
    }

    [TestMethod]
    public void GetInvitationByIdRequest_500StatusCodeIsReturned()
    {
        _invitationAdapter.Setup(adapter => adapter.GetInvitationById(It.IsAny<Guid>()))
            .Throws(new Exception("Database Broken"));

        IActionResult controllerResponse = _invitationsController.GetInvitationById(It.IsAny<Guid>());

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(_expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(_expectedControllerResponse.Value, controllerResponseCasted.Value);
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

        IActionResult controllerResponse = _invitationsController.CreateInvitation(It.IsAny<CreateInvitationRequest>());
        _invitationAdapter.VerifyAll();

        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        Assert.IsNotNull(controllerResponseCasted);

        CreateInvitationResponse? controllerResponseValue = controllerResponseCasted.Value as CreateInvitationResponse;
        Assert.IsNotNull(controllerResponseValue);

        Assert.IsTrue(expectedResponse.Equals(controllerResponseValue));
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }

    [TestMethod]
    public void CreateInvitationRequest_BadRequestIsReturned()
    {
        _invitationAdapter.Setup(adapter => adapter.CreateInvitation(It.IsAny<CreateInvitationRequest>()))
            .Throws(new ObjectErrorException("Firstname cannot be empty"));

        BadRequestObjectResult expectedControllerResponse = new BadRequestObjectResult("Firstname cannot be empty");

        IActionResult controllerResponse = _invitationsController.CreateInvitation(It.IsAny<CreateInvitationRequest>());
        _invitationAdapter.VerifyAll();

        BadRequestObjectResult? controllerResponseCasted = controllerResponse as BadRequestObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }


    [TestMethod]
    public void CreateInvitationRequest_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        _invitationAdapter.Setup(adapter => adapter.CreateInvitation(It.IsAny<CreateInvitationRequest>()))
            .Throws(new Exception("An specific error on the server"));

        IActionResult controllerResponse = _invitationsController.CreateInvitation(It.IsAny<CreateInvitationRequest>());
        _invitationAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(controllerResponseCasted.Value, expectedControllerResponse.Value);
        Assert.AreEqual(controllerResponseCasted.StatusCode, expectedControllerResponse.StatusCode);
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
            _invitationsController.UpdateInvitation(It.IsAny<Guid>(), It.IsAny<UpdateInvitationRequest>());
        _invitationAdapter.VerifyAll();

        NoContentResult? controllerResponseCasted = controllerResponse as NoContentResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }

    [TestMethod]
    public void UpdateInvitationRequest_NotFoundIsReturned()
    {
        NotFoundObjectResult expectedControllerResponse =
            new NotFoundObjectResult("The specific invitation was not found in Database");

        _invitationAdapter.Setup(adapter =>
                adapter.UpdateInvitation(It.IsAny<Guid>(), It.IsAny<UpdateInvitationRequest>()))
            .Throws(new ObjectNotFoundException());

        IActionResult controllerResponse = _invitationsController
            .UpdateInvitation(It.IsAny<Guid>(), It.IsAny<UpdateInvitationRequest>());
        _invitationAdapter.VerifyAll();

        NotFoundObjectResult? controllerResponseCasted = controllerResponse as NotFoundObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    [TestMethod]
    public void UpdateInvitationRequest_BadRequestIsReturned()
    {
        BadRequestObjectResult expectedControllerResponse = new BadRequestObjectResult("Specific Error");

        _invitationAdapter.Setup(adapter =>
                adapter.UpdateInvitation(It.IsAny<Guid>(), It.IsAny<UpdateInvitationRequest>()))
            .Throws(new ObjectErrorException("Specific Error"));

        IActionResult controllerResponse =
            _invitationsController.UpdateInvitation(It.IsAny<Guid>(), It.IsAny<UpdateInvitationRequest>());

        BadRequestObjectResult? controllerResponseCasted = controllerResponse as BadRequestObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    [TestMethod]
    public void UpdateInvitationRequest_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        _invitationAdapter
            .Setup(adapter => adapter.UpdateInvitation(It.IsAny<Guid>(), It.IsAny<UpdateInvitationRequest>()))
            .Throws(new Exception("Unknown Error"));

        IActionResult controllerResponse =
            _invitationsController.UpdateInvitation(It.IsAny<Guid>(), It.IsAny<UpdateInvitationRequest>());

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    #endregion

    #region Delete Invitation

    [TestMethod]
    public void DeleteInvitationRequest_NoContentIsReturned()
    {
        NoContentResult expectedControllerResponse = new NoContentResult();

        _invitationAdapter.Setup(adapter => adapter.DeleteInvitation(It.IsAny<Guid>()));

        IActionResult controllerResponse = _invitationsController.DeleteInvitation(It.IsAny<Guid>());

        NoContentResult? controllerResponseCasted = controllerResponse as NoContentResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }


    [TestMethod]
    public void DeleteInvitationRequest_NotFoundIsReturned()
    {
        NotFoundObjectResult expectedControllerResponse =
            new NotFoundObjectResult("Invitation to delete was not found");

        _invitationAdapter.Setup(adapter => adapter.DeleteInvitation(It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundException());

        IActionResult controllerResponse = _invitationsController.DeleteInvitation(It.IsAny<Guid>());

        NotFoundObjectResult? controllerResponseCasted = controllerResponse as NotFoundObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    [TestMethod]
    public void DeleteInvitationRequest_BadRequestIsReturned()
    {
        BadRequestObjectResult expectedControllerResponse = new BadRequestObjectResult("Some specific error");

        _invitationAdapter.Setup(adapter => adapter.DeleteInvitation(It.IsAny<Guid>()))
            .Throws(new ObjectErrorException("Some specific error"));

        IActionResult controllerResponse = _invitationsController.DeleteInvitation(It.IsAny<Guid>());


        BadRequestObjectResult? controllerResponseCasted = controllerResponse as BadRequestObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    [TestMethod]
    public void DeleteInvitationRequest_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        _invitationAdapter.Setup(adapter => adapter.DeleteInvitation(It.IsAny<Guid>()))
            .Throws(new Exception("Some specific internal error"));

        IActionResult controllerResponse = _invitationsController.DeleteInvitation(It.IsAny<Guid>());

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    #endregion
}