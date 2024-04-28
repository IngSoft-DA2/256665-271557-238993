using Domain;

namespace IRepository;

public interface IMaintenanceRequestRepository
{
    public IEnumerable<MaintenanceRequest> GetAllMaintenanceRequests();
}