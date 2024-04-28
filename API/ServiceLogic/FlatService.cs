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
        
        Flat flatFound = _flatRepository.GetFlatById(buildingId,flatId);
        if (flatFound is null) throw new ObjectNotFoundServiceException();
        
        return flatFound;

    }

    public void CreateFlat(Flat flatToCreate)
    {
        throw new NotImplementedException();
    }
}