using Domain;
using IServiceLogic;
using WebModel.Responses.InvitationResponses;

namespace Adapter;

public class InvitationAdapter
{
    
    private readonly IInvitationServiceLogic _invitationServiceLogic;
    
    public InvitationAdapter(IInvitationServiceLogic invitationServiceLogic)
    {
        _invitationServiceLogic = invitationServiceLogic;
    }
    
    public IEnumerable<GetInvitationResponse> GetAllInvitations()
    {
        try
        {
            IEnumerable<Invitation> serviceResponse = _invitationServiceLogic.GetAllInvitations();
            
            IEnumerable<GetInvitationResponse> adapterResponse = serviceResponse.Select(invitation => new GetInvitationResponse
            {
                Id = invitation.Id,
                Status = (StatusEnumResponse)invitation.Status,
                ExpirationDate = invitation.ExpirationDate,
                Email = invitation.Email,
                Firstname = invitation.Firstname,
                Lastname = invitation.Lastname
            });

            return adapterResponse;
        }
        catch (Exception exceptionCaught)
        {   
            throw new Exception(exceptionCaught.Message);
        }
    }
    
}