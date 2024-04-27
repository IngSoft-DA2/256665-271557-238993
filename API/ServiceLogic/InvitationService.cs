using System.Reflection;
using Domain;
using Domain.Enums;
using IRepository;
using Repositories.CustomExceptions;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class InvitationService
{
    #region Constructor and Dependency Injection

    private readonly IInvitationRepository _invitationRepository;

    public InvitationService(IInvitationRepository invitationRepository)
    {
        _invitationRepository = invitationRepository;
    }

    #endregion

    #region Get all invitations

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

    #endregion

    #region Get Invitation by Id

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

    #endregion

    #region Create Invitation

    public void CreateInvitation(Invitation invitationToAdd)
    {
        try
        {
            invitationToAdd.InvitationValidator();
            CheckIfEmailHasAlreadyAInvitation(invitationToAdd);
            _invitationRepository.CreateInvitation(invitationToAdd);
        }
        catch (InvalidInvitationException exceptionFromDomain)
        {
            throw new ObjectErrorServiceException(exceptionFromDomain.Message);
        }
    }

    private void CheckIfEmailHasAlreadyAInvitation(Invitation invitationToAdd)
    {
        IEnumerable<Invitation> invitations = _invitationRepository.GetAllInvitations();

        foreach (Invitation invitation in invitations)
        {
            if (invitation.Email == invitationToAdd.Email && invitation.Status != StatusEnum.Rejected)
            {
                throw new ObjectRepeatedServiceException();
            }
        }
    }

    #endregion

    public void UpdateInvitation(Guid idOfInvitationToUpdate, Invitation invitationWithUpdates)
    {
        Invitation invitationWithoutUpdates = GetInvitationById(idOfInvitationToUpdate);

        if (invitationWithoutUpdates.Status != StatusEnum.Pending)
        {
            throw new ObjectErrorServiceException("Invitation is not pending, so it cannot be updated.");
        }
        
        if(invitationWithUpdates.Status != StatusEnum.Pending && invitationWithoutUpdates.ExpirationDate < DateTime.Now)
        {
            throw new ObjectErrorServiceException("Invitation is expired, so it cannot be updated.");
        }
        
        try
        {
            foreach (PropertyInfo property in typeof(Invitation).GetProperties())
            {
                object? originalValue = property.GetValue(invitationWithoutUpdates);
                object? updatedValue = property.GetValue(invitationWithUpdates);
            
                if (updatedValue == null && originalValue != null)
                {
                    property.SetValue(invitationWithUpdates, originalValue);
                }
            }
            
            //PARA VALIDAR SI LOS DATOS DE LA INVITACION ACTUALIZADA SON CORRECTOS
            invitationWithUpdates.InvitationValidator();
        
            _invitationRepository.UpdateInvitation(invitationWithUpdates);
        }
        catch (InvalidInvitationException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
    }
}