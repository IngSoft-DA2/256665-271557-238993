using Domain;

namespace IRepository;

public interface IRequestHandlerRepository
{
    public void CreateRequestHandler(RequestHandler requestHandlerSample);
    public IEnumerable<RequestHandler> GetAllRequestHandlers();
}