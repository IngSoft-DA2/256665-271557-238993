using Domain;

namespace IServiceLogic;

public interface IFlatRepository
{
    public IEnumerable<Flat> GetAllFlats();
}