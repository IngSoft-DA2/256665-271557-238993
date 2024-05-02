using Domain;

namespace IServiceLogic;

public interface ISessionService
{
    Guid Authenticate(string email, string password);
    void Logout(Guid sessionId);
    SystemUser? GetCurrentUser (Guid? authToken = null);
}