using Domain;

namespace IServiceLogic;

public interface IInvitationService
{
    public IEnumerable<Invitation> GetAllInvitations();
    public Invitation GetInvitationById(Guid idOfInvitationToFind);
    public void CreateInvitation(Invitation invitationToCreate);
    public void UpdateInvitation(Guid idOfInvitationToUpdate, Invitation invitationToUpdate);
    public void DeleteInvitation(Guid idOfInvitationToDelete);
    public IEnumerable<Invitation> GetAllInvitationsByEmail(string email);
    
}