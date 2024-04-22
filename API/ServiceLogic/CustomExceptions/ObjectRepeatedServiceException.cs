namespace ServiceLogic.CustomExceptions;

public class ObjectRepeatedServiceException : Exception
{
    public ObjectRepeatedServiceException(string message) : base(message)
    {
    }
}