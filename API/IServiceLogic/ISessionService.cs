namespace IServiceLogic;

public interface ISessionService
{
    Guid Authenticate(string email, string password);
    void Logout(string sessionId);
}