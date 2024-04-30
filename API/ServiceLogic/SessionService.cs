using IRepository;
using IServiceLogic;

namespace ServiceLogic;

public class SessionService : ISessionService
{
    #region Constructor and Dependency Injection
    
    private readonly ISessionRepository _sessionRepository;
    
    public SessionService(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    #endregion
    public bool IsValidToken(Guid headerValidationString)
    {
        return _sessionRepository.SessionExists(headerValidationString);
        
    }

    public object? GetUserBySessionString(Guid headerValidationString)
    {
        throw new NotImplementedException();
    }
}