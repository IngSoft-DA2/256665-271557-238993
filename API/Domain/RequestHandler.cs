using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class RequestHandler : SystemUser
{
    public string LastName { get; set; }
    
    public void RequestValidator()
    {
        PersonValidator();
        if (string.IsNullOrEmpty(LastName)) throw new InvalidRequestHandlerException("Last name is required");
        PasswordValidator();
    }
    public override bool Equals(object objectToCompare)
    {
        RequestHandler? toCompare = objectToCompare as RequestHandler;

        bool areEqual =  base.Equals(toCompare) &&
               LastName == toCompare.LastName &&
               Password == toCompare.Password;

        return areEqual;
    }
    
   
}