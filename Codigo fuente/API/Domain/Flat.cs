namespace Domain;

public class Flat
{
    public Guid Id { get; set; }
    public Guid BuildingId { get; set; }
    public int Floor { get; set; }
    public string RoomNumber { get; set; }
    public Owner OwnerAssigned { get; set; }
    public Guid OwnerId { get; set; }
    public int TotalRooms { get; set; }
    public int TotalBaths { get; set; }
    public bool HasTerrace { get; set; }


    public void FlatValidator()
    {
        if (TotalRooms <= 0) throw new InvalidFlatException("Total rooms must be greater than 0");
        if(TotalBaths < 0) throw new InvalidFlatException("Total baths must be greater than or equal to 0");
    }


    public override bool Equals(object? objectToCompare)
    {
        Flat? flatToCompare = objectToCompare as Flat;
        
        return Id == flatToCompare.Id && BuildingId == flatToCompare.BuildingId && Floor == flatToCompare.Floor &&
               RoomNumber == flatToCompare.RoomNumber && OwnerId == flatToCompare.OwnerId &&
               TotalRooms == flatToCompare.TotalRooms && TotalBaths == flatToCompare.TotalBaths &&
               HasTerrace == flatToCompare.HasTerrace;
    }
}