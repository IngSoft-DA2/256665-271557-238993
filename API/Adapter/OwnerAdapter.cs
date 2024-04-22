using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
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
       
    }
}