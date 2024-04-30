using Adapter.CustomExceptions;
using Domain;
using IAdapter;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.ReportRequests;
using WebModel.Responses.ReportResponses;

namespace Adapter;

public class ReportAdapter : IReportAdapter
{
    private IReportService _reportService;
    public ReportAdapter(IReportService reportService)
    {
        _reportService = reportService;
    }

    public IEnumerable<GetMaintenanceReportByBuildingResponse> GetMaintenanceReportByBuilding(
        Guid buildingId)
    {
        try
        {
            IEnumerable<Report> reports =
                _reportService.GetMaintenanceReportByBuilding(buildingId);

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
        Guid requestHandlerId)
    {
        try
        {
            IEnumerable<RequestHandlerReport> reports =
                _reportService.GetMaintenanceReportByRequestHandler(requestHandlerId);

            IEnumerable<GetMaintenanceReportByRequestHandlerResponse> maintenanceReportResponses =
                reports.Select(report => new GetMaintenanceReportByRequestHandlerResponse()
                {
                    ClosedRequests = report.ClosedRequests,
                    OpenRequests = report.OpenRequests,
                    OnAttendanceRequests = report.OnAttendanceRequests,
                    AverageTimeToCloseRequest = report.AvgTimeToCloseRequest,
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
        Guid categoryId)
    {
        try
        {
            IEnumerable<Report> reports =
                _reportService.GetMaintenanceReportByCategory(categoryId);

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

    public IEnumerable<GetMaintenanceReportByBuildingResponse> GetAllBuildingMaintenanceReports()
    {
        try
        {
            IEnumerable<Report> reports = _reportService.GetAllMaintenanceRequestsByBuilding();

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

    public IEnumerable<GetMaintenanceReportByRequestHandlerResponse> GetAllMaintenanceRequestsByRequestHandler()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<GetMaintenanceReportByCategoryResponse> GetAllMaintenanceRequestsByCategory()
    {
        throw new NotImplementedException();
    }
}