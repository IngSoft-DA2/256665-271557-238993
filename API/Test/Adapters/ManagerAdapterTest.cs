using Adapter;
using Domain;
using IServiceLogic;
using Moq;
using WebModel.Responses.ManagerResponses;

namespace Test.Adapters;

[TestClass]

public class ManagerAdapterTest
{
    [TestMethod]
    public void GetAllManagers_ShouldConvertFromDomainToResponse()
    {
        IEnumerable<Manager> domainResponse = new List<Manager>()
        {
            new Manager
            {
                Id = Guid.NewGuid(),
                Name = "Michael Kent",
                Email = "",
                Password = ""
            }
        };

        IEnumerable<GetManagerResponse> expectedAdapterResponse = new List<GetManagerResponse>()
        {
            new GetManagerResponse
            {
                Id = domainResponse.First().Id,
                Name = domainResponse.First().Name,
                Email = domainResponse.First().Email
            }
        };
        
        Mock<IManagerService> managerService = new Mock<IManagerService>(MockBehavior.Strict);
        managerService.Setup(service => service.GetAllManagers()).Returns(domainResponse);
        
        ManagerAdapter managerAdapter = new ManagerAdapter(managerService.Object);
        
        IEnumerable<GetManagerResponse> adapterResponse = managerAdapter.GetAllManagers();
        
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }
}