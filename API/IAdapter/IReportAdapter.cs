using WebModel.Requests.ReportRequests;
using WebModel.Responses.ReportResponses;

namespace IAdapter;

public interface IReportAdapter
{
    public IEnumerable<GetMaintenanceReportResponse> GetMaintenanceRequestsByBuilding(GetMaintenanceReportRequest getMaintenanceReportRequestByBuilding);
    public IEnumerable<GetMaintenanceReportResponse> GetMaintenanceRequestsByRequestHandler(GetMaintenanceReportRequest getMaintenanceReportRequestByUser);
    public IEnumerable<GetMaintenanceReportResponse> GetMaintenanceRequestsByCategory(Guid buildingId, GetMaintenanceReportRequest getMaintenanceReportRequestByCategory);
}