namespace Domain;

public class InvalidSystemUserException : Exception
{
    public InvalidSystemUserException(string message) : base(message)
    {
    }
}