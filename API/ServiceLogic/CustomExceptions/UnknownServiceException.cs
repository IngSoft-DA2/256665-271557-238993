namespace ServiceLogic.CustomExceptions;

public class UnknownServiceException : Exception
{
    public UnknownServiceException (string message) : base(message)
    {
    }
}