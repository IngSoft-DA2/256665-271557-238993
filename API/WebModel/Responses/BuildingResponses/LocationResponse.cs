namespace WebModel.Responses.BuildingResponses;

public class LocationResponse
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    public override bool Equals(object? obj)
    {
        LocationResponse? objectToCompare = obj as LocationResponse;
        
        return Latitude == objectToCompare.Latitude && Longitude == objectToCompare.Longitude;
    }
}