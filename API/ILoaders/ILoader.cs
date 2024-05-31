using WebModel.Requests.BuildingRequests;

namespace ILoaders;

public interface ILoader
{
    public List<CreateBuildingRequest> LoadAllBuildings();
    
}