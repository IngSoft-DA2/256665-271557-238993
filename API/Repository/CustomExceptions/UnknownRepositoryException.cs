namespace Repositories.CustomExceptions;

public class UnknownRepositoryException : Exception
{
    public UnknownRepositoryException(string msg) : base(msg)
    {
    }
}