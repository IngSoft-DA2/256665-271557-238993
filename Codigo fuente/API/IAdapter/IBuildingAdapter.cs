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
    public void UpdateBuildingById(Guid idOfBuilding, UpdateBuildingRequest buildingWithUpdates);
    public CreateBuildingResponse CreateBuilding(CreateBuildingRequest buildingToCreate);
    public void DeleteBuildingById(Guid buildingId);

}