using Adapter;
using Adapter.CustomExceptions;
using Domain;
using Domain.Enums;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.MaintenanceRequests;
using WebModel.Responses.MaintenanceResponses;

namespace Test.Adapters;

[TestClass]
public class MaintenanceRequestAdapterTest
{
    private Mock<IMaintenanceRequestService> _maintenanceRequestService;
    private MaintenanceRequestAdapter _maintenanceRequestAdapter;

    private MaintenanceRequest genericMaintenanceRequest;
    private GetMaintenanceRequestResponse genericMaintenanceRequestResponse;
    private CreateRequestMaintenanceRequest _dummyCreateRequestMaintenanceResponse;

    [TestInitialize]
    public void Initialize()
    {
        _maintenanceRequestService = new Mock<IMaintenanceRequestService>(MockBehavior.Strict);
        _maintenanceRequestAdapter = new MaintenanceRequestAdapter(_maintenanceRequestService.Object);

        _dummyCreateRequestMaintenanceResponse = new CreateRequestMaintenanceRequest();

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
        
        _maintenanceRequestService.Verify(service => service.GetAllMaintenanceRequests(), Times.Once);
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
        
        _maintenanceRequestService.Verify(service => service.GetMaintenanceRequestById(It.IsAny<Guid>()), Times.Once);
    }
    
    [TestMethod]
    public void GetMaintenanceRequestById_ThrowsException()
    {
        _maintenanceRequestService.Setup(service => service.GetMaintenanceRequestById(It.IsAny<Guid>()))
            .Throws(new Exception("Something went wrong"));

        Exception exceptionCaught = Assert.ThrowsException<Exception>(() =>
            _maintenanceRequestAdapter.GetMaintenanceRequestById(genericMaintenanceRequest.Id));

        Assert.AreEqual("Something went wrong", exceptionCaught.Message);
        
        _maintenanceRequestService.Verify(service => service.GetMaintenanceRequestById(It.IsAny<Guid>()), Times.Once);
    }
    
    [TestMethod]
    public void CreateMaintenanceRequest_ShouldReturnCreateMaintenanceRequestResponse()
    {
        CreateRequestMaintenanceRequest createRequest = _dummyCreateRequestMaintenanceResponse;
        
        _maintenanceRequestService.Setup(service => service.CreateMaintenanceRequest(It.IsAny<MaintenanceRequest>()));
        
        CreateRequestMaintenanceResponse adapterResponse = _maintenanceRequestAdapter.CreateMaintenanceRequest(createRequest);
        
        Assert.IsNotNull(adapterResponse);
        
        _maintenanceRequestService.Verify(service => service.CreateMaintenanceRequest(It.IsAny<MaintenanceRequest>()), Times.Once);
    }
    
    [TestMethod]
    public void CreateMaintenanceRequest_ShouldThrowException()
    {
        CreateRequestMaintenanceRequest createRequest = _dummyCreateRequestMaintenanceResponse;
        
        _maintenanceRequestService.Setup(service => service.CreateMaintenanceRequest(It.IsAny<MaintenanceRequest>()))
            .Throws(new Exception("Something went wrong"));
        
        Exception exceptionCaught = Assert.ThrowsException<Exception>(() =>
            _maintenanceRequestAdapter.CreateMaintenanceRequest(createRequest));
        
        Assert.AreEqual("Something went wrong", exceptionCaught.Message);
        
        _maintenanceRequestService.Verify(service => service.CreateMaintenanceRequest(It.IsAny<MaintenanceRequest>()), Times.Once);
    }
    
    [TestMethod]
    public void CreateMaintenanceRequest_ShouldThrowObjectErrorAdapterException()
    {
        CreateRequestMaintenanceRequest createRequest = _dummyCreateRequestMaintenanceResponse;
        
        _maintenanceRequestService.Setup(service => service.CreateMaintenanceRequest(It.IsAny<MaintenanceRequest>()))
            .Throws(new ObjectErrorServiceException("Description can't be empty"));
        
        Assert.ThrowsException<ObjectErrorAdapterException>(() =>
            _maintenanceRequestAdapter.CreateMaintenanceRequest(createRequest));
        
        _maintenanceRequestService.Verify(service => service.CreateMaintenanceRequest(It.IsAny<MaintenanceRequest>()), Times.Once);
    }
    
    [TestMethod]
    public void UpdateMaintenanceRequest_ShouldUpdateMaintenanceRequest()
    {
        UpdateMaintenanceRequestStatusRequest updateRequest = new UpdateMaintenanceRequestStatusRequest
        {
            RequestStatus = (StatusEnumMaintenanceRequest) StatusEnum.Accepted
        };
        
        _maintenanceRequestService.Setup(service => service.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()));
        
        _maintenanceRequestAdapter.UpdateMaintenanceRequest(genericMaintenanceRequest.Id, updateRequest);
        
        _maintenanceRequestService.Verify(service => service.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()), Times.Once);
    }
    
    [TestMethod]
    public void UpdateMaintenanceRequest_ShouldThrowObjectErrorAdapterException()
    {
        UpdateMaintenanceRequestStatusRequest updateRequest = new UpdateMaintenanceRequestStatusRequest
        {
            RequestStatus = (StatusEnumMaintenanceRequest) StatusEnum.Accepted
        };
        
        _maintenanceRequestService.Setup(service => service.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()))
            .Throws(new ObjectErrorServiceException("Request status can't be empty"));
        
        Assert.ThrowsException<ObjectErrorAdapterException>(() =>
            _maintenanceRequestAdapter.UpdateMaintenanceRequest(genericMaintenanceRequest.Id, updateRequest));
        
        _maintenanceRequestService.Verify(service => service.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()), Times.Once);
    }
    
    [TestMethod]
    public void UpdateMaintenanceRequest_ShouldThrowObjectNotFoundAdapterException()
    {
        UpdateMaintenanceRequestStatusRequest updateRequest = new UpdateMaintenanceRequestStatusRequest
        {
            RequestStatus = (StatusEnumMaintenanceRequest) StatusEnum.Accepted
        };
        
        _maintenanceRequestService.Setup(service => service.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()))
            .Throws(new ObjectNotFoundServiceException());
        
        Assert.ThrowsException<ObjectNotFoundAdapterException>(() =>
            _maintenanceRequestAdapter.UpdateMaintenanceRequest(genericMaintenanceRequest.Id, updateRequest));
        
        _maintenanceRequestService.Verify(service => service.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()), Times.Once);
    }
    
    [TestMethod]
    public void UpdateMaintenanceRequest_ShouldThrowException()
    {
        UpdateMaintenanceRequestStatusRequest updateRequest = new UpdateMaintenanceRequestStatusRequest
        {
            RequestStatus = (StatusEnumMaintenanceRequest) StatusEnum.Accepted
        };
        
        _maintenanceRequestService.Setup(service => service.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()))
            .Throws(new Exception("Something went wrong"));
        
        Exception exceptionCaught = Assert.ThrowsException<Exception>(() =>
            _maintenanceRequestAdapter.UpdateMaintenanceRequest(genericMaintenanceRequest.Id, updateRequest));
        
        Assert.AreEqual("Something went wrong", exceptionCaught.Message);
        
        _maintenanceRequestService.Verify(service => service.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()), Times.Once);
    }

    [TestMethod]
    public void AssignMaintenanceRequest_ShouldAssignMaintenanceRequest()
    {
        Guid idOfRequest = genericMaintenanceRequest.Id;
        Guid idOfWorker = Guid.NewGuid();
    
        _maintenanceRequestService
            .Setup(service => service.AssignMaintenanceRequest(idOfRequest, idOfWorker));

        _maintenanceRequestService.Setup(service =>
            service.GetMaintenanceRequestById(It.IsAny<Guid>())).Returns(genericMaintenanceRequest);
        
        _maintenanceRequestAdapter.AssignMaintenanceRequest(idOfRequest, idOfWorker);
        
        _maintenanceRequestService.Verify(service => service.AssignMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
    }
    
    [TestMethod]
    public void AssignMaintenanceRequest_ShouldThrowObjectNotFoundAdapterException()
    {
        Guid idOfRequest = genericMaintenanceRequest.Id;
        Guid idOfWorker = Guid.NewGuid();
    
        _maintenanceRequestService
            .Setup(service => service.AssignMaintenanceRequest(idOfRequest, idOfWorker))
            .Throws(new ObjectNotFoundServiceException());

        _maintenanceRequestService.Setup(service =>
            service.GetMaintenanceRequestById(It.IsAny<Guid>())).Returns(genericMaintenanceRequest);
        
        Assert.ThrowsException<ObjectNotFoundAdapterException>(() =>
            _maintenanceRequestAdapter.AssignMaintenanceRequest(idOfRequest, idOfWorker));
        
        _maintenanceRequestService.Verify(service => service.AssignMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
    }

    
}