namespace ServiceLogic.CustomExceptions;

public class ObjectRepeatedServiceException : Exception
{
    public ObjectRepeatedServiceException(string msg) : base(msg)
    {
    }
}