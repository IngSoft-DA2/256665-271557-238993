using WebModel.Requests.ReportRequests;
using WebModel.Responses.ReportResponses;

namespace IAdapter;

public interface IReportAdapter
{
    public IEnumerable<GetMaintenanceReportResponse> GetRequestsByBuilding(Guid idOfBuilding, GetMaintenanceReportRequest getMaintenanceReportRequestByBuilding);
}