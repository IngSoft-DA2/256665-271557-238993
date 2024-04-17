namespace Adapter.CustomExceptions;

public class ObjectErrorException : Exception
{
    public ObjectErrorException(string msg) : base(msg)
    {
    }
}