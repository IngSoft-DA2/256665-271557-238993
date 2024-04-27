using Domain;

namespace IServiceLogic;

public interface IMaintenanceRequestService
{
    public IEnumerable<MaintenanceRequest> GetAllMaintenanceRequests();
    public MaintenanceRequest GetMaintenanceRequestById(Guid id);
    public void CreateMaintenanceRequest(MaintenanceRequest maintenanceRequest);
    public void UpdateMaintenanceRequest(Guid idToUpdate, MaintenanceRequest maintenanceRequest);
    public void AssignMaintenanceRequest(Guid idToUpdate, Guid idOfWorker);
    
    
}