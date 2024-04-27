using Domain;
using IRepository;
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
}