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
            Id = Guid.NewGuid(),
            Email = "some@example.com",
            Password = "SuperSecretPassword",
            Firstname = "John",
            LastName = "Doe"
        };
    }
    
    #endregion
    
    #region Create Request Handler
    
    [TestMethod]
    public void CreateRequestHandler_ShouldCreateRequestHandler()
    {
        
        _requestHandlerRepository.Setup(x => x.CreateRequestHandler(_requestHandlerSample));
        
        _requestHandlerService.CreateRequestHandler(_requestHandlerSample);
        
        _requestHandlerRepository.VerifyAll();
    }
    
    [TestMethod]
    public void CreateRequestHandler_ThrowRepeatedObjectServiceException()
    {
        _requestHandlerRepository.Setup(x => x.CreateRequestHandler(_requestHandlerSample)).Throws(new ObjectRepeatedServiceException());
        
        Assert.ThrowsException<ObjectRepeatedServiceException>(() => _requestHandlerService.CreateRequestHandler(_requestHandlerSample));
        
        _requestHandlerRepository.VerifyAll();
    }

    [TestMethod]
    public void CreateRequestHandler_ThrowsInvalidPersonException()
    {
        _requestHandlerSample.Password = "";
        
        Assert.ThrowsException<ObjectErrorServiceException>(() => _requestHandlerService.CreateRequestHandler(_requestHandlerSample));
        
    }
    
    
    #endregion
    
}