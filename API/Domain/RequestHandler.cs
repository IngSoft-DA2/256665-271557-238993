namespace Domain;

public class RequestHandler : Person
{
    public string LastName { get; set; }
    public string Password { get; set; }
    
    

    public void RequestValidator()
    {
        PasswordValidator();
    }
    
    private void PasswordValidator()
    {
        if (string.IsNullOrEmpty(Password) || Password.Length < 8)
        {
            throw new InvalidRequestHandlerException("Password must have at least 8 characters");
        }
    }
    
    
}