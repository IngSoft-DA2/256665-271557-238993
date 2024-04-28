using Domain;

namespace IRepository;

public interface IReportRepository
{
    public IEnumerable<Report> GetMaintenanceReportByBuilding(Guid buildingId);

}