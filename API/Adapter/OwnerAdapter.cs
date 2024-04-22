using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.OwnerRequests;
using WebModel.Responses.OwnerResponses;

namespace Adapter;

public class OwnerAdapter
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
            throw new ObjectNotFoundAdapterException();
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    public CreateOwnerResponse CreateOwner(CreateOwnerRequest ownerRequest)
    {

        try
        {
            Owner ownerToCreate = new Owner
            {
                Firstname = ownerRequest.Firstname,
                Lastname = ownerRequest.Lastname,
                Email = ownerRequest.Email
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
      
    }
}