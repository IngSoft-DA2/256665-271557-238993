using Domain;
using IRepository;
using Moq;
using ServiceLogic;

namespace Test.Services;

[TestClass]
public class RequestHandlerServiceTest
{
    #region Test Initialize
    
    private Mock<IRequestHandlerRepository> _requestHandlerRepository;
    private RequestHandlerService _requestHandlerService;
    
    [TestInitialize]
    public void Initialize()
    {
        _requestHandlerRepository = new Mock<IRequestHandlerRepository>();
        _requestHandlerService = new RequestHandlerService(_requestHandlerRepository.Object);
    }
    
    #endregion
    
    #region Create Request Handler
    
    [TestMethod]
    public void CreateRequestHandler_ShouldCreateRequestHandler()
    {
        RequestHandler requestHandler = new RequestHandler
        {   
            Id = Guid.NewGuid(),
            FirstName = "John",
            Lastname = "Doe",
            Email = "john@doe.com",
            Password = "someSecretPass"
        };
        
        _requestHandlerRepository.Setup(x => x.CreateRequestHandler(requestHandler));
        
        _requestHandlerService.CreateRequestHandler(requestHandler);
        
        _requestHandlerRepository.VerifyAll();
    }
    
    #endregion
    
}