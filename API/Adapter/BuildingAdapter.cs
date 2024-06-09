using Adapter.CustomExceptions;
using Domain;
using IAdapter;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.BuildingRequests;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.ConstructionCompanyResponses;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.ManagerResponses;
using WebModel.Responses.OwnerResponses;

namespace Adapter;

public class BuildingAdapter : IBuildingAdapter
{
    #region Constructor and Attributes

    private readonly IBuildingService _buildingService;
    private readonly IConstructionCompanyService _constructionCompanyService;
    private readonly IOwnerService _ownerService;
    private readonly IManagerService _managerService;

    public BuildingAdapter(IBuildingService buildingService, IConstructionCompanyService constructionCompanyService,
        IOwnerService ownerService, IManagerService managerService)
    {
        _buildingService = buildingService;
        _constructionCompanyService = constructionCompanyService;
        _ownerService = ownerService;
        _managerService = managerService;
    }

    #endregion

    #region Get all buildings

    public IEnumerable<GetBuildingResponse> GetAllBuildings(Guid constructionCompanyAdminId)
    {
        try
        {
            IEnumerable<Building> buildingsInDb = _buildingService.GetAllBuildings(constructionCompanyAdminId);
            List<GetBuildingResponse> buildingsToReturn = buildingsInDb.Select(building => new GetBuildingResponse
            {
                Id = building.Id,
                Manager = new GetManagerResponse
                {
                    Id = building.Manager.Id,
                    Name = building.Manager.Firstname,
                    Email = building.Manager.Email,
                    BuildingsId = building.Manager.Buildings.Select(building => building.Id).ToList(),
                    MaintenanceRequestsId = building.Manager.Requests.Select(maintenance => maintenance.Id).ToList(),
                },
                Name = building.Name,
                Address = building.Address,
                Location = new LocationResponse()
                {
                    Latitude = building.Location.Latitude,
                    Longitude = building.Location.Longitude
                },
                ConstructionCompany = new GetConstructionCompanyResponse
                {
                    Id = building.ConstructionCompany.Id,
                    Name = building.ConstructionCompany.Name,
                    UserCreatorId = building.ConstructionCompany.UserCreatorId,
                    BuildingsId = building.ConstructionCompany.Buildings.Select(building => building.Id).ToList()
                },
                CommonExpenses = building.CommonExpenses,
                Flats = building.Flats.Select(flat => new GetFlatResponse
                {
                    Id = flat.Id,
                    Floor = flat.Floor,
                    RoomNumber = flat.RoomNumber,
                    OwnerAssigned = flat.OwnerAssigned != null
                        ? new GetOwnerResponse
                        {
                            Id = flat.OwnerAssigned.Id,
                            Firstname = flat.OwnerAssigned.Firstname,
                            Lastname = flat.OwnerAssigned.Lastname,
                            Email = flat.OwnerAssigned.Email
                        }
                        : null,
                    TotalRooms = flat.TotalRooms,
                    TotalBaths = flat.TotalBaths,
                    HasTerrace = flat.HasTerrace
                }).ToList()
            }).ToList();

            return buildingsToReturn;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }

    #endregion

    #region Get building by Id

    public GetBuildingResponse GetBuildingById(Guid idOfBuilding)
    {
        try
        {
            Building buildingInDb = _buildingService.GetBuildingById(idOfBuilding);
            GetBuildingResponse buildingToReturn = new GetBuildingResponse
            {
                Id = buildingInDb.Id,
                Manager = new GetManagerResponse
                {
                    Id = buildingInDb.Manager.Id,
                    Name = buildingInDb.Manager.Firstname,
                    Email = buildingInDb.Manager.Email,
                    BuildingsId = buildingInDb.Manager.Buildings.Select(building => building.Id).ToList(),
                    MaintenanceRequestsId = buildingInDb.Manager.Requests.Select(maintenance => maintenance.Id).ToList(),
                },
                Name = buildingInDb.Name,
                Address = buildingInDb.Address,
                Location = new LocationResponse()
                {
                    Latitude = buildingInDb.Location.Latitude,
                    Longitude = buildingInDb.Location.Longitude
                },
                ConstructionCompany = new GetConstructionCompanyResponse
                {
                    Id = buildingInDb.ConstructionCompany.Id,
                    Name = buildingInDb.ConstructionCompany.Name,
                    UserCreatorId = buildingInDb.ConstructionCompany.UserCreatorId,
                    BuildingsId = buildingInDb.ConstructionCompany.Buildings.Select(building => building.Id).ToList()
                },
                CommonExpenses = buildingInDb.CommonExpenses,
                Flats = buildingInDb.Flats.Select(flat => new GetFlatResponse
                {
                    Id = flat.Id,
                    Floor = flat.Floor,
                    RoomNumber = flat.RoomNumber,
                    OwnerAssigned = flat.OwnerAssigned != null
                        ? new GetOwnerResponse
                        {
                            Id = flat.OwnerAssigned.Id,
                            Firstname = flat.OwnerAssigned.Firstname,
                            Lastname = flat.OwnerAssigned.Lastname,
                            Email = flat.OwnerAssigned.Email
                        }
                        : null,
                    TotalRooms = flat.TotalRooms,
                    TotalBaths = flat.TotalBaths,
                    HasTerrace = flat.HasTerrace
                }).ToList()
            };

            return buildingToReturn;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Building was not found");
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }

    #endregion

    #region Create building

    public CreateBuildingResponse CreateBuilding(CreateBuildingRequest createBuildingRequest)
    {
        try
        {
            Building buildingToCreate = new Building
            {
                Id = Guid.NewGuid(),
                Name = createBuildingRequest.Name,
                Address = createBuildingRequest.Address,
                Location = new Location
                {
                    Id = Guid.NewGuid(),
                    Latitude = createBuildingRequest.Location.Latitude,
                    Longitude = createBuildingRequest.Location.Longitude
                },
                ConstructionCompany =
                    _constructionCompanyService.GetConstructionCompanyById(createBuildingRequest.ConstructionCompanyId),
                CommonExpenses = createBuildingRequest.CommonExpenses,

                Flats = createBuildingRequest.Flats.Select(flat => new Flat
                {
                    Id = Guid.NewGuid(),
                    Floor = flat.Floor,
                    RoomNumber = flat.RoomNumber,
                    OwnerAssigned =_ownerService.GetOwnerById(flat.OwnerAssignedId),
                    TotalRooms = flat.TotalRooms,
                    TotalBaths = flat.TotalBaths,
                    HasTerrace = flat.HasTerrace
                }).ToList(),
                Manager  = _managerService.GetManagerById(createBuildingRequest.ManagerId),
                ManagerId = createBuildingRequest.ManagerId
            };
            _buildingService.CreateBuilding(buildingToCreate);

            CreateBuildingResponse buildingResponse = new CreateBuildingResponse
            {
                Id = buildingToCreate.Id
            };

            return buildingResponse;
        }

        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Construction Company,Owner or Manager were not found");
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (ObjectRepeatedServiceException)
        {
            throw new ObjectRepeatedAdapterException();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }

    #endregion

    #region Update Building

    public void UpdateBuildingById(Guid buildingIdToUpd, UpdateBuildingRequest updateBuildingRequest)
    {
        Manager managerFound = _managerService.GetManagerById(updateBuildingRequest.ManagerId);

        try
        {
            Building buildingToUpd = new Building
            {
                Id = buildingIdToUpd,
                CommonExpenses = updateBuildingRequest.CommonExpenses,
                Manager = managerFound,
                ManagerId = managerFound.Id
            };
            _buildingService.UpdateBuilding(buildingToUpd);
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Building was not found");
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }

    #endregion

    #region Delete building

    public void DeleteBuildingById(Guid buildingIdToDelete)
    {
        try
        {
            _buildingService.DeleteBuilding(buildingIdToDelete);
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Building was not found");
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }

    #endregion
}