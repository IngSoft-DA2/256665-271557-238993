using Domain.Enums;

namespace Domain;

public class Invitation
{
    public Guid Id = Guid.NewGuid();
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public DateTime ExpirationDate { get; set; }
    public StatusEnum Status { get; set; }
}