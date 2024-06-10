namespace ServiceLogic.CustomExceptions;

public class ObjectErrorServiceException : Exception
{
    public ObjectErrorServiceException (string message) : base(message)
    {
    }
}