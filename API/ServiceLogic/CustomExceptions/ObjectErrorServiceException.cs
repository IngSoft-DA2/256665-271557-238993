namespace ServiceLogic.CustomExceptions;

public class ObjectErrorServiceException : Exception
{
    public ObjectErrorServiceException(string msg) : base(msg)
    {
    }
}