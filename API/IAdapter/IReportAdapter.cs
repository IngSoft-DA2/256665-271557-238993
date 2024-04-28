using WebModel.Requests.ReportRequests;
using WebModel.Responses.ReportResponses;

namespace IAdapter;

public interface IReportAdapter
{
    public IEnumerable<GetMaintenanceReportByBuildingResponse> GetMaintenanceRequestsByBuilding(
        Guid buildingId);
    public IEnumerable<GetMaintenanceReportByRequestHandlerResponse> GetMaintenanceRequestsByRequestHandler(Guid requestHandlerId);
    public IEnumerable<GetMaintenanceReportByCategoryResponse> GetMaintenanceRequestsByCategory(Guid categoryId);
}