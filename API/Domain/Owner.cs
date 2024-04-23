namespace Domain;

public class Owner
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }

    public IEnumerable<Flat> Flats { get; set; } = new List<Flat>();
}