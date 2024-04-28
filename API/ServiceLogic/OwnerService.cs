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
        Owner ownerObtained;
        try
        {
            ownerObtained = _ownerRepository.GetOwnerById(ownerIdToObtain);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }

        if (ownerObtained is null) throw new ObjectNotFoundServiceException();

        return ownerObtained;
    }

    public void CreateOwner(Owner ownerToCreate)
    {
        try
        {
            ownerToCreate.OwnerValidator();

            IEnumerable<Owner> ownersInDb = GetAllOwners();

            if (ownersInDb.Any(owner => owner.Email.Equals(ownerToCreate.Email)))
            {
                throw new ObjectRepeatedServiceException();
            }

            _ownerRepository.CreateOwner(ownerToCreate);
        }
        catch (InvalidOwnerException exceptionCaught)
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

    public void UpdateOwnerById(Owner ownerWithUpdates)
    {
        throw new NotImplementedException();
    }
}