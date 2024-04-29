namespace Domain;

public class Administrator : Person
{
    public string LastName { get; set; }
    public string Password { get; set; }
    public IEnumerable<Invitation> Invitations { get; set; }
}