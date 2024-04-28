namespace Domain;

public class InvalidInvitationException : Exception
{
    public InvalidInvitationException(string message) : base(message)
    {
    }
}
