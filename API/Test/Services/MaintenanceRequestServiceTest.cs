using System.Collections;
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
        _maintenanceRequestRepository = new Mock<IMaintenanceRequestRepository>();
        _maintenanceRequestService = new MaintenanceRequestService(_maintenanceRequestRepository.Object);
        _maintenanceRequestSample = new MaintenanceRequest
        {
            Id = Guid.NewGuid(),
            BuildingId = Guid.NewGuid(),
            Description = "Fix the door",
            FlatId = Guid.NewGuid(),
            OpenedDate = DateTime.Now,
            RequestHandlerId = Guid.NewGuid(),
            Category = Guid.NewGuid(),
            RequestStatus = StatusEnum.Accepted
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
                BuildingId = Guid.NewGuid(),
                Description = "Fix the door",
                FlatId = Guid.NewGuid(),
                OpenedDate = DateTime.Now,
                RequestHandlerId = Guid.NewGuid(),
                Category = Guid.NewGuid(),
                RequestStatus = StatusEnum.Accepted
            },
            
            new MaintenanceRequest()
            {
                Id = Guid.NewGuid(),
                BuildingId = Guid.NewGuid(),
                Description = "Fix the window",
                FlatId = Guid.NewGuid(),
                OpenedDate = DateTime.Now,
                RequestHandlerId = Guid.NewGuid(),
                Category = Guid.NewGuid(),
                RequestStatus = StatusEnum.Accepted
            }
        };
        
        _maintenanceRequestRepository.Setup( maintenanceRequestRepository => 
            maintenanceRequestRepository.GetAllMaintenanceRequests()).Returns(expectedRepositoryResponse);
        
        IEnumerable<MaintenanceRequest> actualResponse = _maintenanceRequestService.GetAllMaintenanceRequests();
        
        Assert.AreEqual(expectedRepositoryResponse, actualResponse);
        Assert.IsTrue(expectedRepositoryResponse.SequenceEqual(actualResponse));
        
    }
    
    [TestMethod]
    public void GetAllMaintenanceRequests_RepositoryThrowsException_UnknownServiceExceptionIsThrown()
    {
        _maintenanceRequestRepository.Setup( maintenanceRequestRepository => 
            maintenanceRequestRepository.GetAllMaintenanceRequests()).Throws(new Exception());
        
        Assert.ThrowsException<UnknownServiceException>(() => _maintenanceRequestService.GetAllMaintenanceRequests());
    }
    
    #endregion
    
    #region Get Maintenance Request By Category

    [TestMethod]
    public void GetMaintenanceRequestByCategory_MaintenanceRequestIsReturned()
    {
        Guid categoryId = Guid.NewGuid();
        
        _maintenanceRequestRepository.Setup( maintenanceRequestRepository => 
            maintenanceRequestRepository.GetMaintenanceRequestByCategory(categoryId)).Returns(_maintenanceRequestSample);
        
        MaintenanceRequest actualResponse = _maintenanceRequestService.GetMaintenanceRequestByCategory(categoryId);
        
        Assert.AreEqual(_maintenanceRequestSample, actualResponse);
    }

    [TestMethod]
    public void GetMaintenanceRequestByCategory_CategoryNotFound()
    {
        _maintenanceRequestRepository.Setup( maintenanceRequestRepository => 
            maintenanceRequestRepository.GetMaintenanceRequestByCategory(It.IsAny<Guid>())).Returns(() => null);
        
        Assert.ThrowsException<ObjectNotFoundServiceException>(() => 
            _maintenanceRequestService.GetMaintenanceRequestByCategory(Guid.NewGuid()));
        
        _maintenanceRequestRepository.VerifyAll();
    }
    
    [TestMethod]
    public void GetMaintenanceRequestByCategory_UnknownServiceExceptionIsThrown()
    {
        _maintenanceRequestRepository.Setup( maintenanceRequestRepository => 
            maintenanceRequestRepository.GetMaintenanceRequestByCategory(It.IsAny<Guid>())).Throws(new Exception());
        
        Assert.ThrowsException<UnknownServiceException>(() => 
            _maintenanceRequestService.GetMaintenanceRequestByCategory(Guid.NewGuid()));
    }
    
    #endregion
    


}