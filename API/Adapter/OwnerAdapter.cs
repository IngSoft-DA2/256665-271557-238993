using Domain;
using IServiceLogic;
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

        IEnumerable<Owner> owners = _ownerService.GetAllOwners();
        
        IEnumerable<GetOwnerResponse> ownerResponses = owners.Select(owner => new GetOwnerResponse
        {
            Id = owner.Id,
            Name = owner.Firstname,
            Lastname = owner.Lastname,
            Email = owner.Email
        });

        return ownerResponses;
    }
}