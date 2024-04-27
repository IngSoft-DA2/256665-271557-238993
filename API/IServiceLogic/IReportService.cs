using Domain;
using WebModel.Requests.ReportRequests;
using WebModel.Responses.ReportResponses;

namespace IServiceLogic;

public interface IReportService
{
    IEnumerable<Report> GetMaintenanceReportByBuilding(GetMaintenanceReportByBuildingRequest request);
    IEnumerable<RequestHandlerReport> GetMaintenanceReportByRequestHandler(
        GetMaintenanceReportByRequestHandlerResponse request);

    IEnumerable<Report> GetMaintenanceReportByCategory(GetMaintenanceReportByCategoryResponse request);
}