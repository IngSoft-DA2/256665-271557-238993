using Adapter;
using Domain;
using Domain.Enums;
using IServiceLogic;
using Moq;
using WebModel.Responses.InvitationResponses;

namespace Test.Adapters;

[TestClass]

public class InvitationAdapterTest
{
    private Mock<IInvitationServiceLogic> _invitationServiceLogic;
    private InvitationAdapter _invitationAdapter;
    
    [TestInitialize]
    public void Initialize()
    {
        _invitationServiceLogic = new Mock<IInvitationServiceLogic>(MockBehavior.Strict);
        _invitationAdapter = new InvitationAdapter(_invitationServiceLogic.Object);
    }
    
    [TestMethod]
    public void GetAllInvitations_ShouldReturnAllInvitationsConvertedFromDomainToResponse()
    {
        IEnumerable<Invitation> invitations = new List<Invitation>
        {
            new Invitation
            {
                Id = Guid.NewGuid(),
                Firstname = "John",
                Lastname = "Doe",
                Email = "",
                ExpirationDate = DateTime.Now,
                Status = StatusEnum.Pending,
            }       
        };
        
        IEnumerable<GetInvitationResponse> expectedInvitations = new List<GetInvitationResponse>
        {
            new GetInvitationResponse
            {
                Id = invitations.First().Id,
                Firstname = invitations.First().Firstname,
                Lastname = invitations.First().Lastname,
                Email = invitations.First().Email,
                ExpirationDate = invitations.First().ExpirationDate,
                Status = (StatusEnumResponse) invitations.First().Status
            }
        };
        
        _invitationServiceLogic.Setup(service => service.GetAllInvitations()).Returns(invitations);
        
        IEnumerable<GetInvitationResponse> adapterResponse = _invitationAdapter.GetAllInvitations();
        
        _invitationServiceLogic.VerifyAll();
        
        IEnumerable<GetInvitationResponse> adapterResponseList = adapterResponse.ToList();
        Assert.IsTrue(expectedInvitations.SequenceEqual(adapterResponseList));

    }
    
    [TestMethod]
    public void GetAllInvitations_ShouldThrowException()
    {
        _invitationServiceLogic.Setup(service => service.GetAllInvitations()).Throws(new Exception("Something went wrong"));
        
        Assert.ThrowsException<Exception>(() => _invitationAdapter.GetAllInvitations());
        
        _invitationServiceLogic.VerifyAll();
    }
    
    [TestMethod]
    public void GetInvitationById_ShouldReturnInvitationConvertedFromDomainToResponse()
    {
        Invitation invitation = new Invitation
        {
            Id = Guid.NewGuid(),
            Firstname = "John",
            Lastname = "Doe",
            Email = "johnDoe@gmail.com",
            ExpirationDate = DateTime.Now,
            Status = StatusEnum.Pending,
        };
        
        GetInvitationResponse expectedAdapterResponse = new GetInvitationResponse
        {
            Id = invitation.Id,
            Firstname = invitation.Firstname,
            Lastname = invitation.Lastname,
            Email = "johnDoe@gmail.com",
            ExpirationDate = invitation.ExpirationDate,
            Status = (StatusEnumResponse) invitation.Status
        };
        
        _invitationServiceLogic.Setup(service => service.GetInvitationById(It.IsAny<Guid>())).Returns(invitation);
        
        GetInvitationResponse adapterResponse = _invitationAdapter.GetInvitationById(invitation.Id);
        
        _invitationServiceLogic.VerifyAll();
        
        Assert.AreEqual(expectedAdapterResponse, adapterResponse);
    }
    
    [TestMethod]
    public void GetInvitationById_ShouldThrowException()
    {
        _invitationServiceLogic.Setup(service => service.GetInvitationById(It.IsAny<Guid>())).Throws(new Exception("Something went wrong"));
        
        Assert.ThrowsException<Exception>(() => _invitationAdapter.GetInvitationById(It.IsAny<Guid>()));
        
        _invitationServiceLogic.VerifyAll();
    }

}