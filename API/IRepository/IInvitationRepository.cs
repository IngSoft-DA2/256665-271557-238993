using Domain;

namespace IRepository;

public interface IInvitationRepository
{
    
    public IEnumerable<Invitation> GetAllInvitations();
    
    
}