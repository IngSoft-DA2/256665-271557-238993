using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.BuildingRequests;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.InvitationResponses;

namespace IAdapter;

public interface IBuildingAdapter
{

    public IEnumerable<GetBuildingResponse> GetBuildings(Guid userId);
    public GetBuildingResponse GetBuildingById(Guid buildingId);
    public void UpdateBuilding(Guid idOfBuilding, UpdateBuildingRequest buildingWithUpdates);
}