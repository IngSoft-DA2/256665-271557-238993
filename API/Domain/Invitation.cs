namespace Domain;

public class Invitation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Firstname { get; set; }
    public string Email { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime ExpirationDate { get; set; }
}