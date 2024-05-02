namespace Adapter.CustomExceptions;

public class UnknownAdapterException : Exception
{
    public UnknownAdapterException(string msg) : base(msg)
    {
    }
}