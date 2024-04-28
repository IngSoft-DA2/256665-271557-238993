using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class MaintenanceRequestService : IMaintenanceRequestService
{
    #region Constructor and Dependency Injection
    
    private readonly IMaintenanceRequestRepository _maintenanceRequestRepository;
    
    public MaintenanceRequestService(IMaintenanceRequestRepository maintenanceRequestRepository)
    {
        _maintenanceRequestRepository = maintenanceRequestRepository;
    }

    #endregion

    public IEnumerable<MaintenanceRequest> GetAllMaintenanceRequests()
    {
        try
        {
            IEnumerable<MaintenanceRequest> maintenanceRequests = _maintenanceRequestRepository.GetAllMaintenanceRequests();
            return maintenanceRequests;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    public MaintenanceRequest GetMaintenanceRequestByCategory(Guid id)
    {
        throw new NotImplementedException();
    }

    public void CreateMaintenanceRequest(MaintenanceRequest maintenanceRequest)
    {
        throw new NotImplementedException();
    }

    public void UpdateMaintenanceRequest(Guid idToUpdate, MaintenanceRequest maintenanceRequest)
    {
        throw new NotImplementedException();
    }

    public void AssignMaintenanceRequest(Guid idToUpdate, Guid idOfWorker)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MaintenanceRequest> GetMaintenanceRequestsByRequestHandler(Guid requestHandlerId)
    {
        throw new NotImplementedException();
    }

    public MaintenanceRequest GetMaintenanceRequestById(Guid id)
    {
        throw new NotImplementedException();
    }
}