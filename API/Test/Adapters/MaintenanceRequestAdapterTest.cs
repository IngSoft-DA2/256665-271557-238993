using Adapter;
using Domain;
using Domain.Enums;
using IServiceLogic;
using Moq;
using WebModel.Responses.MaintenanceResponses;

namespace Test.Adapters;

[TestClass]

public class MaintenanceRequestAdapterTest
{
    [TestMethod]
    public void GetAllMaintenanceRequests_ReturnsMaintenanceRequestResponses()
    {
        IEnumerable<MaintenanceRequest> expectedServiceResponse = new List<MaintenanceRequest>
        {
            new MaintenanceRequest
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                Description = "Test Description",
                FlatId = Guid.NewGuid(),
                Category = Guid.NewGuid(),
                RequestStatus = StatusEnum.Accepted,
                OpenedDate = DateTime.Now,
                ClosedDate = DateTime.Now,
                RequestHandlerId = Guid.NewGuid()
            }
        };

        IEnumerable<GetMaintenanceRequestResponse> expectedAdapterResponse =
            new List<GetMaintenanceRequestResponse>
            {
                new GetMaintenanceRequestResponse
                {
                    Id = expectedServiceResponse.First().Id,
                    BuildingId = expectedServiceResponse.First().BuildingId,
                    Description = expectedServiceResponse.First().Description,
                    FlatId = expectedServiceResponse.First().FlatId,
                    Category = expectedServiceResponse.First().Category,
                    RequestStatus = (StatusEnumMaintenanceResponse)expectedServiceResponse.First().RequestStatus,
                    OpenedDate = expectedServiceResponse.First().OpenedDate,
                    ClosedDate = expectedServiceResponse.First().ClosedDate,
                    RequestHandlerId = expectedServiceResponse.First().RequestHandlerId
                }
            };

        Mock<IMaintenanceRequestService> maintenanceRequestService =
            new Mock<IMaintenanceRequestService>(MockBehavior.Strict);
        maintenanceRequestService.Setup(service => service.GetAllMaintenanceRequests())
            .Returns(expectedServiceResponse);

        MaintenanceRequestAdapter maintenanceRequestAdapter =
            new MaintenanceRequestAdapter(maintenanceRequestService.Object);
        
        IEnumerable<GetMaintenanceRequestResponse> adapterResponse = 
            maintenanceRequestAdapter.GetAllMaintenanceRequests();
        maintenanceRequestService.VerifyAll();
        
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }
}