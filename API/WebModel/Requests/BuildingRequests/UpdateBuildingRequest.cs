using WebModel.Requests.ConstructionCompanyRequests;

namespace WebModel.Requests.BuildingRequests;

public class UpdateBuildingRequest
{
    public UpdateConstructionCompanyRequest ConstructionCompany { get; set; }
    public double CommonExpenses { get; set; }
}