using ILoaders;
using WebModel.Requests.LoaderRequests;
using WebModel.Responses.LoaderReponses;

namespace IServiceLogic;

public interface ILoaderService
{
    public CreateBuildingFromLoadResponse CreateBuildingFromLoad(CreateLoaderRequest createLoaderRequest);
    public List<ILoader> GetAllImporters(); 
}