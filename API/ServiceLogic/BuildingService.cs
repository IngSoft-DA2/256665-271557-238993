using Domain;
using IRepository;

namespace ServiceLogic;

public class BuildingService
{
    
    private readonly IBuildingRepository _buildingRepository;
    
    public BuildingService(IBuildingRepository buildingRepository)
    {
        _buildingRepository = buildingRepository;
    }

    public IEnumerable<Building> GetAllBuildings()
    {
        return _buildingRepository.GetAllBuildings();
    }
}