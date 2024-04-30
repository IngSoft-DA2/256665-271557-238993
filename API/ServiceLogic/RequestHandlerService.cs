using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class RequestHandlerService : IRequestHandlerService
{
    #region Constructor and dependency injection
    
    private readonly IRequestHandlerRepository _requestHandlerRepository;
    
    public RequestHandlerService(IRequestHandlerRepository requestHandlerRepository)
    {
        _requestHandlerRepository = requestHandlerRepository;
    }

    #endregion
    
    #region Create Request Handler
    public void CreateRequestHandler(RequestHandler requestHandler)
    {
            CheckIfEmailIsAlreadyRegistered(requestHandler);
            
            _requestHandlerRepository.CreateRequestHandler(requestHandler);
        
        
    }

    private void CheckIfEmailIsAlreadyRegistered(RequestHandler requestHandler)
    {
        IEnumerable<RequestHandler> requestHandlers = _requestHandlerRepository.GetAllRequestHandlers();
        if (requestHandlers.Any(x => x.Email == requestHandler.Email))
        {
            throw new ObjectRepeatedServiceException();
        }
    }
    
    #endregion
}