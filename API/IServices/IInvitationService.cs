using Domain;
namespace IServices;

public interface IInvitationService
{
    IEnumerable<Invitation> GetAllInvitations();
}