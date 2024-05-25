using Domain;

namespace IDataAccess;


public interface ISessionRepository
{
    public void CreateSession(Session session);
    public void DeleteSession(Session session);
    public Session? GetSessionBySessionString(Guid sessionString);
}