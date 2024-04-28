namespace Domain;

public class Manager : User
{
    public IEnumerable<Guid> Buildings { get; set; }
}