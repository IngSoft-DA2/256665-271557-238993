using Adapter;
using Adapter.CustomExceptions;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.AdministratorResponses;

namespace Test.Adapters;

[TestClass]
public class AdministratorAdapterTest
{
    [TestMethod]
    public void CreateAdministrator_ShouldCreateAdminsitrator()
    {
        Guid administratorId = Guid.NewGuid();
        
        Mock<IAdministratorService> administratorService = new Mock<IAdministratorService>(MockBehavior.Strict);
        administratorService.Setup(service => service.CreateAdministrator(administratorId));
        AdministratorAdapter administratorAdapter = new AdministratorAdapter(administratorService.Object);
        
        CreateAdministratorResponse response = administratorAdapter.CreateAdministrator(administratorId);
        
        Assert.AreEqual(administratorId, response.Id);
    }
    
    [TestMethod]
    public void CreateAdministrator_ShouldThrowObjectRepeatedAdapterException()
    {
        Guid administratorId = Guid.NewGuid();
        
        Mock<IAdministratorService> administratorService = new Mock<IAdministratorService>(MockBehavior.Strict);
        administratorService.Setup(service => service.CreateAdministrator(administratorId)).Throws(new ObjectRepeatedServiceException("Administrator already exists"));
        AdministratorAdapter administratorAdapter = new AdministratorAdapter(administratorService.Object);
        
        Assert.ThrowsException<ObjectRepeatedAdapterException>(() => administratorAdapter.CreateAdministrator(administratorId));
    }
    
    
}