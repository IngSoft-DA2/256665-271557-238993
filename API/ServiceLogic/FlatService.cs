using Domain;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class FlatService
{
    private readonly IFlatRepository _flatRepository;
    public FlatService(IFlatRepository flatRepository)
    {
        _flatRepository = flatRepository;
    }

    public IEnumerable<Flat> GetAllFlats()
    {
        try
        {
            IEnumerable<Flat> flatsInDb = _flatRepository.GetAllFlats();
            return flatsInDb;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
      
    }
}