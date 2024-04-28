using Domain;

namespace IServiceLogic;

public interface IFlatRepository
{
    public IEnumerable<Flat> GetAllFlats(Guid buildingId);
    public Flat GetFlatById(Guid buildingId,Guid idOfFlatToFind);
}