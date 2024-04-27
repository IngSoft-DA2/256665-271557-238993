using WebModel.Requests.ReportRequests;
using WebModel.Responses.ReportResponses;

namespace IAdapter;

public interface IReportAdapter
{
    public IEnumerable<GetMaintenanceReportByBuildingResponse> GetMaintenanceRequestsByBuilding(
        GetMaintenanceReportByBuildingRequest getMaintenanceReportRequestByBuilding);
    public IEnumerable<GetMaintenanceReportByRequestHandlerResponse> GetMaintenanceRequestsByRequestHandler(GetMaintenanceReportRequest getMaintenanceReportRequestByUser);
    public IEnumerable<GetMaintenanceReportByCategoryResponse> GetMaintenanceRequestsByCategory(Guid buildingId, GetMaintenanceReportRequest getMaintenanceReportRequestByCategory);
}