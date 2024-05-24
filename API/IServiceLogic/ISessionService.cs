using Domain;

namespace IServiceLogic;

public interface ISessionService
{
    public Guid Authenticate(string email, string password);
    public void Logout(Guid sessionId);
    public bool IsSessionValid(Guid sessionString);
    public string GetUserRoleBySessionString(Guid sessionStringOfUser);
}