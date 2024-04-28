using System.Collections;
using Domain;

namespace IRepository;

public interface IMaintenanceRequestRepository
{
    public IEnumerable<MaintenanceRequest> GetAllMaintenanceRequests();
    public MaintenanceRequest GetMaintenanceRequestByCategory(Guid id);
    
}