using WebModel.Requests.FlatRequests;

namespace WebModel.Requests.BuildingRequests;

public class CreateBuildingRequest
{
    public Guid ManagerId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public LocationRequest Location { get; set; }
    public Guid ConstructionCompanyId { get; set; }
    public int CommonExpenses { get; set; }
    public IEnumerable<CreateFlatRequest> Flats { get; set; } = new List<CreateFlatRequest>();
}