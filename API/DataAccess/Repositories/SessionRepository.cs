using Domain;
using IDataAccess;
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

    public Session GetSessionBySessionString(Guid sessionString)
    {
        try
        {
            Session? sessionFound = _dbContext.Set<Session>()
                .FirstOrDefault(session => session.SessionString.Equals(sessionString));
            
            return sessionFound;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }
}