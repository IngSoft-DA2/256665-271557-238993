using Domain;
using Domain.Enums;
using IRepository;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class MaintenanceRequestServiceTest
{
    #region Initialize

    private Mock<IMaintenanceRequestRepository> _maintenanceRequestRepository;
    private MaintenanceRequestService _maintenanceRequestService;
    private MaintenanceRequest _maintenanceRequestSample;

    [TestInitialize]
    public void Initialize()
    {
        _maintenanceRequestRepository = new Mock<IMaintenanceRequestRepository>(MockBehavior.Strict);
        _maintenanceRequestService = new MaintenanceRequestService(_maintenanceRequestRepository.Object);
        _maintenanceRequestSample = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            Description = "Fix the door",
            FlatId = Guid.NewGuid(),
            OpenedDate = DateTime.Now,
            RequestHandlerId = Guid.NewGuid(),
            CategoryId = Guid.NewGuid(),
            RequestStatus = RequestStatusEnum.Open
        };
    }

    #endregion

    #region Get Maintenance Request

    [TestMethod]
    public void GetAllMaintenanceRequests_MaintenanceRequestAreReturned()
    {
        IEnumerable<MaintenanceRequest> expectedRepositoryResponse = new List<MaintenanceRequest>
        {
            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                FlatId = Guid.NewGuid(),
                OpenedDate = DateTime.Now,
                RequestHandlerId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                RequestStatus = RequestStatusEnum.Closed
            },

            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                Description = "Fix the window",
                FlatId = Guid.NewGuid(),
                OpenedDate = DateTime.Now,
                RequestHandlerId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                RequestStatus = RequestStatusEnum.Closed
            }
        };

        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
                maintenanceRequestRepository.GetAllMaintenanceRequests(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Returns(expectedRepositoryResponse);

        IEnumerable<MaintenanceRequest> actualResponse =
            _maintenanceRequestService.GetAllMaintenanceRequests(It.IsAny<Guid>(), It.IsAny<Guid>());

        Assert.AreEqual(expectedRepositoryResponse, actualResponse);
        Assert.IsTrue(expectedRepositoryResponse.SequenceEqual(actualResponse));
    }

    [TestMethod]
    public void GetAllMaintenanceRequests_RepositoryThrowsException_UnknownServiceExceptionIsThrown()
    {
        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
            maintenanceRequestRepository.GetAllMaintenanceRequests(It.IsAny<Guid>(), It.IsAny<Guid>())).Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _maintenanceRequestService.GetAllMaintenanceRequests(It.IsAny<Guid>(), It.IsAny<Guid>()));
    }

    #endregion

    #region Get Maintenance Request By Category

    [TestMethod]
    public void GetMaintenanceRequestByCategory_MaintenanceRequestIsReturned()
    {
        Guid categoryId = Guid.NewGuid();

        IEnumerable<MaintenanceRequest> maintenanceRequestSample = new List<MaintenanceRequest>
        {
            _maintenanceRequestSample
        };

        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
            maintenanceRequestRepository.GetMaintenanceRequestByCategory(categoryId)).Returns(maintenanceRequestSample);

        IEnumerable<MaintenanceRequest> actualResponse =
            _maintenanceRequestService.GetMaintenanceRequestByCategory(categoryId);

        Assert.IsTrue(maintenanceRequestSample.SequenceEqual(actualResponse));
    }

    [TestMethod]
    public void GetMaintenanceRequestByCategory_CategoryNotFound()
    {
        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
            maintenanceRequestRepository.GetMaintenanceRequestByCategory(It.IsAny<Guid>())).Returns(() => null);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() =>
            _maintenanceRequestService.GetMaintenanceRequestByCategory(Guid.NewGuid()));

        _maintenanceRequestRepository.VerifyAll();
    }

    [TestMethod]
    public void GetMaintenanceRequestByCategory_UnknownServiceExceptionIsThrown()
    {
        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
            maintenanceRequestRepository.GetMaintenanceRequestByCategory(It.IsAny<Guid>())).Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _maintenanceRequestService.GetMaintenanceRequestByCategory(Guid.NewGuid()));
    }

    #endregion

    #region Create Maintenance Request

    [TestMethod]
    public void CreateMaintenanceRequestWithCorrectData_MaintenanceRequestIsCreated()
    {
        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
            maintenanceRequestRepository.CreateMaintenanceRequest(_maintenanceRequestSample));

        _maintenanceRequestService.CreateMaintenanceRequest(_maintenanceRequestSample);

        _maintenanceRequestRepository.VerifyAll();
    }

    #region Create Maintenance Requests - Domain Validations

    [TestMethod]
    public void CreateMaintenanceRequestWithEmptyDescription_ThrowsObjectErrorServiceException()
    {
        _maintenanceRequestSample.Description = string.Empty;

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _maintenanceRequestService.CreateMaintenanceRequest(_maintenanceRequestSample));
    }

    [TestMethod]
    public void CreateMaintenanceRequestWithEmptyFlatId_ThrowsObjectErrorServiceException()
    {
        _maintenanceRequestSample.FlatId = Guid.Empty;

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _maintenanceRequestService.CreateMaintenanceRequest(_maintenanceRequestSample));
    }

    [TestMethod]
    public void CreateMaintenanceRequestWithEmptyCategory_ThrowsObjectErrorServiceException()
    {
        _maintenanceRequestSample.Category = null;

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _maintenanceRequestService.CreateMaintenanceRequest(_maintenanceRequestSample));
    }

    [TestMethod]
    public void CreateMaintenanceRequestWithEmptyRequestHandlerId_ThrowsObjectErrorServiceException()
    {
        _maintenanceRequestSample.RequestHandlerId = Guid.Empty;

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _maintenanceRequestService.CreateMaintenanceRequest(_maintenanceRequestSample));
    }

    [TestMethod]
    public void CreateMaintenanceWithEmptyOpenDate_ThrowsObjectErrorServiceException()
    {
        _maintenanceRequestSample.OpenedDate = null;

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _maintenanceRequestService.CreateMaintenanceRequest(_maintenanceRequestSample));
    }

    [TestMethod]
    public void WhenMaintenanceRequestCreated_CloseDateIsNull()
    {
        MaintenanceRequest maintenanceRequestCloseDateValidation = new MaintenanceRequest();
        Assert.AreEqual(maintenanceRequestCloseDateValidation.ClosedDate, null);
    }

    #endregion

    #endregion

    #region Update Maintenance Request

    [TestMethod]
    public void UpdateMaintenanceRequest_MaintenanceRequestIsUpdated()
    {
        MaintenanceRequest maintenanceRequestInDb = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            Description = "Fix the door",
            FlatId = Guid.NewGuid(),
            OpenedDate = DateTime.Now,
            RequestHandlerId = Guid.NewGuid(),
            CategoryId = Guid.NewGuid(),
            RequestStatus = RequestStatusEnum.Open,
            Category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "category"
            },
            Flat = new Flat()
            {
                Id = Guid.NewGuid()
            },
            Manager = new Manager(),
            RequestHandler = new RequestHandler(),
            ManagerId = Guid.NewGuid(),
            ClosedDate = DateTime.Now.AddDays(1)
        };

        MaintenanceRequest maintenanceRequestWithUpdates = new MaintenanceRequest
        {
            Id = maintenanceRequestInDb.Id,
            RequestStatus = RequestStatusEnum.Closed,
        };

        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
                maintenanceRequestRepository.GetMaintenanceRequestById(maintenanceRequestInDb.Id))
            .Returns(maintenanceRequestInDb);

        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
            maintenanceRequestRepository.UpdateMaintenanceRequest(maintenanceRequestInDb.Id,
                maintenanceRequestWithUpdates));

        _maintenanceRequestService.UpdateMaintenanceRequest(maintenanceRequestInDb.Id, maintenanceRequestWithUpdates);

        _maintenanceRequestRepository.VerifyAll();
    }


    [TestMethod]
    public void UpdateMaintenanceRequest_MaintenanceRequestNotFound_ObjectNotFoundServiceExceptionIsThrown()
    {
        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
            maintenanceRequestRepository.GetMaintenanceRequestById(It.IsAny<Guid>())).Returns(() => null);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() =>
            _maintenanceRequestService.UpdateMaintenanceRequest(Guid.NewGuid(), _maintenanceRequestSample));

        _maintenanceRequestRepository.VerifyAll();
    }

    [TestMethod]
    public void UpdateMaintenanceRequest_RepositoryThrowsException_UnknownServiceExceptionIsThrown()
    {
        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
                maintenanceRequestRepository.GetMaintenanceRequestById(It.IsAny<Guid>()))
            .Returns(_maintenanceRequestSample);

        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
                maintenanceRequestRepository.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()))
            .Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _maintenanceRequestService.UpdateMaintenanceRequest(Guid.NewGuid(), _maintenanceRequestSample));

        _maintenanceRequestRepository.Verify(maintenanceRequestRepository =>
            maintenanceRequestRepository.GetMaintenanceRequestById(It.IsAny<Guid>()), Times.Once);
    }
    
    [TestMethod]
    public void AssignMaintenanceRequest_MaintenanceRequestIsAssigned()
    {
        Guid idToUpdate = Guid.NewGuid();
        Guid idOfWorker = Guid.NewGuid();

        MaintenanceRequest maintenanceRequest = new MaintenanceRequest
        {
            Id = idToUpdate,
            Description = "Fix the door",
            FlatId = Guid.NewGuid(),
            OpenedDate = DateTime.Now,
            RequestHandlerId = Guid.NewGuid(),
            CategoryId = Guid.NewGuid(),
            RequestStatus = RequestStatusEnum.Open
        };

        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
            maintenanceRequestRepository.GetMaintenanceRequestById(idToUpdate)).Returns(maintenanceRequest);

        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
            maintenanceRequestRepository.UpdateMaintenanceRequest(idToUpdate, maintenanceRequest));

        _maintenanceRequestService.AssignMaintenanceRequest(idToUpdate, idOfWorker);

        _maintenanceRequestRepository.VerifyAll();
    }

    [TestMethod]
    public void AssignMaintenanceRequest_RequestNotFound_ObjectNotFoundServiceExceptionIsThrown()
    {
        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
            maintenanceRequestRepository.GetMaintenanceRequestById(It.IsAny<Guid>())).Returns(() => null);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() =>
            _maintenanceRequestService.AssignMaintenanceRequest(Guid.NewGuid(), Guid.NewGuid()));

        _maintenanceRequestRepository.VerifyAll();
    }

    [TestMethod]
    public void AssignMaintenanceRequest_ThrowsUnknownErrorException()
    {
        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
                maintenanceRequestRepository.GetMaintenanceRequestById(It.IsAny<Guid>()))
            .Returns(_maintenanceRequestSample);

        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
                maintenanceRequestRepository.UpdateMaintenanceRequest(It.IsAny<Guid>(), It.IsAny<MaintenanceRequest>()))
            .Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _maintenanceRequestService.AssignMaintenanceRequest(Guid.NewGuid(), Guid.NewGuid()));

        _maintenanceRequestRepository.VerifyAll();
    }
    
    [TestMethod]
    public void AssignMaintenanceRequest_RequestIsAlreadyInProgress_ObjectErrorServiceExceptionIsThrown()
    {
        MaintenanceRequest maintenanceRequest = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            Description = "Fix the door",
            FlatId = Guid.NewGuid(),
            OpenedDate = DateTime.Now,
            RequestHandlerId = Guid.NewGuid(),
            CategoryId = Guid.NewGuid(),
            RequestStatus = RequestStatusEnum.InProgress
        };

        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
            maintenanceRequestRepository.GetMaintenanceRequestById(It.IsAny<Guid>())).Returns(maintenanceRequest);

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _maintenanceRequestService.AssignMaintenanceRequest(Guid.NewGuid(), Guid.NewGuid()));

        _maintenanceRequestRepository.VerifyAll();
    }
    
    [TestMethod]
    public void AssignMaintenanceRequest_RequestIsAlreadyClosed_ObjectErrorServiceExceptionIsThrown()
    {
        MaintenanceRequest maintenanceRequest = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            Description = "Fix the door",
            FlatId = Guid.NewGuid(),
            OpenedDate = DateTime.Now,
            RequestHandlerId = Guid.NewGuid(),
            CategoryId = Guid.NewGuid(),
            RequestStatus = RequestStatusEnum.Closed
        };

        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
            maintenanceRequestRepository.GetMaintenanceRequestById(It.IsAny<Guid>())).Returns(maintenanceRequest);

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _maintenanceRequestService.AssignMaintenanceRequest(Guid.NewGuid(), Guid.NewGuid()));

        _maintenanceRequestRepository.VerifyAll();
    }

    #endregion
    
    #region Get Maintenance Request By Id

    [TestMethod]
    public void GetMaintenanceRequestById_MaintenanceRequestIsReturned()
    {
        Guid id = Guid.NewGuid();

        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
            maintenanceRequestRepository.GetMaintenanceRequestById(id)).Returns(_maintenanceRequestSample);

        MaintenanceRequest actualResponse = _maintenanceRequestService.GetMaintenanceRequestById(id);

        Assert.AreEqual(_maintenanceRequestSample, actualResponse);
    }

    [TestMethod]
    public void GetMaintenanceRequestById_MaintenanceRequestNotFound_ObjectNotFoundServiceExceptionIsThrown()
    {
        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
            maintenanceRequestRepository.GetMaintenanceRequestById(It.IsAny<Guid>())).Returns(() => null);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() =>
            _maintenanceRequestService.GetMaintenanceRequestById(Guid.NewGuid()));
    }

    [TestMethod]
    public void GetMaintenanceRequestById_RepositoryThrowsException_UnknownServiceExceptionIsThrown()
    {
        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
            maintenanceRequestRepository.GetMaintenanceRequestById(It.IsAny<Guid>())).Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _maintenanceRequestService.GetMaintenanceRequestById(Guid.NewGuid()));
    }

    #endregion

    #region Get Maintenance Requests By Request Handler

    [TestMethod]
    public void GetMaintenanceRequestsByRequestHandler_MaintenanceRequestsAreReturned()
    {
        Guid requestHandlerId = Guid.NewGuid();

        IEnumerable<MaintenanceRequest> expectedRepositoryResponse = new List<MaintenanceRequest>
        {
            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                Description = "Fix the door",
                FlatId = Guid.NewGuid(),
                OpenedDate = DateTime.Now,
                RequestHandlerId = requestHandlerId,
                CategoryId = Guid.NewGuid(),
                RequestStatus = RequestStatusEnum.Closed
            },

            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                Description = "Fix the window",
                FlatId = Guid.NewGuid(),
                OpenedDate = DateTime.Now,
                RequestHandlerId = requestHandlerId,
                CategoryId = Guid.NewGuid(),
                RequestStatus = RequestStatusEnum.Closed
            }
        };

        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
                maintenanceRequestRepository.GetMaintenanceRequestsByRequestHandler(requestHandlerId))
            .Returns(expectedRepositoryResponse);

        IEnumerable<MaintenanceRequest> actualResponse =
            _maintenanceRequestService.GetMaintenanceRequestsByRequestHandler(requestHandlerId);

        Assert.AreEqual(expectedRepositoryResponse, actualResponse);
        Assert.IsTrue(expectedRepositoryResponse.SequenceEqual(actualResponse));
    }

    [TestMethod]
    public void GetMaintenanceRequestsByRequestHandler_RequestHandlerNotFound_ObjectNotFoundServiceExceptionIsThrown()
    {
        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
            maintenanceRequestRepository.GetMaintenanceRequestsByRequestHandler(It.IsAny<Guid>())).Returns(() => null);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() =>
            _maintenanceRequestService.GetMaintenanceRequestsByRequestHandler(Guid.NewGuid()));
    }

    [TestMethod]
    public void GetMaintenanceRequestsByRequestHandler_RepositoryThrowsException_UnknownServiceExceptionIsThrown()
    {
        _maintenanceRequestRepository.Setup(maintenanceRequestRepository =>
                maintenanceRequestRepository.GetMaintenanceRequestsByRequestHandler(It.IsAny<Guid>()))
            .Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _maintenanceRequestService.GetMaintenanceRequestsByRequestHandler(Guid.NewGuid()));
    }

    #endregion
}