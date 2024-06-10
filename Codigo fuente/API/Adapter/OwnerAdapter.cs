using Adapter.CustomExceptions;
using Domain;
using IAdapter;
using IServiceLogic;
using Microsoft.AspNetCore.Mvc;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.OwnerRequests;
using WebModel.Responses.OwnerResponses;

namespace Adapter;

public class OwnerAdapter : IOwnerAdapter
{
    private readonly IOwnerService _ownerService;

    public OwnerAdapter(IOwnerService ownerService)
    {
        _ownerService = ownerService;
    }

    public IEnumerable<GetOwnerResponse> GetAllOwners()
    {
        try
        {
            IEnumerable<Owner> owners = _ownerService.GetAllOwners();

            IEnumerable<GetOwnerResponse> ownerResponses = owners.Select(owner => new GetOwnerResponse
            {
                Id = owner.Id,
                Firstname = owner.Firstname,
                Lastname = owner.Lastname,
                Email = owner.Email
            });

            return ownerResponses;
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    public GetOwnerResponse GetOwnerById(Guid ownerId)
    {
        try
        {
            Owner ownerFound = _ownerService.GetOwnerById(ownerId);
            GetOwnerResponse ownerResponse = new GetOwnerResponse
            {
                Id = ownerFound.Id,
                Firstname = ownerFound.Firstname,
                Lastname = ownerFound.Lastname,
                Email = ownerFound.Email
            };
            return ownerResponse;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Owner was not found");
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    public CreateOwnerResponse CreateOwner(CreateOwnerRequest createRequest)
    {
        try
        {
            Owner ownerToCreate = new Owner
            {
                Firstname = createRequest.Firstname,
                Lastname = createRequest.Lastname,
                Email = createRequest.Email
            };

            _ownerService.CreateOwner(ownerToCreate);

            CreateOwnerResponse createOwnerResponse = new CreateOwnerResponse
            {
                Id = ownerToCreate.Id
            };
            return createOwnerResponse;
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
    

    public void UpdateOwnerById(Guid idOfOwnerToUpdate, UpdateOwnerRequest updateRequest)
    {
        try
        {
            Owner ownerWithUpdates = new Owner
            {
                Id = idOfOwnerToUpdate,
                Firstname = updateRequest.Firstname,
                Lastname = updateRequest.Lastname,
                Email = updateRequest.Email
            };

            _ownerService.UpdateOwnerById(ownerWithUpdates);
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Owner was not found");
        }
        catch (ObjectRepeatedServiceException)
        {
            throw new ObjectRepeatedAdapterException();
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }
}