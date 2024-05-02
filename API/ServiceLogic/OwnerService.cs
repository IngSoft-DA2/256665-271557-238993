using System.Reflection;
using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class OwnerService : IOwnerService
{
    #region Constructor and dependency injection

    private readonly IOwnerRepository _ownerRepository;

    public OwnerService(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    #endregion

    #region Get all Owners

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

    #endregion

    #region Get Owner By Id

    public Owner GetOwnerById(Guid ownerIdToObtain)
    {
        Owner ownerObtained = new Owner();
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

    #endregion

    #region Create Owner

    public void CreateOwner(Owner ownerToCreate)
    {
        try
        {
            ownerToCreate.PersonValidator();
            CheckIfEmailIsUsed(ownerToCreate);
            _ownerRepository.CreateOwner(ownerToCreate);
        }
        catch (InvalidPersonException exceptionCaught)
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

    #endregion

    #region Update Owner By Id

    public void UpdateOwnerById(Owner ownerWithUpdates)
    {
        Owner ownerWithoutUpdates = GetOwnerById(ownerWithUpdates.Id);
        try
        {
            ownerWithUpdates.PersonValidator();
            ValidationsForBeingPossibleToUpdate(ownerWithUpdates, ownerWithoutUpdates);
            _ownerRepository.UpdateOwnerById(ownerWithUpdates);
        }
        catch (InvalidPersonException exceptionCaught)
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

    #endregion

    #region Auxiliary Functions

    private void ValidationsForBeingPossibleToUpdate(Owner ownerWithUpdates, Owner ownerWithoutUpdates)
    {
        CheckIfEmailIsUsed(ownerWithUpdates);
        ValidateThatAreNotEqual(ownerWithUpdates, ownerWithoutUpdates);
    }

    private void CheckIfEmailIsUsed(Owner ownerWithUpdates)
    {
        IEnumerable<Owner> ownersInDb = GetAllOwners();
        if (ownersInDb.Any(owner => owner.Email.Equals(ownerWithUpdates.Email) && owner.Id != ownerWithUpdates.Id))
        {
            throw new ObjectRepeatedServiceException();
        }
    }
    
    private static void ValidateThatAreNotEqual(Owner ownerWithUpdates, Owner ownerWithoutUpdates)
    {
        if (ownerWithUpdates.Equals(ownerWithoutUpdates)) throw new ObjectRepeatedServiceException();
    }

    #endregion
}