using Domain;

namespace IServiceLogic;

public interface IFlatService
{

    public IEnumerable<Flat> GetAllFlats(Guid buildingId);
}