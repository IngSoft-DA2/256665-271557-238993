using Domain;
using IRepository;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class RequestHandlerServiceTest
{
    #region Test Initialize

    private Mock<IRequestHandlerRepository> _requestHandlerRepository;
    private RequestHandlerService _requestHandlerService;
    private RequestHandler _requestHandlerSample;

    [TestInitialize]
    public void Initialize()
    {
        _requestHandlerRepository = new Mock<IRequestHandlerRepository>();
        _requestHandlerService = new RequestHandlerService(_requestHandlerRepository.Object);
        _requestHandlerSample = new RequestHandler
        {
            Email = "some@gmail.com",
            Password = "admin12345",
            Firstname = "John",
            LastName = "Gates"
        };
    }

    #endregion

    #region Create Request Handler

    [TestMethod]
    public void CreateRequestHandler_ShouldCreateRequestHandler()
    {
        _requestHandlerRepository.Setup(requestHandlerRepository =>
            requestHandlerRepository.CreateRequestHandler(_requestHandlerSample));

        _requestHandlerService.CreateRequestHandler(_requestHandlerSample);
        _requestHandlerRepository.VerifyAll();
    }

    [TestMethod]
    public void CreateRequestHandlerWithIncorrectPassword_ThrowsObjectErrorServiceException()
    {
        _requestHandlerSample.Password = "123";

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _requestHandlerService.CreateRequestHandler(_requestHandlerSample));
    }

    [TestMethod]
    public void CreateRequestHandlerWithIncorrectValues_ThrowsObjectErrorServiceException()
    {
        _requestHandlerSample.Password = "";

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _requestHandlerService.CreateRequestHandler(_requestHandlerSample));
    }

    [TestMethod]
    public void CreateRequestHandler_ThrowRepeatedObjectServiceException()
    {
        _requestHandlerRepository.Setup(x => x.CreateRequestHandler(_requestHandlerSample))
            .Throws(new ObjectRepeatedServiceException());

        Assert.ThrowsException<ObjectRepeatedServiceException>(() =>
            _requestHandlerService.CreateRequestHandler(_requestHandlerSample));

        _requestHandlerRepository.VerifyAll();
    }

    [TestMethod]
    public void CreateRequestHandler_ObjectErrorServiceException()
    {
        _requestHandlerSample.Email = "";

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _requestHandlerService.CreateRequestHandler(_requestHandlerSample));
    }

    [TestMethod]
    public void CreateRequestHandlerWithUsedEmail_ThrowsObjectRepeatedException()
    {
        _requestHandlerRepository.Setup(requestHandlerRepository => requestHandlerRepository.GetAllRequestHandlers())
            .Returns(new List<RequestHandler> { _requestHandlerSample });

        RequestHandler requestHandlerToCreate = new RequestHandler
        {
            Id = Guid.NewGuid(),
            Firstname = "John",
            LastName = "Doe",
            Email = _requestHandlerSample.Email,
            Password = "admin12345"
        };

        Assert.ThrowsException<ObjectRepeatedServiceException>(() =>
            _requestHandlerService.CreateRequestHandler(requestHandlerToCreate));
    }
    
    [TestMethod]
    public void CreateRequestHanlderWithEmptyLastName_ThrowsObjectErrorServiceException()
    {
        _requestHandlerSample.LastName = "";

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _requestHandlerService.CreateRequestHandler(_requestHandlerSample));
    }

    [TestMethod]
    public void CreateRequestHandler_UnknownServiceException()
    {
        _requestHandlerRepository.Setup(x => x.CreateRequestHandler(_requestHandlerSample)).Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _requestHandlerService.CreateRequestHandler(_requestHandlerSample));

        _requestHandlerRepository.VerifyAll();
    }

    #endregion
}