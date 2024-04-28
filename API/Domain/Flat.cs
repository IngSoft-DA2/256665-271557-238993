namespace Domain;

public class Flat
{
    public Guid Id { get; set; }
    public Guid BuildingId { get; set; }
    public int Floor { get; set; }
    public int RoomNumber { get; set; }
    public Owner? OwnerAssigned { get; set; }
    public int TotalRooms { get; set; }
    public int TotalBaths { get; set; }
    public bool HasTerrace { get; set; }
    
}