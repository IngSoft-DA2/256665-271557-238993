namespace Domain;

public class Building
{
    public Guid Id { get; set; }
    public Guid ManagerId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public Location Location { get; set; }
    public ConstructionCompany ConstructionCompany { get; set; }
    public int CommonExpenses { get; set; }
    public IEnumerable<Flat> Flats { get; set; } = new List<Flat>();

    public override bool Equals(object objectToCompare)
    {
        Building? toCompare = objectToCompare as Building;

        if (toCompare is null) return false;
        
        return Id == toCompare.Id &&
               ManagerId == toCompare.ManagerId &&
               Name == toCompare.Name &&
               Address == toCompare.Address &&
               Location.Equals(toCompare.Location) &&
               ConstructionCompany.Equals(toCompare.ConstructionCompany) &&
               CommonExpenses == toCompare.CommonExpenses &&
               Flats.SequenceEqual(toCompare.Flats);
    }
}