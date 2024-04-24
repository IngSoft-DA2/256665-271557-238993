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
    
    private Mock<IMaintenanceRequestService> _maintenanceRequestService;
    private MaintenanceRequestAdapter _maintenanceRequestAdapter;
    
    [TestInitialize]
    public void Initialize()
    {
        _maintenanceRequestService = new Mock<IMaintenanceRequestService>();
        _maintenanceRequestAdapter = new MaintenanceRequestAdapter(_maintenanceRequestService.Object);
    }
    
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
        
        _maintenanceRequestService.Setup(service => service.GetAllMaintenanceRequests())
            .Returns(expectedServiceResponse);
        
        IEnumerable<GetMaintenanceRequestResponse> adapterResponse = 
            _maintenanceRequestAdapter.GetAllMaintenanceRequests();
        _maintenanceRequestService.VerifyAll();
        
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }
    
    [TestMethod]
    public void GetAllMaintenanceRequests_ThrowsException_ReturnsExceptionMessage()
    {
        _maintenanceRequestService.Setup(service => service.GetAllMaintenanceRequests())
            .Throws(new Exception("Something went wrong"));
        
        Exception exceptionCaught = Assert.ThrowsException<Exception>(() =>
            _maintenanceRequestAdapter.GetAllMaintenanceRequests());
        
        Assert.AreEqual("Something went wrong", exceptionCaught.Message);
    }
    
    [TestMethod]
    public void GetMaintenanceRequestById_ReturnsMaintenanceRequestResponse()
    {
        MaintenanceRequest expectedServiceResponse = new MaintenanceRequest
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
        };

        GetMaintenanceRequestResponse expectedAdapterResponse = new GetMaintenanceRequestResponse
        {
            Id = expectedServiceResponse.Id,
            BuildingId = expectedServiceResponse.BuildingId,
            Description = expectedServiceResponse.Description,
            FlatId = expectedServiceResponse.FlatId,
            Category = expectedServiceResponse.Category,
            RequestStatus = (StatusEnumMaintenanceResponse)expectedServiceResponse.RequestStatus,
            OpenedDate = expectedServiceResponse.OpenedDate,
            ClosedDate = expectedServiceResponse.ClosedDate,
            RequestHandlerId = expectedServiceResponse.RequestHandlerId
        };
        
        _maintenanceRequestService.Setup(service => service.GetMaintenanceRequestById(It.IsAny<Guid>()))
            .Returns(expectedServiceResponse);
        
        GetMaintenanceRequestResponse adapterResponse = 
            _maintenanceRequestAdapter.GetMaintenanceRequestById(expectedServiceResponse.Id);
        _maintenanceRequestService.VerifyAll();
        
        Assert.AreEqual(expectedAdapterResponse, adapterResponse);
    }
    
}