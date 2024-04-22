using Domain;

namespace IServiceLogic;

public interface IFlatService
{
    public IEnumerable<Flat> GetAllFlats(Guid buildingId);
    public Flat GetFlatById(Guid buildingId, Guid flatId);
    public void CreateFlat(Flat flatToCreate);
}