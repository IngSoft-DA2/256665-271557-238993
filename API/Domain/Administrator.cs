namespace Domain;

public class Administrator : Person
{
    public string LastName { get; set; }
    public string Password { get; set; }
    public IEnumerable<Invitation> Invitations { get; set; }
    public void PasswordValidator()
    {
        if (string.IsNullOrEmpty(Password) || Password.Length < 8)
        {
            throw new InvalidManagerException("Password must have at least 8 characters");
        }
    }
}