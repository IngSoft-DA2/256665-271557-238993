using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;

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
        _dbContext.Set<RequestHandler>().Add(requestHandlerToAdd); 
    }

    public IEnumerable<RequestHandler> GetAllRequestHandlers()
    {
        throw new NotImplementedException();
    }
}