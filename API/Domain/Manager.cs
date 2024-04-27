namespace Domain;

public class Manager
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public IEnumerable<Guid> Buildings { get; set; }
}