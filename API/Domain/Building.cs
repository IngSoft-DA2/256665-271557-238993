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
}