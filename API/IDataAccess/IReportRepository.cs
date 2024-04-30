using Domain;

namespace IRepository;

public interface IReportRepository
{
    public IEnumerable<Report> GetMaintenanceReportByBuilding(Guid buildingId);

    public IEnumerable<RequestHandlerReport> GetMaintenanceReportByRequestHandler(Guid requestHandlerId);

    public IEnumerable<Report> GetMaintenanceReportByCategory(Guid categoryId);
    public IEnumerable<Report> GetAllMaintenanceRequestsByBuilding();
}