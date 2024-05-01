namespace Domain;

public abstract class SystemUser : Person
{
    public string Password { get; set; }
    
    public string Role { get; set; }
    
    public void PasswordValidator()
    {
        if (string.IsNullOrEmpty(Password) || Password.Length < 8)
        {
            throw new InvalidRequestHandlerException("Password must have at least 8 characters");
        }
    }
    
}