namespace Domain;

public class InvalidManagerException : Exception
{
    public InvalidManagerException(string message) : base(message)
    {
    }
}