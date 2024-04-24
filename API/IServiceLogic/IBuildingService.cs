using Domain;

namespace IServiceLogic;

public interface IBuildingService
{
    public IEnumerable<Building> GetAllBuildings();
    public Building GetBuildingById (Guid buildingId);
}