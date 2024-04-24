using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.ConstructionCompanyResponses;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.OwnerResponses;

namespace Adapter;

public class BuildingAdapter
{
    #region Constructor and Attributes

    private readonly IBuildingService _buildingService;

    public BuildingAdapter(IBuildingService buildingService)
    {
        _buildingService = buildingService;
    }

    #endregion


    public IEnumerable<GetBuildingResponse> GetAllBuildings()
    {
        try
        {
            IEnumerable<Building> buildingsInDb = _buildingService.GetAllBuildings();
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
}