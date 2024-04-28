namespace Domain;

public class InvalidCategoryException : Exception
{
    public InvalidCategoryException(string message) : base(message)
    {
    }
}
