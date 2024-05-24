using Domain;
using IDataAccess;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

namespace DataAccess.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly DbContext _dbContext;

    public SessionRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void CreateSession(Session session)
    {
        try
        {
            _dbContext.Set<Session>().Add(session);
            _dbContext.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public void DeleteSession(Session session)
    {
        try
        {
            _dbContext.Set<Session>().Remove(session);
            _dbContext.Entry(session).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public Session GetSessionById(Guid sessionId)
    {
        try
        {
            return _dbContext.Set<Session>().Find(sessionId);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public IEnumerable<Session> GetAllSessions()
    {
        try
        {
            return _dbContext.Set<Session>().Include(session => session.User).ToList();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public void Save()
    {
        try
        {
            _dbContext.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }
}