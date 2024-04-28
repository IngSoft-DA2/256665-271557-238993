using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.AdministratorRequests;
using WebModel.Responses.AdministratorResponses;

namespace Test.Adapters;

[TestClass]
public class AdministratorAdapterTest
{
    #region Initialize
    
    private Mock<IAdministratorService> _administratorService;
    private AdministratorAdapter _administratorAdapter;
    private CreateAdministratorRequest _dummyCreateAdministratorRequest;
    
    [TestInitialize]
    public void TestInitialize()
    {
        _administratorService = new Mock<IAdministratorService>(MockBehavior.Strict);
        _administratorAdapter = new AdministratorAdapter(_administratorService.Object);
        _dummyCreateAdministratorRequest = new CreateAdministratorRequest(){
            Firstname = "Gustavo",
            Lastname = "Dealva",
            Email = "gustavo@gmail.com",
            Password = "123123123dd"
        };
    }
    
    #endregion
    
    #region Create Administrator

    [TestMethod]
    public void CreateAdministrator_ShouldCreateAdministrator()
    {
        _administratorService.Setup(service => service.CreateAdministrator(It.IsAny<Administrator>()));
        AdministratorAdapter administratorAdapter = new AdministratorAdapter(_administratorService.Object);
        
        CreateAdministratorResponse response = administratorAdapter.CreateAdministrator(_dummyCreateAdministratorRequest);
        
        Assert.IsNotNull(response.Id);
        
        _administratorService.Verify(service => service.CreateAdministrator(It.IsAny<Administrator>()), Times.Once);
        
    }
    
    [TestMethod]
    public void CreateAdministrator_ShouldThrowObjectRepeatedAdapterException()
    {
        Guid administratorId = Guid.NewGuid();
        
        _administratorService.Setup(service => service.CreateAdministrator(It.IsAny<Administrator>())).Throws(new ObjectRepeatedServiceException());
        AdministratorAdapter administratorAdapter = new AdministratorAdapter(_administratorService.Object);
        
        Assert.ThrowsException<ObjectRepeatedAdapterException>(() => administratorAdapter.CreateAdministrator(_dummyCreateAdministratorRequest));
        
        _administratorService.VerifyAll();
    }
    
    [TestMethod]
    public void CreateAdministrator_ShouldThrowObjectErrorAdapterException()
    {
        Guid administratorId = Guid.NewGuid();
        
        _administratorService.Setup(service => service.CreateAdministrator(It.IsAny<Administrator>())).Throws(new ObjectErrorServiceException("Error creating administrator"));
        AdministratorAdapter administratorAdapter = new AdministratorAdapter(_administratorService.Object);
        
        Assert.ThrowsException<ObjectErrorAdapterException>(() => administratorAdapter.CreateAdministrator(_dummyCreateAdministratorRequest));
    }
    
    [TestMethod]
    public void CreateAdministrator_ShouldThrowException()
    {
        Guid administratorId = Guid.NewGuid();
        
        _administratorService.Setup(service => service.CreateAdministrator(It.IsAny<Administrator>())).Throws(new Exception("Error creating administrator"));
        AdministratorAdapter administratorAdapter = new AdministratorAdapter(_administratorService.Object);
        
        Assert.ThrowsException<Exception>(() => administratorAdapter.CreateAdministrator(_dummyCreateAdministratorRequest));
        
        _administratorService.VerifyAll();
    }
    
    #endregion
}