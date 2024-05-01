namespace Domain;

public class RequestHandler : Person
{
    public string LastName { get; set; }
    public string Password { get; set; }
    
    public void RequestValidator()
    {
        PersonValidator();
        if (string.IsNullOrEmpty(LastName)) throw new InvalidRequestHandlerException("Last name is required");
        PasswordValidator();
    }

    private void PasswordValidator()
    {
        if (string.IsNullOrEmpty(Password) || Password.Length < 8)
        {
            throw new InvalidRequestHandlerException("Password must have at least 8 characters");
        }
    }
    
    public override bool Equals(object objectToCompare)
    {
        RequestHandler? toCompare = objectToCompare as RequestHandler;

        return base.Equals(toCompare) &&
               LastName == toCompare.LastName &&
               Password == toCompare.Password;
    }
    
   
}