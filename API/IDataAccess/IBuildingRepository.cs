using Domain;

namespace IRepository;

public interface IBuildingRepository
{
    public IEnumerable<Building> GetAllBuildings(Guid managerId);
    public Building GetBuildingById(Guid buildingId);
    public void CreateBuilding(Building building);
    public void UpdateBuilding(Building building);
    public void DeleteBuilding(Building buildingToDelete);
}