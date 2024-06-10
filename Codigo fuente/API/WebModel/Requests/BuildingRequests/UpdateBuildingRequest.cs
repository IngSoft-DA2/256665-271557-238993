using WebModel.Requests.ConstructionCompanyRequests;

namespace WebModel.Requests.BuildingRequests;

public class UpdateBuildingRequest
{
    public Guid ManagerId { get; set; }
    public int CommonExpenses { get; set; }
}