using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
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

    [TestMethod]
    public void CreateRequestHandler_ThrowsObjectErrorAdapterException_WhenServiceFails()
    {
        CreateRequestHandlerRequest dummyRequest = new CreateRequestHandlerRequest();
        _requestHandlerService.Setup(service => service.CreateRequestHandler(It.IsAny<RequestHandler>()))
            .Throws(new ObjectErrorServiceException("Service Error"));
        
        Assert.ThrowsException<ObjectErrorAdapterException>(() => 
            _requestHandlerAdapter.CreateRequestHandler(dummyRequest));
        _requestHandlerService.VerifyAll();
    }

    #endregion
}