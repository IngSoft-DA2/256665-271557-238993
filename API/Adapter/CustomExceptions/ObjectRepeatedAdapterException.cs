namespace Adapter.CustomExceptions;

public class ObjectRepeatedAdapterException : Exception
{
    public ObjectRepeatedAdapterException(string msg) : base(msg)
    {
    }
}