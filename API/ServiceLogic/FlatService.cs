using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class FlatService : IFlatService
{
    private readonly IOwnerService _ownerService;
    public FlatService(IOwnerService ownerService)
    {
        _ownerService = ownerService;
    }

    public void CreateFlat(Flat flatToAdd)
    {
        try
        {
            flatToAdd.FlatValidator();
            ValidateOwnerAssigned(flatToAdd);
        }
        catch (InvalidFlatException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    private void ValidateOwnerAssigned(Flat flatToAdd)
    {

        IEnumerable<Owner> ownersInDb = _ownerService.GetAllOwners();
        bool ownerExists = ownersInDb.Any(owner => owner.Id == flatToAdd.OwnerAssigned.Id);
        
        if (!ownerExists)
        {
             throw new ObjectErrorServiceException("Owner is not authenticated to let him be assigned to the flat.");
        }
    }
}