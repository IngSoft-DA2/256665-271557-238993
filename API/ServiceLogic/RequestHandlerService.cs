using Domain;
using Domain.CustomExceptions;
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
        try
        {
            requestHandler.RequestValidator();
            CheckIfEmailIsAlreadyRegistered(requestHandler);

            _requestHandlerRepository.CreateRequestHandler(requestHandler);
        }
        catch (InvalidPersonException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (InvalidSystemUserException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (InvalidRequestHandlerException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (ObjectRepeatedServiceException)
        {
            throw new ObjectRepeatedServiceException();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    public IEnumerable<RequestHandler> GetAllRequestHandlers()
    {
        return _requestHandlerRepository.GetAllRequestHandlers();
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