using Domain;

namespace IRepository;

public interface IReportRepository
{
    public IEnumerable<MaintenanceRequest> GetMaintenanceReportByBuilding(Guid personId, Guid buildingId);
    public IEnumerable<MaintenanceRequest> GetMaintenanceReportByRequestHandler(Guid? requestHandlerId, Guid buildingId,
        Guid personId);
    public IEnumerable<MaintenanceRequest> GetMaintenanceReportByCategory(Guid buildingId, Guid categoryId);
    public IEnumerable<MaintenanceRequest> GetFlatRequestsReportByBuilding(Guid buildingId);
}