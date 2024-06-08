using Adapter.CustomExceptions;
using Domain;
using Domain.Enums;
using IAdapter;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.InvitationRequests;
using WebModel.Responses.InvitationResponses;

namespace Adapter;

public class InvitationAdapter : IInvitationAdapter
{
    #region Constructor and attributes
    
    private readonly IInvitationService _invitationServiceLogic;

    public InvitationAdapter(IInvitationService invitationServiceLogic)
    {
        _invitationServiceLogic = invitationServiceLogic;
    }
    
    #endregion
    
    #region Get All Invitations

    public IEnumerable<GetInvitationResponse> GetAllInvitations()
    {
        try
        {
            IEnumerable<Invitation> serviceResponse = _invitationServiceLogic.GetAllInvitations();

            IEnumerable<GetInvitationResponse> adapterResponse = serviceResponse.Select(invitation =>
                new GetInvitationResponse
                {
                    Id = invitation.Id,
                    Status = (StatusEnumResponse)invitation.Status,
                    ExpirationDate = invitation.ExpirationDate,
                    Email = invitation.Email,
                    Firstname = invitation.Firstname,
                    Lastname = invitation.Lastname,
                    Role = (SystemUserRoleEnumResponse)invitation.Role
                });

            return adapterResponse;
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }
    
    #endregion
    
    #region Get Invitation By Id
    
    public GetInvitationResponse GetInvitationById(Guid idOfInvitationToFind)
    {
        try
        {
            Invitation serviceResponse = _invitationServiceLogic.GetInvitationById(idOfInvitationToFind);

            GetInvitationResponse adapterResponse = new GetInvitationResponse
            {
                Id = serviceResponse.Id,
                Status = (StatusEnumResponse)serviceResponse.Status,
                ExpirationDate = serviceResponse.ExpirationDate,
                Email = serviceResponse.Email,
                Firstname = serviceResponse.Firstname,
                Lastname = serviceResponse.Lastname,
                Role = (SystemUserRoleEnumResponse)serviceResponse.Role
            };

            return adapterResponse;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Invitation was not found");
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }
    
    #endregion
    
    #region Get All Invitations By Email
    public IEnumerable<GetInvitationResponse> GetAllInvitationsByEmail(string email)
    {
        try
        {
            IEnumerable<Invitation> serviceResponse = _invitationServiceLogic.GetAllInvitationsByEmail(email);

            IEnumerable<GetInvitationResponse> adapterResponse = serviceResponse.Select(invitation =>
                new GetInvitationResponse
                {
                    Id = invitation.Id,
                    Status = (StatusEnumResponse)invitation.Status,
                    ExpirationDate = invitation.ExpirationDate,
                    Email = invitation.Email,
                    Firstname = invitation.Firstname,
                    Lastname = invitation.Lastname,
                    Role = (SystemUserRoleEnumResponse)invitation.Role
                });

            return adapterResponse;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Invitations for the email were not found");
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }
    
    #endregion

    #region Create Invitation

    public CreateInvitationResponse CreateInvitation(CreateInvitationRequest invitationToCreate)
    {
        try
        {
            Invitation invitation = new Invitation
            {
                Id = Guid.NewGuid(),
                Firstname = invitationToCreate.Firstname,
                Lastname = invitationToCreate.Lastname,
                Email = invitationToCreate.Email,
                ExpirationDate = invitationToCreate.ExpirationDate,
                Role = (SystemUserRoleEnum) invitationToCreate.Role
            };

            _invitationServiceLogic.CreateInvitation(invitation);
            
            CreateInvitationResponse invitationResponse = new CreateInvitationResponse { Id = invitation.Id };

            return invitationResponse;
        }
        catch (ObjectRepeatedServiceException exceptionCaught)
        {
            throw new ObjectRepeatedAdapterException();
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }
    
    #endregion
    
    #region Update Invitation

    public void UpdateInvitation(Guid idOfInvitationToUpdate, UpdateInvitationRequest invitationToUpdateRequest)
    {
        try
        {
            Invitation invitation = new Invitation
            {
                Id = idOfInvitationToUpdate,
                Status = (StatusEnum)invitationToUpdateRequest.Status, 
                ExpirationDate = invitationToUpdateRequest.ExpirationDate
            };
            
            _invitationServiceLogic.UpdateInvitation(idOfInvitationToUpdate, invitation);
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Invitation was not found");
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (ObjectRepeatedServiceException exceptionCaught)
        {
            throw new ObjectRepeatedAdapterException();
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }
    
    #endregion
    
    #region Delete Invitation

    public void DeleteInvitation(Guid idOfInvitationToDelete)
    {
        try
        {
            _invitationServiceLogic.DeleteInvitation(idOfInvitationToDelete);
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Invitation was not found");
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }
    
    #endregion
}