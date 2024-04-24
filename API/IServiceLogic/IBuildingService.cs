using Domain;
using WebModel.Requests.BuildingRequests;
using WebModel.Responses.BuildingResponses;

namespace IServiceLogic;

public interface IBuildingService
{
    public IEnumerable<Building> GetAllBuildings();
    public Building GetBuildingById (Guid buildingId);
    public void CreateBuilding(Building buildingToCreate);
}