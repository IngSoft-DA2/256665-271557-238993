namespace Domain;

public class Manager : Person
{
    public IEnumerable<Guid> Buildings { get; set; }
    public string Password { get; set; }
}