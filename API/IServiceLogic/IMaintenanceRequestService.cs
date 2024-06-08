using Domain;

namespace IServiceLogic;

public interface IMaintenanceRequestService
{
    public IEnumerable<MaintenanceRequest> GetAllMaintenanceRequests(Guid? managerId, Guid categoryId);
    public IEnumerable<MaintenanceRequest> GetMaintenanceRequestByCategory(Guid id);
    public void CreateMaintenanceRequest(MaintenanceRequest maintenanceRequest);
    public void UpdateMaintenanceRequest(Guid idToUpdate, MaintenanceRequest maintenanceRequest);
    public void AssignMaintenanceRequest(Guid idToUpdate, Guid idOfWorker);
    public IEnumerable<MaintenanceRequest> GetMaintenanceRequestsByRequestHandler(Guid? requestHandlerId);
    public MaintenanceRequest GetMaintenanceRequestById(Guid id);
}