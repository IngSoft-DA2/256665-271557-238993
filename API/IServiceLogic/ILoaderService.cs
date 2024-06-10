using ILoaders;
using WebModel.Requests.LoaderRequests;
using WebModel.Responses.LoaderReponses;

namespace IServiceLogic;

public interface ILoaderService
{
    public List<ILoader> GetAllLoaders(); 
}