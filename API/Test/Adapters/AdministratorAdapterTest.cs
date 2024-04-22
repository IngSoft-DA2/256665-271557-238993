using Adapter;
using IServiceLogic;
using Moq;
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
}