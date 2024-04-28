namespace Domain;

public class InvalidOwnerException : Exception
{
    public InvalidOwnerException(string message) : base(message)
    {
    }
}
