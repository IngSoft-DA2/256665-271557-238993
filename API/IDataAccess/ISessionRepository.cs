using Domain;

namespace IRepository;


public interface ISessionRepository
{
    public void CreateSession(Session session);
    public void DeleteSession(Session session);
    public Session GetSessionById(Guid token);
    public IEnumerable<Session> GetAllSessions();
    void Save();
}