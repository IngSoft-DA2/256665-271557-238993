using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class FlatService : IFlatService
{
    public FlatService()
    {
    }

    public void CreateFlat(Flat flatToAdd)
    {
        try
        {
            ValidateOwnerAssigned(flatToAdd);
            flatToAdd.FlatValidator();
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

    private static void ValidateOwnerAssigned(Flat flatToAdd)
    {
        if (flatToAdd.OwnerAssigned is null)
        {
            throw new ObjectErrorServiceException("Owner must be assigned");
        }
    }
}