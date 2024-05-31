using WebModel.Requests.BuildingRequests;
using WebModel.Responses.LoaderReponses;

namespace ILoaders;

public interface ILoader
{
    public string LoaderName();
    public IEnumerable<CreateBuildingFromLoadResponse> LoadAllBuildings(string filePath);
}