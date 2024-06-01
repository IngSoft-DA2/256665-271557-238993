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
        try
        {
            List<string> loadersResponse = _loaderService.GetAllImporters()
                .Select(importer => importer.LoaderName())
                .ToList();
            
            return loadersResponse;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownAdapterException("Error getting all loaders");
        }
    }

    public List<CreateBuildingFromLoadResponse> CreateAllBuildingsFromLoad(CreateLoaderRequest createLoaderRequestWithSettings)
    {
        try
        {
            List<CreateBuildingFromLoadResponse> buildings = new List<CreateBuildingFromLoadResponse>();

            List<ILoader> loaders = _loaderService.GetAllImporters();

            ILoader loaderNeeded = loaders.Where(loader =>
                loader.LoaderName().ToUpper().Contains(createLoaderRequestWithSettings.LoaderName.ToUpper())).First();

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
        catch (Exception exceptionCaught)
        {
            throw new UnknownAdapterException("Error creating buildings from load");
        }
    }
}