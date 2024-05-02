using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Location
{
    public Guid Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    [ForeignKey("Building")] public Guid BuildingId { get; set; }

    public Building Building { get; set; }

    public override bool Equals(object? obj)
    {
        Location locationToCompare = obj as Location;
        return locationToCompare.Id == Id && locationToCompare.Latitude == Latitude &&
               locationToCompare.Longitude == Longitude && locationToCompare.BuildingId == BuildingId &&
               locationToCompare.Building == Building;
    }
}