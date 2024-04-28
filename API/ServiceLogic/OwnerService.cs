using System.Reflection;
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
            CheckThatEmailIsNotUsed(ownerToCreate);
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

    private void CheckThatEmailIsNotUsed(Owner ownerToCreate)
    {
        IEnumerable<Owner> ownersInDb = GetAllOwners();

        if (ownersInDb.Any(owner => owner.Email.Equals(ownerToCreate.Email)))
        {
            throw new ObjectRepeatedServiceException();
        }
    }

    public void UpdateOwnerById(Owner ownerWithUpdates)
    {
        Owner ownerWithoutUpdates = GetOwnerById(ownerWithUpdates.Id);
        try
        {
            ownerWithUpdates.OwnerValidator();
            
            IEnumerable<Owner> ownersInDb = GetAllOwners();
            
            if (ownersInDb.Any(owner => owner.Email.Equals(ownerWithUpdates.Email) && owner.Id != ownerWithUpdates.Id))
            {
                throw new ObjectRepeatedServiceException();
            }
            
            MapProperties(ownerWithUpdates, ownerWithoutUpdates);
            _ownerRepository.UpdateOwnerById(ownerWithUpdates);
        }
        catch (InvalidOwnerException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
     
    }
    
    private static void MapProperties(Owner ownerWithUpdates, Owner ownerWithoutUpdates)
    {
        foreach (PropertyInfo property in typeof(Owner).GetProperties())
        {
            object? originalValue = property.GetValue(ownerWithoutUpdates);
            object? updatedValue = property.GetValue(ownerWithUpdates);

            if (updatedValue == null && originalValue != null)
            {
                property.SetValue(ownerWithUpdates, originalValue);
            }
        }
    }
}