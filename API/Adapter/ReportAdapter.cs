using Adapter.CustomExceptions;
using Domain;
using IAdapter;
using IServiceLogic;
using Microsoft.AspNetCore.Http.Features;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.ReportResponses;

namespace Adapter;

public class ReportAdapter : IReportAdapter
{
    private IReportService _reportService;

    public ReportAdapter(IReportService reportService)
    {
        _reportService = reportService;
    }

    public IEnumerable<GetMaintenanceReportByBuildingResponse> GetMaintenanceReportByBuilding(Guid personId,
        Guid buildingId)
    {
        try
        {
            IEnumerable<Report> reports =
                _reportService.GetMaintenanceReportByBuilding(personId, buildingId);

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
        Guid requestHandlerId, Guid buildingId, Guid personId)
    {
        try
        {
            IEnumerable<RequestHandlerReport> reports =
                _reportService.GetMaintenanceReportByRequestHandler(requestHandlerId, buildingId, personId);

            IEnumerable<GetMaintenanceReportByRequestHandlerResponse> maintenanceReportResponses =
                reports.Select(report => new GetMaintenanceReportByRequestHandlerResponse()
                {
                    RequestHandlerId = report.IdOfResourceToReport,
                    ClosedRequests = report.ClosedRequests,
                    OpenRequests = report.OpenRequests,
                    OnAttendanceRequests = report.OnAttendanceRequests,
                    AverageTimeToCloseRequest = report.AvgTimeToCloseRequest
                });

            return maintenanceReportResponses;
        }
        catch (Exception exceptionCaught)
        {
            Console.WriteLine(exceptionCaught);
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }

    public IEnumerable<GetMaintenanceReportByCategoryResponse> GetMaintenanceReportByCategory(Guid buildingId,
        Guid categoryId)
    {
        try
        {
            IEnumerable<Report> reports =
                _reportService.GetMaintenanceReportByCategory(buildingId, categoryId);

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

    public IEnumerable<GetFlatRequestsReportByBuildingResponse> GetFlatRequestsByBuildingReport(Guid buildingId)
    {
        try
        {
            IEnumerable<FlatRequestReport> reports = _reportService.GetFlatRequestsByBuildingReport(buildingId);

            IEnumerable<GetFlatRequestsReportByBuildingResponse> flatReportResponses =
                reports.Select(report => new GetFlatRequestsReportByBuildingResponse()
                {
                    BuildingId = report.IdOfResourceToReport,
                    ClosedRequests = report.ClosedRequests,
                    OpenRequests = report.OpenRequests,
                    OnAttendanceRequests = report.OnAttendanceRequests,
                    OwnerName = report.OwnerName,
                    FlatNumber = report.FlatNumber
                });

            return flatReportResponses;
        }
        catch (Exception exceptionCaught)
        {
            Console.WriteLine(exceptionCaught);
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }
}