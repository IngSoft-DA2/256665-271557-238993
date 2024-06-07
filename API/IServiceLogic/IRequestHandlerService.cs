using System.Collections;
using Domain;
using WebModel.Requests.RequestHandlerRequests;

namespace IServiceLogic;

public interface IRequestHandlerService
{
    public void CreateRequestHandler(RequestHandler requestHandler);
    public IEnumerable<RequestHandler> GetAllRequestHandlers();
}