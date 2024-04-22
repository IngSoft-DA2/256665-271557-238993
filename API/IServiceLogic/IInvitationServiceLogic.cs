using Domain;

namespace IServiceLogic;

public interface IInvitationServiceLogic
{
    public IEnumerable<Invitation> GetAllInvitations();
    public Invitation GetInvitationById(Guid idOfInvitationToFind);
    
}