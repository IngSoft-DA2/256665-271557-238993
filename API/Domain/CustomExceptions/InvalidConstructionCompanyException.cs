namespace Domain;

public class InvalidConstructionCompanyException : Exception
{
    public InvalidConstructionCompanyException(string message) : base(message)
    {
    }
}
