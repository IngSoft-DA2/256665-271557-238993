using Domain;
using IRepository;

namespace ServiceLogic;

public class InvitationService
{

    private readonly IInvitationRepository _invitationRepository;
    public InvitationService(IInvitationRepository invitationRepository)
    {
        _invitationRepository = invitationRepository;
    }
    
    
    public IEnumerable<Invitation> GetAllInvitations()
    {
        IEnumerable<Invitation> invitations = _invitationRepository.GetAllInvitations();
        return invitations;
    }
}