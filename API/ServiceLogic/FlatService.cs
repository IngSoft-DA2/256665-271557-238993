using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class FlatService : IFlatService
{
    private readonly ISessionService _sessionService;
    public FlatService(ISessionService sessionService)
    {
        _sessionService = sessionService;
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
        if (!_sessionService.IsUserAuthenticated(flatToAdd.OwnerAssigned.Email))
        {
             throw new ObjectErrorServiceException("Owner is not authenticated to let him be assigned to the flat.");
        }
    }
}