using Domain;

namespace IRepository;

public interface IReportRepository
{
    public IEnumerable<MaintenanceRequest> GetMaintenanceReportByBuilding(Guid buildingId);
    public IEnumerable<MaintenanceRequest> GetMaintenanceReportByRequestHandler(Guid requestHandlerId);
    public IEnumerable<MaintenanceRequest> GetMaintenanceReportByCategory(Guid categoryId);
}