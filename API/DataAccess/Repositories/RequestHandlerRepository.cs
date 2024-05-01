using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

namespace DataAccess.Repositories;

public class RequestHandlerRepository : IRequestHandlerRepository
{
    private readonly DbContext _dbContext;

    public RequestHandlerRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateRequestHandler(RequestHandler requestHandlerToAdd)
    {
        try
        {
            _dbContext.Set<RequestHandler>().Add(requestHandlerToAdd);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public IEnumerable<RequestHandler> GetAllRequestHandlers()
    {
        try
        {
            return _dbContext.Set<RequestHandler>().ToList();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }
}