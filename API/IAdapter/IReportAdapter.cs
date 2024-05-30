using WebModel.Responses.ReportResponses;

namespace IAdapter;

public interface IReportAdapter
{
    public IEnumerable<GetMaintenanceReportByBuildingResponse> GetMaintenanceReportByBuilding(Guid personId,
        Guid buildingId);
    public IEnumerable<GetMaintenanceReportByRequestHandlerResponse> GetMaintenanceReportByRequestHandler(Guid requestHandlerId, Guid buildingId, Guid personId);
    public IEnumerable<GetMaintenanceReportByCategoryResponse> GetMaintenanceReportByCategory(Guid buildingId, Guid categoryId);
    
    public IEnumerable<GetFlatRequestsReportByBuildingResponse> GetFlatRequestsByBuildingReport(Guid buildingId);
}