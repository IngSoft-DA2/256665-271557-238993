namespace Domain;

public class Administrator : User
{
    public string LastName { get; set; }
    public IEnumerable<Invitation> Invitations { get; set; }
}