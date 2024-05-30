namespace Domain.CustomExceptions;

public class InvalidConstructionCompanyAdminException : Exception
{
    public InvalidConstructionCompanyAdminException(string message) : base(message)
    {
    }
}