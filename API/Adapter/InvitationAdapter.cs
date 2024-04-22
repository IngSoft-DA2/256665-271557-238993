﻿using Adapter.CustomExceptions;
using Domain;
using Domain.Enums;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.InvitationRequests;
using WebModel.Responses.InvitationResponses;

namespace Adapter;

public class InvitationAdapter
{
    private readonly IInvitationServiceLogic _invitationServiceLogic;

    public InvitationAdapter(IInvitationServiceLogic invitationServiceLogic)
    {
        _invitationServiceLogic = invitationServiceLogic;
    }

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
                    Lastname = invitation.Lastname
                });

            return adapterResponse;
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

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
                Lastname = serviceResponse.Lastname
            };

            return adapterResponse;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException();
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    public CreateInvitationResponse CreateInvitation(CreateInvitationRequest invitationToCreate)
    {
        try
        {
            Invitation invitation = new Invitation
            {
                Firstname = invitationToCreate.Firstname,
                Lastname = invitationToCreate.Lastname,
                Email = invitationToCreate.Email,
                ExpirationDate = invitationToCreate.ExpirationDate
            };

            _invitationServiceLogic.CreateInvitation(invitation);
            
            CreateInvitationResponse invitationResponse = new CreateInvitationResponse { Id = invitation.Id };

            return invitationResponse;
        }
        catch (ObjectRepeatedServiceException exceptionCaught)
        {
            throw new ObjectRepeatedAdapterException(exceptionCaught.Message);
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

    public void UpdateInvitation(Guid idOfInvitationToUpdate, UpdateInvitationRequest invitationToUpdateRequest)
    {
        try
        {
            Invitation invitation = new Invitation
            {
                Status = (StatusEnum)invitationToUpdateRequest.Status,
                ExpirationDate = invitationToUpdateRequest.ExpirationDate
            };

            _invitationServiceLogic.UpdateInvitation(idOfInvitationToUpdate, invitation);
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException();
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (ObjectRepeatedServiceException exceptionCaught)
        {
            throw new ObjectRepeatedAdapterException(exceptionCaught.Message);
        }
    }

    public void DeleteInvitation(Guid idOfInvitationToDelete)
    {
        _invitationServiceLogic.DeleteInvitation(idOfInvitationToDelete);
    }
}