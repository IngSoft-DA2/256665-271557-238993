namespace Domain;

public class Owner : Person
{
    public string Lastname { get; set; }
    public IEnumerable<Flat> Flats { get; set; } = new List<Flat>();
    
    public override void PersonValidator()
    {
        base.PersonValidator();
        ValidateName(Lastname,"Lastname");
    }
    
    public override bool Equals(object? objectToCompare)
    {
        Owner? ownerToCompare = objectToCompare as Owner;

        return Id == ownerToCompare.Id && Firstname == ownerToCompare.Firstname &&
               Lastname == ownerToCompare.Lastname && Email == ownerToCompare.Email
               && Flats.SequenceEqual(ownerToCompare.Flats);
    }
}