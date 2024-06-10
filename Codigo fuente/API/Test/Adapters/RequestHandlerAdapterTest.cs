using System.Collections;
using Adapter;
using Adapter.CustomExceptions;
using Domain;
using Domain.Enums;
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
        CreateRequestHandlerRequest dummyRequest = new CreateRequestHandlerRequest
        {
            Firstname = "Firstname",
            Lastname = "Lastname",
            Email = "requestHandler@gmail.com",
            Password = "Password"
        };
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

    [TestMethod]
    public void CreateRequestHandler_ThrowsException()
    {
        CreateRequestHandlerRequest dummyRequest = new CreateRequestHandlerRequest();
        _requestHandlerService.Setup(service => service.CreateRequestHandler(It.IsAny<RequestHandler>()))
            .Throws(new Exception("Internal Server Error"));

        Assert.ThrowsException<Exception>(() => _requestHandlerAdapter.CreateRequestHandler(dummyRequest));
        _requestHandlerService.VerifyAll();
    }

    #endregion

    #region Get All Request Handlers

    [TestMethod]
    public void GetAllRequestHandlers_ReturnsGetRequestHandlerResponseList()
    {
        List<RequestHandler> dummyRequestHandlers = new List<RequestHandler>
        {
            new RequestHandler
            {
                Id = Guid.NewGuid(),
                Firstname = "Firstname",
                LastName = "Lastname",
                Email = "someone@gmail.com",
                Password = "Password"
            }
        };
        
        List<GetRequestHandlerResponse> expectedAdapterResponse = new List<GetRequestHandlerResponse>
        {
            new GetRequestHandlerResponse
            {
                Id = dummyRequestHandlers[0].Id,
                Name = "Firstname",
                LastName = "Lastname",
                Email = "someone@gmail.com"
            }
        };
        
        _requestHandlerService.Setup(service => service.GetAllRequestHandlers())
            .Returns(dummyRequestHandlers);

        IEnumerable<GetRequestHandlerResponse> adapterResponse = _requestHandlerAdapter.GetAllRequestHandlers();
        
        _requestHandlerService.VerifyAll();

        Assert.IsTrue(adapterResponse.SequenceEqual(expectedAdapterResponse));
    }
    
    [TestMethod]
    public void GetAllRequestHandlers_ThrowsException()
    {
        _requestHandlerService.Setup(service => service.GetAllRequestHandlers())
            .Throws(new Exception("Internal Server Error"));

        Assert.ThrowsException<Exception>(() => _requestHandlerAdapter.GetAllRequestHandlers());
        _requestHandlerService.VerifyAll();
    }

    #endregion
}