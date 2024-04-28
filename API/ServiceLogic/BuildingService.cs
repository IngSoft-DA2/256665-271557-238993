using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

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
        try
        {
            return _buildingRepository.GetAllBuildings();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    public Building GetBuildingById(Guid buildingId)
    {
        Building buildingFound;
        try
        {
            buildingFound = _buildingRepository.GetBuildingById(buildingId);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }

        if (buildingFound == null)
        {
            throw new ObjectNotFoundServiceException();
        }

        return buildingFound;
    }

    public void CreateBuilding(Building building)
    {
        building.BuildingValidator();
        
        IEnumerable<Building> buildings = _buildingRepository.GetAllBuildings();
        
        if (buildings.Any(b => b.Name == building.Name))
        {
            throw new ObjectRepeatedServiceException();
        }

        
        _buildingRepository.CreateBuilding(building);
    }
}