using Adapter;
using Domain;
using IServiceLogic;
using Moq;
using WebModel.Requests.RequestHandlerRequests;
using WebModel.Responses.RequestHandlerResponses;

namespace Test.Adapters;

[TestClass]
public class RequestHandlerAdapterTest
{
    #region Initialize

    private Mock<IRequestHandlerService> _requestHandlerService;
    private RequestHandlerAdapter _requestHandlerAdapter;
    
    [TestInitialize]
    public void Initialize()
    {
        _requestHandlerService = new Mock<IRequestHandlerService>(MockBehavior.Strict);
        _requestHandlerAdapter = new RequestHandlerAdapter(_requestHandlerService.Object);
    }

    #endregion

    #region Create Request Handler

    [TestMethod]
    public void CreateRequestHandler_ReturnsCreateRequestHandlerResponse()
    {
        CreateRequestHandlerRequest dummyRequest = new CreateRequestHandlerRequest();
        _requestHandlerService.Setup(service => service.CreateRequestHandler(It.IsAny<RequestHandler>()));
        
        CreateRequestHandlerResponse adapterResponse = _requestHandlerAdapter.CreateRequestHandler(dummyRequest);
        _requestHandlerService.VerifyAll();
        Assert.IsNotNull(adapterResponse.Id);
    }

    #endregion
}