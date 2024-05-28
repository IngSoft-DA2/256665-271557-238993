using Adapter.CustomExceptions;
using Domain;
using IAdapter;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.BuildingRequests;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.ConstructionCompanyResponses;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.OwnerResponses;

namespace Adapter;

public class BuildingAdapter : IBuildingAdapter
{
    #region Constructor and Attributes

    private readonly IBuildingService _buildingService;
    private readonly IConstructionCompanyService _constructionCompanyService;
    private readonly IOwnerService _ownerService;

    public BuildingAdapter(IBuildingService buildingService, IConstructionCompanyService constructionCompanyService,
        IOwnerService ownerService)
    {
        _buildingService = buildingService;
        _constructionCompanyService = constructionCompanyService;
        _ownerService = ownerService;
    }

    #endregion

    #region Get all buildings

    public IEnumerable<GetBuildingResponse> GetAllBuildings(Guid managerId)
    {
        try
        {
            IEnumerable<Building> buildingsInDb = _buildingService.GetAllBuildings(managerId);
            List<GetBuildingResponse> buildingsToReturn = buildingsInDb.Select(building => new GetBuildingResponse
            {
                Id = building.Id,
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
                    Name = building.ConstructionCompany.Name
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
                    Name = buildingInDb.ConstructionCompany.Name
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
            throw new ObjectNotFoundAdapterException();
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
                ConstructionCompany = _constructionCompanyService.GetConstructionCompanyById(createBuildingRequest.ConstructionCompanyId),
                CommonExpenses = createBuildingRequest.CommonExpenses,
                
                Flats = createBuildingRequest.Flats.Select(flat => new Flat
                {
                    Id = Guid.NewGuid(),
                    Floor = flat.Floor,
                    RoomNumber = flat.RoomNumber,
                    OwnerAssigned = flat.OwnerAssignedId != null
                        ? _ownerService.GetOwnerById(flat.OwnerAssignedId.Value)
                        : null,
                    TotalRooms = flat.TotalRooms,
                    TotalBaths = flat.TotalBaths,
                    HasTerrace = flat.HasTerrace
                }).ToList(),
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
            throw new ObjectNotFoundAdapterException();
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
        try
        {
            Building buildingToUpd = new Building
            {
                Id = buildingIdToUpd,
                CommonExpenses = updateBuildingRequest.CommonExpenses,
                ConstructionCompanyId = updateBuildingRequest.ConstructionCompanyId
            };
            _buildingService.UpdateBuilding(buildingToUpd);
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException();
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
            throw new ObjectNotFoundAdapterException();
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