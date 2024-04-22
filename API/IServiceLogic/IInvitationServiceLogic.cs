using Domain;

namespace IServiceLogic;

public interface IInvitationServiceLogic
{
    public IEnumerable<Invitation> GetAllInvitations();
    
}