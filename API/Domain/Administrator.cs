namespace Domain;

public class Administrator : Person
{
    public string LastName { get; set; }
    public string Password { get; set; }
    public IEnumerable<Invitation> Invitations { get; set; }
    
    public void AdministratorValidator()
    {
        try
        {
            PersonValidator();
        }
        catch (InvalidPersonException exceptionCaught)
        {
            throw new InvalidAdministratorException(exceptionCaught.Message);
        }

        PasswordValidator();
    }
    private void PasswordValidator()
    {
        if (string.IsNullOrEmpty(Password) || Password.Length < 8)
        {
            throw new InvalidAdministratorException("Password must have at least 8 characters");
        }
    }
}