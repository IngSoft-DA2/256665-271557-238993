using Domain;
using IRepository;
using Repositories.CustomExceptions;
using ServiceLogic.CustomExceptions;

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
        try
        {
            IEnumerable<Invitation> invitations = _invitationRepository.GetAllInvitations();
            return invitations;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }
    public Invitation GetInvitationById(Guid invitationId)
    {
        Invitation invitationFound;
        try
        {
            invitationFound = _invitationRepository.GetInvitationById(invitationId);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
        
        if (invitationFound is null) throw new ObjectNotFoundServiceException();
        return invitationFound;
    }
}