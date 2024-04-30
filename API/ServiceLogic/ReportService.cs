using Domain;
using Domain.Enums;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class ReportService : IReportService
{
    #region Constructor and Dependency Injection

    private readonly IReportRepository _reportRepository;

    public ReportService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    #endregion

    #region Get maintenance report by building

    public IEnumerable<Report> GetMaintenanceReportByBuilding(Guid buildingId)
    {
        try
        {
            List<MaintenanceRequest> maintenanceRequestsFilteredBySelectedBuilding =
                _reportRepository.GetMaintenanceReportByBuilding(buildingId).ToList();

            Dictionary<Guid, Report> reportsDictionary = new Dictionary<Guid, Report>();

            foreach (MaintenanceRequest request in maintenanceRequestsFilteredBySelectedBuilding)
            {
                if (!reportsDictionary.ContainsKey(request.BuildingId))
                {
                    reportsDictionary[request.BuildingId] = new Report
                    {
                        IdOfResourceToReport = request.BuildingId
                    };
                }
                Report buildingReport = reportsDictionary[request.BuildingId];
                AddByCorespondingStatus(request, buildingReport);
            }
            return reportsDictionary.Values;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    private static void AddByCorespondingStatus(MaintenanceRequest request, Report report)
    {
        switch (request.RequestStatus)
        {
            case RequestStatusEnum.Open:
                report.OpenRequests++;
                break;
            case RequestStatusEnum.InProgress:
                report.OnAttendanceRequests++;
                break;
            case RequestStatusEnum.Closed:
                report.ClosedRequests++;
                break;
        }
    }

    #endregion

    #region Get maintenance report by request handler

    public IEnumerable<RequestHandlerReport> GetMaintenanceReportByRequestHandler(Guid isAny)
    {
        try
        {
            List<MaintenanceRequest> maintenanceRequestsFilteredBySelectedBuilding =
                _reportRepository.GetMaintenanceReportByRequestHandler(isAny).ToList();

            Dictionary<Guid, RequestHandlerReport> reportsDictionary = new Dictionary<Guid, RequestHandlerReport>();

            foreach (MaintenanceRequest request in maintenanceRequestsFilteredBySelectedBuilding)
            {
                if (!reportsDictionary.ContainsKey(request.RequestHandlerId))
                {
                    reportsDictionary[request.RequestHandlerId] = new RequestHandlerReport
                    {
                        IdOfResourceToReport = request.RequestHandlerId
                    };
                }
                RequestHandlerReport handlerReport = reportsDictionary[request.RequestHandlerId];
                AddByCorrespondingStatusOnRequestHandlerReport(request, handlerReport);
            }
            return reportsDictionary.Values;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    private static void AddByCorrespondingStatusOnRequestHandlerReport(MaintenanceRequest request,
        RequestHandlerReport handlerReport)
    {
        if (request.RequestStatus == RequestStatusEnum.Closed && request.ClosedDate != null &&
            request.OpenedDate != null)
        {
            TimeSpan timeToClose = request.ClosedDate.Value - request.OpenedDate.Value;
            handlerReport.TotalTime += timeToClose;
            handlerReport.ClosedRequests++;
        }
        else
        {
            switch (request.RequestStatus)
            {
                case RequestStatusEnum.Open:
                    handlerReport.OpenRequests++;
                    break;
                case RequestStatusEnum.InProgress:
                    handlerReport.OnAttendanceRequests++;
                    break;
            }
        }

        if (handlerReport.ClosedRequests > 0)
        {
            handlerReport.AvgTimeToCloseRequest = handlerReport.TotalTime / handlerReport.ClosedRequests;
        }
    }

    #endregion

    #region Get maintenance report by category

    public IEnumerable<Report> GetMaintenanceReportByCategory(Guid buidlingId, Guid categoryId)
    {
        try
        {
            List<MaintenanceRequest> maintenanceRequestsFilteredBySelectedCategory =
                _reportRepository.GetMaintenanceReportByCategory(buidlingId, categoryId).ToList();

            Dictionary<Guid, Report> reportsDictionary = new Dictionary<Guid, Report>();

            foreach (MaintenanceRequest request in maintenanceRequestsFilteredBySelectedCategory)
            {
                if (!reportsDictionary.ContainsKey(request.Category))
                {
                    reportsDictionary[request.Category] = new Report
                    {
                        IdOfResourceToReport = request.Category
                    };
                }
                Report categoryReport = reportsDictionary[request.Category];
                AddByCorespondingStatus(request, categoryReport);   
            }
            return reportsDictionary.Values;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }
    

    #endregion
}