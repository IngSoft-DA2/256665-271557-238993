using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using WebModel.Requests.ReportRequests;
using WebModel.Responses.ReportResponses;

namespace Adapter;

public class ReportAdapter
{
    private IReportService _reportService;
    public ReportAdapter(IReportService reportService)
    {
        _reportService = reportService;
    }

    public IEnumerable<GetMaintenanceReportByBuildingResponse> GetMaintenanceReportByBuilding(
        GetMaintenanceReportByBuildingRequest request)
    {
        try
        {
            IEnumerable<Report> reports =
                _reportService.GetMaintenanceReportByBuilding(request);

            IEnumerable<GetMaintenanceReportByBuildingResponse> maintenanceReportResponses =
                reports.Select(report => new GetMaintenanceReportByBuildingResponse()
                {
                    ClosedRequests = report.ClosedRequests,
                    OpenRequests = report.OpenRequests,
                    OnAttendanceRequests = report.OnAttendanceRequests,
                    BuildingId = report.IdOfResourceToReport
                });

            return maintenanceReportResponses;
        }
        catch (Exception exceptionCaught)
        {
            Console.WriteLine(exceptionCaught);
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }

    public IEnumerable<GetMaintenanceReportByRequestHandlerResponse> GetMaintenanceReportByRequestHandler(
        GetMaintenanceReportByRequestHandlerResponse request)
    {
        try
        {
            IEnumerable<RequestHandlerReport> reports =
                _reportService.GetMaintenanceReportByRequestHandler(request);

            IEnumerable<GetMaintenanceReportByRequestHandlerResponse> maintenanceReportResponses =
                reports.Select(report => new GetMaintenanceReportByRequestHandlerResponse()
                {
                    ClosedRequests = report.ClosedRequests,
                    OpenRequests = report.OpenRequests,
                    OnAttendanceRequests = report.OnAttendanceRequests,
                    AverageTimeToCloseRequest = report.AvgTimeToCloseRequest,
                    RequestHandlerId = report.RequestHandlerId,
                });

            return maintenanceReportResponses;
        }
        catch (Exception exceptionCaught)
        {
            Console.WriteLine(exceptionCaught);
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }

    public IEnumerable<GetMaintenanceReportByCategoryResponse> GetMaintenanceReportByCategory(
        GetMaintenanceReportByCategoryResponse request)
    {
        try
        {
            IEnumerable<Report> reports =
                _reportService.GetMaintenanceReportByCategory(request);

            IEnumerable<GetMaintenanceReportByCategoryResponse> maintenanceReportResponses =
                reports.Select(report => new GetMaintenanceReportByCategoryResponse()
                {
                    ClosedRequests = report.ClosedRequests,
                    OpenRequests = report.OpenRequests,
                    OnAttendanceRequests = report.OnAttendanceRequests,
                    CategoryId = report.IdOfResourceToReport
                });

            return maintenanceReportResponses;
        }
        catch (Exception exceptionCaught)
        {
            Console.WriteLine(exceptionCaught);
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }
}