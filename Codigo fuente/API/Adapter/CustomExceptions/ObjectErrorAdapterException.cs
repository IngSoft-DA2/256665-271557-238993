namespace Adapter.CustomExceptions;

public class ObjectErrorAdapterException : Exception
{
    public ObjectErrorAdapterException(string msg) : base(msg)
    {
    }
}