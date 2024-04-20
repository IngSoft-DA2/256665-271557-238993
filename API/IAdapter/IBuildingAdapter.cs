using WebModel.Responses.BuildingResponses;
using WebModel.Responses.InvitationResponses;

namespace IAdapter;

public interface IBuildingAdapter
{

    public IEnumerable<GetBuildingResponse> GetBuildings(Guid userId);
}