namespace Domain;

public class Owner
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }

    public IEnumerable<Flat> Flats { get; set; } = new List<Flat>();


    public override bool Equals(object? objectToCompare)
    {
        Owner? ownerToCompare = objectToCompare as Owner;
        if (ownerToCompare is null) return false;

        return Id == ownerToCompare.Id && Firstname == ownerToCompare.Firstname &&
               Lastname == ownerToCompare.Lastname && Email == ownerToCompare.Email;
    }
}