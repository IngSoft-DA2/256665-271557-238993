
namespace Domain.CustomExceptions;

public class InvalidSystemUserException : Exception
{
    public InvalidSystemUserException(string message) : base(message)
    {
    }
}