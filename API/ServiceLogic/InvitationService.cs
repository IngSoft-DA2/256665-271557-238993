using System.Collections;
using System.Reflection;
using Domain;
using Domain.Enums;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class InvitationService : IInvitationService
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
        catch (ObjectRepeatedServiceException)
        {
            throw new ObjectRepeatedServiceException();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
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

    #region Update Invitation By Id

    public void UpdateInvitation(Guid idOfInvitationToUpdate, Invitation invitationUpdated)
    {
        Invitation invitationNotUpdated = GetInvitationById(idOfInvitationToUpdate);
        try
        {
            ValidationForBeingPossibleToUpdate(invitationUpdated, invitationNotUpdated);
            MapProperties(invitationUpdated, invitationNotUpdated);
            invitationUpdated.InvitationValidator();

            _invitationRepository.UpdateInvitation(invitationUpdated);
        }
        catch (InvalidInvitationException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (ObjectRepeatedServiceException)
        {
            throw new ObjectRepeatedServiceException();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    private static void ValidationForBeingPossibleToUpdate(Invitation invitationUpdated,
        Invitation invitationNotUpdated)
    {
        if (invitationNotUpdated.Status != StatusEnum.Pending)
        {
            throw new InvalidInvitationException("Invitation is not pending status, so it is not usable.");
        }

        if (invitationUpdated.Status == StatusEnum.Pending
            && invitationNotUpdated.ExpirationDate.Date > DateTime.UtcNow.AddDays(1))
        {
            throw new InvalidInvitationException(
                "Expiration date cannot be updated to a later date. It must be expired or one day from now.");
        }

        if (invitationUpdated.Status != StatusEnum.Pending &&
            invitationUpdated.ExpirationDate != invitationNotUpdated.ExpirationDate)
        {
            throw new InvalidInvitationException("Expiration date cannot be updated if the status is not pending.");
        }
    }

    private static void MapProperties(Invitation invitationWithUpdates, Invitation invitationWithoutUpdates)
    {

        if (invitationWithUpdates.Equals(invitationWithoutUpdates))
        {
            throw new ObjectRepeatedServiceException();
        }
        
        foreach (PropertyInfo property in typeof(Invitation).GetProperties())
        {
            object? originalValue = property.GetValue(invitationWithoutUpdates);
            object? updatedValue = property.GetValue(invitationWithUpdates);

            if (updatedValue == null && originalValue != null)
            {
                property.SetValue(invitationWithUpdates, originalValue);
            }
        }
    }

    #endregion

    #region Delete Invitation By Id

    public void DeleteInvitation(Guid invitationIdToDelete)
    {
        Invitation invitationToDelete = GetInvitationById(invitationIdToDelete);
        try
        {
            DeleteRepoValidations(invitationToDelete);
            _invitationRepository.DeleteInvitation(invitationToDelete);
        }
        catch (InvalidInvitationException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }


        _invitationRepository.DeleteInvitation(invitationToDelete);
    }

    public IEnumerable<Invitation> GetAllInvitationsByEmail(string email)
    {
        try
        {
            IEnumerable<Invitation> invitations = _invitationRepository.GetAllInvitationsByEmail(email);
            return invitations;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
       
    }

    private static void DeleteRepoValidations(Invitation invitationToDelete)
    {
        if (invitationToDelete.Status == StatusEnum.Accepted)
            throw new InvalidInvitationException("Invitation that is accepted, can not be deleted.");
    }

    #endregion
}