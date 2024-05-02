namespace Domain;

public class InvalidRequestHandlerException : Exception
{
    public InvalidRequestHandlerException(string message) : base(message)
    {
    }
    
}