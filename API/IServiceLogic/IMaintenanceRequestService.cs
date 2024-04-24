using Domain;

namespace IServiceLogic;

public interface IMaintenanceRequestService
{
    public IEnumerable<MaintenanceRequest> GetAllMaintenanceRequests();
    public MaintenanceRequest GetMaintenanceRequestById(Guid id);
    
    
}