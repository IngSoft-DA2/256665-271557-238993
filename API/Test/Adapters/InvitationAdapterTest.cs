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
        
        Mock<IInvitationServiceLogic> invitationServiceLogicMock = new Mock<IInvitationServiceLogic>(MockBehavior.Strict);
        invitationServiceLogicMock.Setup(service => service.GetAllInvitations()).Returns(invitations);
        
        InvitationAdapter invitationAdapter = new InvitationAdapter(invitationServiceLogicMock.Object);
        IEnumerable<GetInvitationResponse> adapterResponse = invitationAdapter.GetAllInvitations();
        
        invitationServiceLogicMock.VerifyAll();
        
        
        IEnumerable<GetInvitationResponse> adapterResponseList = adapterResponse.ToList();
        Assert.IsTrue(expectedInvitations.SequenceEqual(adapterResponseList));

    }

}