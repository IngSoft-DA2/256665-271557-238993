using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.BuildingRequests;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.InvitationResponses;

namespace IAdapter;

public interface IBuildingAdapter
{

    public IEnumerable<GetBuildingResponse> GetAllBuildings(Guid userId);
    public GetBuildingResponse GetBuildingById(Guid buildingId);
    public void UpdateBuilding(Guid idOfBuilding, UpdateBuildingRequest buildingWithUpdates);
    public CreateBuildingResponse CreateBuilding(CreateBuildingRequest buildingToCreate);
    public void DeleteBuilding(Guid buildingId);
    public IEnumerable<GetFlatResponse> GetAllFlatsByBuilding(Guid buildingId);

}