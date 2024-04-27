namespace ServiceLogic.CustomExceptions;

public class UnknownServiceException : Exception
{
    public UnknownServiceException(string msg) : base(msg)
    {
    }
}