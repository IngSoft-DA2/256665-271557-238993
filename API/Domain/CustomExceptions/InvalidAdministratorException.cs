namespace Domain;

public class InvalidAdministratorException : Exception
{
    public InvalidAdministratorException(string message) : base(message)
    {
    }
}