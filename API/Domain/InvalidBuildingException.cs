namespace Domain;

public class InvalidBuildingException : Exception
{
    public InvalidBuildingException(string message) : base(message)
    {
    }
}