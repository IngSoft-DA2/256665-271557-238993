using ILoaders;
using WebModel.Requests.LoaderRequests;
using WebModel.Responses.BuildingResponses;

namespace IAdapter;

public interface ILoaderAdapter
{
    public List<string> GetAllLoaders();
    public List<CreateBuildingResponse> CreateAllBuildingsFromLoad(CreateLoaderRequest createLoaderRequest);

}