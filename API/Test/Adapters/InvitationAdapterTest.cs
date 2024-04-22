using Adapter;
using Domain;
using Domain.Enums;
using IServiceLogic;
using Moq;
using WebModel.Requests.InvitationRequests;
using WebModel.Responses.InvitationResponses;

namespace Test.Adapters;

[TestClass]
public class InvitationAdapterTest
{
    private Mock<IInvitationServiceLogic> _invitationServiceLogic;
    private InvitationAdapter _invitationAdapter;
    private GetInvitationResponse _genericInvitationResponse;
    private Invitation _genericInvitation1;

    [TestInitialize]
    public void Initialize()
    {
        _invitationServiceLogic = new Mock<IInvitationServiceLogic>(MockBehavior.Strict);
        _invitationAdapter = new InvitationAdapter(_invitationServiceLogic.Object);
        
        _genericInvitation1 = new Invitation
        {
            Id = Guid.NewGuid(),
            Firstname = "John",
            Lastname = "Doe",
            Email = "johndoe@gmail.com",
            ExpirationDate = DateTime.Now,
            Status = StatusEnum.Pending,
        };
        
        _genericInvitationResponse = new GetInvitationResponse
        {
            Id = _genericInvitation1.Id,
            Firstname = _genericInvitation1.Firstname,
            Lastname = _genericInvitation1.Lastname,
            Email = _genericInvitation1.Email,
            ExpirationDate = _genericInvitation1.ExpirationDate,
            Status = (StatusEnumResponse)_genericInvitation1.Status
        };
    }

    [TestMethod]
    public void GetAllInvitations_ShouldReturnAllInvitationsConvertedFromDomainToResponse()
    {
        IEnumerable<Invitation> invitations = new List<Invitation> {_genericInvitation1};

        IEnumerable<GetInvitationResponse> expectedInvitations = new List<GetInvitationResponse> {_genericInvitationResponse};
        
        _invitationServiceLogic.Setup(service => service.GetAllInvitations()).Returns(invitations);
        
        IEnumerable<GetInvitationResponse> adapterResponse = _invitationAdapter.GetAllInvitations();

        _invitationServiceLogic.VerifyAll();
        
        Assert.IsTrue(expectedInvitations.SequenceEqual(adapterResponse));
    }

    [TestMethod]
    public void GetAllInvitations_ShouldThrowException()
    {
        _invitationServiceLogic.Setup(service => service.GetAllInvitations())
            .Throws(new Exception("Something went wrong"));

        Assert.ThrowsException<Exception>(() => _invitationAdapter.GetAllInvitations());

        _invitationServiceLogic.VerifyAll();
    }

    [TestMethod]
    public void GetInvitationById_ShouldReturnInvitationConvertedFromDomainToResponse()
    {
        Invitation invitation = _genericInvitation1;

        GetInvitationResponse expectedAdapterResponse = _genericInvitationResponse;

        _invitationServiceLogic.Setup(service => service.GetInvitationById(It.IsAny<Guid>())).Returns(invitation);

        GetInvitationResponse adapterResponse = _invitationAdapter.GetInvitationById(invitation.Id);

        _invitationServiceLogic.VerifyAll();

        Assert.AreEqual(expectedAdapterResponse, adapterResponse);
    }

    [TestMethod]
    public void GetInvitationById_ShouldThrowException()
    {
        _invitationServiceLogic.Setup(service => service.GetInvitationById(It.IsAny<Guid>()))
            .Throws(new Exception("Something went wrong"));

        Assert.ThrowsException<Exception>(() => _invitationAdapter.GetInvitationById(It.IsAny<Guid>()));

        _invitationServiceLogic.VerifyAll();
    }
    
    [TestMethod]
    public void CreateInvitation_ShouldCreateInvitation()
    {
        CreateInvitationRequest invitationToCreate = new CreateInvitationRequest
        {
            Firstname = _genericInvitation1.Firstname,
            Lastname = _genericInvitation1.Lastname,
            Email = _genericInvitation1.Email,
            ExpirationDate = _genericInvitation1.ExpirationDate
        };

        _invitationServiceLogic.Setup(service => service.CreateInvitation(It.IsAny<Invitation>()));

        _invitationAdapter.CreateInvitation(invitationToCreate);

        _invitationServiceLogic.Verify(service => service.CreateInvitation(It.IsAny<Invitation>()), Times.Once);
    }
}