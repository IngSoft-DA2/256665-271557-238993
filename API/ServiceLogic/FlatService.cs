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
}