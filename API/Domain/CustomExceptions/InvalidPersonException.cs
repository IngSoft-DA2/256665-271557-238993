namespace Domain;

public class InvalidPersonException : Exception
{
    public InvalidPersonException(string message) : base(message)
    {
    }
}