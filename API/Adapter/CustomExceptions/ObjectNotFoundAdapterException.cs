namespace Adapter.CustomExceptions;

public class ObjectNotFoundAdapterException : Exception
{
    public ObjectNotFoundAdapterException(string msg) : base(msg)
    {
    }
}