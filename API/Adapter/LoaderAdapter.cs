using Adapter.CustomExceptions;
using Domain;
using IAdapter;
using ILoaders;
using IServiceLogic;
using WebModel.Requests.LoaderRequests;
using WebModel.Responses.LoaderReponses;

namespace Adapter;

public class LoaderAdapter : ILoaderAdapter
{
    private readonly ILoaderService _loaderService;
    private readonly IBuildingService _buildingService;
    private readonly IConstructionCompanyService _constructionCompanyService;
    private readonly IManagerService _managerService;
    private readonly IOwnerService _ownerService;
    private readonly ISessionService _sessionService;

    public LoaderAdapter (ILoaderService loaderService, IBuildingService buildingService, IManagerService managerService, IOwnerService ownerService, ISessionService sessionService, IConstructionCompanyService constructionCompanyService)
    {
        _loaderService = loaderService;
        _buildingService = buildingService;
        _managerService = managerService;
        _ownerService = ownerService;
        _sessionService = sessionService;
        _constructionCompanyService = constructionCompanyService;
        
    }

    public List<string> GetAllLoaders()
    {
        try
        {
            List<string> loadersResponse = _loaderService.GetAllLoaders()
                .Select(importer => importer.LoaderName())
                .ToList();
            
            return loadersResponse;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownAdapterException("Error getting all loaders");
        }
    }

    public List<CreateBuildingFromLoadResponse> CreateAllBuildingsFromLoad(
        ImportBuildingFromFileRequest importBuildingFromFileRequestWithSettings, Guid sessionStringOfUser)
    {
        try
        {
            List<CreateBuildingFromLoadResponse> buildingsCreated = new List<CreateBuildingFromLoadResponse>();
            
            string filePath = importBuildingFromFileRequestWithSettings.FilePath;
            
            string fileExtension = filePath.Split('.').Last();

            List<ILoader> loaders = _loaderService.GetAllLoaders();

            Guid idOfCurrentUser = _sessionService.GetUserIdBySessionString(sessionStringOfUser);

            ILoader loaderNeeded = loaders.First(loader => loader.LoaderName().ToUpper().Contains( fileExtension.ToUpper()));
            
            if (loaderNeeded != null)
            {
                IEnumerable<Building> buildingsFromFile = loaderNeeded.LoadAllBuildings(filePath);
                
                IEnumerable<ConstructionCompany> constructionCompanies = _constructionCompanyService.GetAllConstructionCompanies();
                ConstructionCompany adminConstructionCompany = constructionCompanies.First(company => company.UserCreatorId == idOfCurrentUser);
                IEnumerable<Manager> managers = _managerService.GetAllManagers();
                IEnumerable<Owner> owners = _ownerService.GetAllOwners();

                foreach (Building building in buildingsFromFile)
                {
                    if (building.Manager != null)
                    {
                        Manager buildingManager = new Manager()
                        {
                            Email = building.Manager.Email
                        };
                            // managers.First(manager => manager.Email == building.Manager.Email);
                        
                        building.Manager = buildingManager;
                        building.ConstructionCompany = adminConstructionCompany;
                        
                        foreach (Flat flat in building.Flats)
                        {
                            // Owner flatOwner = owners.First(owner => owner.Email == flat.OwnerAssigned.Email);
                            Owner flatOwner = new Owner()
                            {
                                Email = flat.OwnerAssigned.Email,
                            };
                            flat.OwnerAssigned = flatOwner;
                        }
                    
                        _buildingService.CreateBuilding(building);

                        buildingsCreated.Add(new CreateBuildingFromLoadResponse()
                        {
                            Details = building.Name,
                            idOfBuildingCreated = building.Id
                        });
                    }
                    
                }

            }
           
            return buildingsCreated;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }
}