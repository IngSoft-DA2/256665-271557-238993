using Domain;
using IServiceLogic;

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
        IEnumerable<Flat> flatsInDb = _flatRepository.GetAllFlats();
        return flatsInDb;
    }
}