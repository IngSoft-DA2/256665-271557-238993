using System.Collections;
using Domain;

namespace IRepository;

public interface IMaintenanceRequestRepository
{
    public IEnumerable<MaintenanceRequest> GetAllMaintenanceRequests(Guid? managerId, Guid categoryId);
    public IEnumerable<MaintenanceRequest> GetMaintenanceRequestByCategory(Guid categoryId);
    public void CreateMaintenanceRequest(MaintenanceRequest requestToCreate);

    void UpdateMaintenanceRequest(Guid isAny, MaintenanceRequest maintenanceRequestSample);
    MaintenanceRequest GetMaintenanceRequestById(Guid idToUpdate);
    IEnumerable<MaintenanceRequest> GetMaintenanceRequestsByRequestHandler(Guid? requestHandlerId);
}