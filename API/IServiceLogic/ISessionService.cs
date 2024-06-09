using Domain;
using Domain.Enums;

namespace IServiceLogic;

public interface ISessionService
{
    public Session Authenticate(string email, string password);
    public void Logout(Guid sessionId);
    public bool IsSessionValid(Guid sessionString);
    public SystemUserRoleEnum GetUserRoleBySessionString(Guid sessionStringOfUser);
    public bool IsUserAuthenticated(string email);
}