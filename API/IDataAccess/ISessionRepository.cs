using Domain;

namespace IRepository;


public interface ISessionRepository
{
    public void CreateSession(Session session);
    public void DeleteSession(Session session);
    public Session GetSessionByToken(Guid token);
    public IEnumerable<Session> GetAllSessions();
    public void UpdateSession(Session session);
    void Save();
    void Insert(Session session);
}