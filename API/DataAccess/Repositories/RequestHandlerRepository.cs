using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

namespace DataAccess.Repositories;

public class RequestHandlerRepository : IRequestHandlerRepository
{
    #region Constructor and attributes
    
    private readonly DbContext _dbContext;

    public RequestHandlerRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    #endregion
    
    #region Create RequestHandler

    public void CreateRequestHandler(RequestHandler requestHandlerToAdd)
    {
        try
        {
            _dbContext.Set<RequestHandler>().Add(requestHandlerToAdd);
            _dbContext.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }
    
    #endregion
    
    #region Get All RequestHandlers

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
    
    #endregion
}