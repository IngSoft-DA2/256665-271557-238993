using WebModel.Requests.FlatRequests;

namespace WebModel.Requests.BuildingRequests;

public class CreateBuildingRequest
{
    public string Name { get; set; }
    public string Address { get; set; }
    public LocationRequest Location { get; set; }
    public string ConstructionCompany { get; set; }
    public double CommonExpenses { get; set; }
    public IEnumerable<CreateFlatRequest>? Flats { get; set; }
}