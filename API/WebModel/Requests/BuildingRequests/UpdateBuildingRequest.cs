using WebModel.Requests.ConstructionCompanyRequests;

namespace WebModel.Requests.BuildingRequests;

public class UpdateBuildingRequest
{
    public Guid ConstructionCompanyId { get;}
    public int CommonExpenses { get;}
}