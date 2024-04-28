using Domain;
using IRepository;
using IServiceLogic;

namespace ServiceLogic;

public class OwnerService : IOwnerService
{

    private readonly IOwnerRepository _ownerRepository;
    public OwnerService(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }
    public IEnumerable<Owner> GetAllOwners()
    {
        return _ownerRepository.GetAllOwners();
    }

    public Owner GetOwnerById(Guid ownerId)
    {
        throw new NotImplementedException();
    }

    public void CreateOwner(Owner ownerToCreate)
    {
        throw new NotImplementedException();
    }

    public void UpdateOwnerById(Owner ownerWithUpdates)
    {
        throw new NotImplementedException();
    }
}