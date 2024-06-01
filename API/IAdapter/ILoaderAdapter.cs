using ILoaders;
using WebModel.Requests.LoaderRequests;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.LoaderReponses;

namespace IAdapter;

public interface ILoaderAdapter
{
    public List<string> GetAllLoaders();
    public List<CreateBuildingFromLoadResponse> CreateAllBuildingsFromLoad(CreateLoaderRequest createLoaderRequest);

}