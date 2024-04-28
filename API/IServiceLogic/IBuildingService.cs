using Domain;
using WebModel.Requests.BuildingRequests;
using WebModel.Responses.BuildingResponses;

namespace IServiceLogic;

public interface IBuildingService
{
    public IEnumerable<Building> GetAllBuildings(Guid userId);
    public Building GetBuildingById (Guid buildingId);
    public void CreateBuilding(Building buildingToCreate);
    public void UpdateBuildingById(Guid id, Building buildingWithChanges);
    public void DeleteBuilding(Guid buildingToDelete);
}