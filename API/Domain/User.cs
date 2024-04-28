using System.Security.AccessControl;

namespace Domain;

public abstract class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Firstname { get; set; }
    public string Password { get; set; }
}