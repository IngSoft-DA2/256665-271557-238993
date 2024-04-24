using Adapter;
using Adapter.CustomExceptions;
using Domain;
using Domain.Enums;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.MaintenanceResponses;

namespace Test.Adapters;

[TestClass]
public class MaintenanceRequestAdapterTest
{
    private Mock<IMaintenanceRequestService> _maintenanceRequestService;
    private MaintenanceRequestAdapter _maintenanceRequestAdapter;

    private MaintenanceRequest genericMaintenanceRequest;
    private GetMaintenanceRequestResponse genericMaintenanceRequestResponse;

    [TestInitialize]
    public void Initialize()
    {
        _maintenanceRequestService = new Mock<IMaintenanceRequestService>();
        _maintenanceRequestAdapter = new MaintenanceRequestAdapter(_maintenanceRequestService.Object);

        genericMaintenanceRequest = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            BuildingId = Guid.NewGuid(),
            Description = "Test Description Random",
            FlatId = Guid.NewGuid(),
            Category = Guid.NewGuid(),
            RequestStatus = StatusEnum.Accepted,
            OpenedDate = DateTime.Now,
            ClosedDate = DateTime.Now,
            RequestHandlerId = Guid.NewGuid()
        };
        
        genericMaintenanceRequestResponse = new GetMaintenanceRequestResponse
        {
            Id = genericMaintenanceRequest.Id,
            BuildingId = genericMaintenanceRequest.BuildingId,
            Description = genericMaintenanceRequest.Description,
            FlatId = genericMaintenanceRequest.FlatId,
            Category = genericMaintenanceRequest.Category,
            RequestStatus = (StatusEnumMaintenanceResponse)genericMaintenanceRequest.RequestStatus,
            OpenedDate = genericMaintenanceRequest.OpenedDate,
            ClosedDate = genericMaintenanceRequest.ClosedDate,
            RequestHandlerId = genericMaintenanceRequest.RequestHandlerId
        };
    }

    [TestMethod]
    public void GetAllMaintenanceRequests_ReturnsMaintenanceRequestResponses()
    {
        IEnumerable<MaintenanceRequest> expectedServiceResponse = new List<MaintenanceRequest>
            { genericMaintenanceRequest };

        IEnumerable<GetMaintenanceRequestResponse> expectedAdapterResponse =
            new List<GetMaintenanceRequestResponse> {genericMaintenanceRequestResponse};

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
        _maintenanceRequestService.Setup(service => service.GetMaintenanceRequestById(It.IsAny<Guid>()))
            .Returns(genericMaintenanceRequest);

        GetMaintenanceRequestResponse adapterResponse =
            _maintenanceRequestAdapter.GetMaintenanceRequestById(genericMaintenanceRequest.Id);
        _maintenanceRequestService.VerifyAll();

        Assert.AreEqual(genericMaintenanceRequestResponse, adapterResponse);
    }
    
    [TestMethod]
    public void GetMaintenanceRequestById_ShouldThrowObjectNotFoundAdapterException()
    {
        _maintenanceRequestService.Setup(service => service.GetMaintenanceRequestById(It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundServiceException());

        Assert.ThrowsException<ObjectNotFoundAdapterException>(() =>
            _maintenanceRequestAdapter.GetMaintenanceRequestById(genericMaintenanceRequest.Id));
    }
    
    [TestMethod]
    public void GetMaintenanceRequestById_ThrowsException()
    {
        _maintenanceRequestService.Setup(service => service.GetMaintenanceRequestById(It.IsAny<Guid>()))
            .Throws(new Exception("Something went wrong"));

        Exception exceptionCaught = Assert.ThrowsException<Exception>(() =>
            _maintenanceRequestAdapter.GetMaintenanceRequestById(genericMaintenanceRequest.Id));

        Assert.AreEqual("Something went wrong", exceptionCaught.Message);
    }
}