using Domain;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class FlatService : IFlatService
{
    private readonly IFlatRepository _flatRepository;

    public FlatService(IFlatRepository flatRepository)
    {
        _flatRepository = flatRepository;
    }

    public IEnumerable<Flat> GetAllFlats(Guid buildingId)
    {
        try
        {
            IEnumerable<Flat> flatsInDb = _flatRepository.GetAllFlats(buildingId);
            return flatsInDb;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    public Flat GetFlatById(Guid buildingId, Guid flatId)
    {
        Flat flatFound;
        try
        {
            flatFound = _flatRepository.GetFlatById(buildingId, flatId);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
        if (flatFound is null) throw new ObjectNotFoundServiceException();
        return flatFound;
    }

    public void CreateFlat(Flat flatToAdd)
    {

        try
        {
            flatToAdd.FlatValidator();
            _flatRepository.CreateFlat(flatToAdd);
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