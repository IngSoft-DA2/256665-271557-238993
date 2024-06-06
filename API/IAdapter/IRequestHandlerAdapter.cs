using WebModel.Requests;
using WebModel.Requests.RequestHandlerRequests;
using WebModel.Responses.RequestHandlerResponses;

namespace IAdapter;

public interface IRequestHandlerAdapter
{
    public CreateRequestHandlerResponse CreateRequestHandler(CreateRequestHandlerRequest request);
    public IEnumerable<GetRequestHandlerResponse> GetAllRequestHandlers();
}