using IAdapter;
using ILoaders;
using IServiceLogic;
using WebModel.Requests.BuildingRequests;
using WebModel.Requests.LoaderRequests;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.LoaderReponses;

namespace Adapter.CustomExceptions;

public class LoaderAdapter : ILoaderAdapter
{
    private readonly ILoaderService _loaderService;
    
    public LoaderAdapter(ILoaderService loaderService)
    {
        _loaderService = loaderService;
    }
    public List<string> GetAllLoaders()
    {
        throw new NotImplementedException();
    }

    public List<CreateBuildingFromLoadResponse> CreateAllBuildingsFromLoad(CreateLoaderRequest createLoaderRequestWithSettings)
    {
        List<CreateBuildingFromLoadResponse> buildings = new List<CreateBuildingFromLoadResponse>();

        List<ILoader> loaders = _loaderService.GetAllImporters();
        
        ILoader loaderNeeded = loaders.Where(loader => loader.LoaderName().ToLower().Contains(createLoaderRequestWithSettings.LoaderName.ToLower())).First();
        
        if (loaderNeeded != null)
        {
            buildings = loaderNeeded.LoadAllBuildings(createLoaderRequestWithSettings.Filepath).ToList();
        }
        
        foreach (var building in buildings)
        {
            _loaderService.CreateBuildingFromLoad(createLoaderRequestWithSettings);
        }

        return buildings;
    }
}