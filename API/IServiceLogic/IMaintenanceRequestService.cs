using Domain;

namespace IServiceLogic;

public interface IMaintenanceRequestService
{
    public IEnumerable<MaintenanceRequest> GetAllMaintenanceRequests();
    
    
}