namespace Domain;

public class Location
{
    public Guid Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public override bool Equals(object? obj)
    {
        Location locationToCompare = obj as Location;
        return locationToCompare.Id == Id && locationToCompare.Latitude == Latitude &&
               locationToCompare.Longitude == Longitude;
    }
}