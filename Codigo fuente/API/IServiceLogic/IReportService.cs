using Domain;
using WebModel.Responses.ReportResponses;

namespace IServiceLogic;

public interface IReportService
{
    IEnumerable<Report> GetMaintenanceReportByBuilding(Guid personId, Guid request);
    IEnumerable<RequestHandlerReport> GetMaintenanceReportByRequestHandler(Guid requestHandlerId, Guid buildingId, Guid personId);
    IEnumerable<Report> GetMaintenanceReportByCategory(Guid buildingId, Guid request);
    IEnumerable<FlatRequestReport> GetFlatRequestsByBuildingReport(Guid buildingId);
}