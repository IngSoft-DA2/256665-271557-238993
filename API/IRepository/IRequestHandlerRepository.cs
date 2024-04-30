using Domain;

namespace IRepository;

public interface IRequestHandlerRepository
{
    public void CreateRequestHandler(RequestHandler requestHandler);
    IEnumerable<RequestHandler> GetAllRequestHandlers();
}