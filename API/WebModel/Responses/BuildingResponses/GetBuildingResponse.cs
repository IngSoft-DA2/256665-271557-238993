using Domain;
using WebModel.Responses.ConstructionCompanyResponses;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.ManagerResponses;

namespace WebModel.Responses.BuildingResponses;

public class GetBuildingResponse
{
    public Guid Id { get; set; }
    public GetManagerResponse Manager { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public LocationResponse Location { get; set; }
    public GetConstructionCompanyResponse ConstructionCompany { get; set; }
    public double CommonExpenses { get; set; }
    public IEnumerable<GetFlatResponse> Flats { get; set; } = new List<GetFlatResponse>();

    public override bool Equals(object? toCompare)
    {
        GetBuildingResponse? buildingToCompare = toCompare as GetBuildingResponse;

        return Id == buildingToCompare.Id
               && Name == buildingToCompare.Name
            && Manager.Equals(buildingToCompare.Manager)
            && Address == buildingToCompare.Address
            && Location.Equals(buildingToCompare.Location)
            && ConstructionCompany.Equals(buildingToCompare.ConstructionCompany)
            && Math.Abs(CommonExpenses - buildingToCompare.CommonExpenses) < 0.02
            && Flats.SequenceEqual(buildingToCompare.Flats);
    }
}