using WebModel.Responses.FlatResponses;

namespace WebModel.Responses.BuildingResponses;

public class GetBuildingResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public LocationResponse Location { get; set; }
    public string ConstructionCompany { get; set; }
    public double CommonExpenses { get; set; }
    public GetFlatResponse Flats { get; set; }

    public override bool Equals(object? toCompare)
    {
        GetBuildingResponse? buildingToCompare = toCompare as GetBuildingResponse;

        if (buildingToCompare is null) return false;

        return Id == buildingToCompare.Id
               && Name == buildingToCompare.Name
               && Address == buildingToCompare.Address
               && Location.Latitude == buildingToCompare.Location.Latitude
               && Location.Longitude == buildingToCompare.Location.Longitude
               && ConstructionCompany == buildingToCompare.ConstructionCompany
               && CommonExpenses == buildingToCompare.CommonExpenses
               && Flats.Equals(buildingToCompare.Flats);
    }
}