using Domain;

namespace IRepository;

public interface IBuildingRepository
{
    public IEnumerable<Building> GetAllBuildings();
    public Building GetBuildingById(Guid buildingId);
    public void CreateBuilding(Building building);
}