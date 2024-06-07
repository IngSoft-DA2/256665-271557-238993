using Adapter.CustomExceptions;
using Domain;
using Domain.Enums;
using IAdapter;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.RequestHandlerRequests;
using WebModel.Responses.RequestHandlerResponses;

namespace Adapter;

public class RequestHandlerAdapter : IRequestHandlerAdapter
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
                Id = Guid.NewGuid(),
                Firstname = createRequest.Firstname,
                LastName = createRequest.Lastname,
                Email = createRequest.Email,
                Password = createRequest.Password,
                Role = SystemUserRoleEnum.RequestHandler
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

    public IEnumerable<GetRequestHandlerResponse> GetAllRequestHandlers()
    {
        try
        {
            IEnumerable<RequestHandler> requestHandlers = _requestHandlerService.GetAllRequestHandlers();
            List<GetRequestHandlerResponse> getRequestHandlerResponses = new List<GetRequestHandlerResponse>();
            foreach (RequestHandler requestHandler in requestHandlers)
            {
                GetRequestHandlerResponse getRequestHandlerResponse = new GetRequestHandlerResponse
                {
                    Id = requestHandler.Id,
                    Name = requestHandler.Firstname,
                    LastName = requestHandler.LastName,
                    Email = requestHandler.Email
                };
                getRequestHandlerResponses.Add(getRequestHandlerResponse);
            }

            return getRequestHandlerResponses;
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }
}