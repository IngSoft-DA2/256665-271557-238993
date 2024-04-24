using Domain;
using WebModel.Requests.RequestHandlerRequests;

namespace IServiceLogic;

public interface IRequestHandlerService
{
    public void CreateRequestHandler(RequestHandler requestHandler);
}