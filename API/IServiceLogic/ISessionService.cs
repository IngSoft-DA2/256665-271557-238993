namespace IServiceLogic;

public interface ISessionService
{
    bool IsValidToken(Guid headerValidationString);
    object? GetUserBySessionString(Guid headerValidationString);
}