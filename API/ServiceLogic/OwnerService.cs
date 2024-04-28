using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

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
        try
        {
            return _ownerRepository.GetAllOwners();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    public Owner GetOwnerById(Guid ownerIdToObtain)
    {
        return _ownerRepository.GetOwnerById(ownerIdToObtain);
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