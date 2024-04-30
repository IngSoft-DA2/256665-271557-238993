using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.RequestHandlerRequests;
using WebModel.Responses.RequestHandlerResponses;

namespace Adapter;

public class RequestHandlerAdapter
{
    private readonly IRequestHandlerService _requestHandlerService;
    public RequestHandlerAdapter(IRequestHandlerService requestHandlerService)
    {
        _requestHandlerService = requestHandlerService;
    }


    public CreateRequestHandlerResponse CreateRequestHandler(CreateRequestHandlerRequest createRequest)
    {
        try
        {
            RequestHandler requestHandlerToCreate = new RequestHandler
            {
                Firstname = createRequest.Firstname,
                LastName = createRequest.Lastname,
                Email = createRequest.Email,
                Password = createRequest.Password
            };
            _requestHandlerService.CreateRequestHandler(requestHandlerToCreate);
            
            CreateRequestHandlerResponse createRequestHandlerResponse = new CreateRequestHandlerResponse
            {
                Id = requestHandlerToCreate.Id
            };
            return createRequestHandlerResponse;
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }
}