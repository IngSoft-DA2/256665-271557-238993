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

                switch (request.RequestStatus)
                {
                    case RequestStatusEnum.Open:
                        buildingReport.OpenRequests++;
                        break;
                    case RequestStatusEnum.InProgress:
                        buildingReport.OnAttendanceRequests++;
                        break;
                    case RequestStatusEnum.Closed:
                        buildingReport.ClosedRequests++;
                        break;
                }
            }

            return reportsDictionary.Values;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
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

            return reportsDictionary.Values;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    #endregion

    #region Get maintenance report by category

    public IEnumerable<Report> GetMaintenanceReportByCategory(Guid isAny)
    {
        try
        {
            IEnumerable<Report> reports = _reportRepository.GetMaintenanceReportByCategory(isAny);
            return reports;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    public IEnumerable<Report> GetAllMaintenanceRequestsByBuilding()
    {
        try
        {
            IEnumerable<Report> reports = _reportRepository.GetAllMaintenanceRequestsByBuilding();
            return reports;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    public IEnumerable<RequestHandlerReport> GetAllMaintenanceRequestsByRequestHandler()
    {
        try
        {
            IEnumerable<RequestHandlerReport> reports = _reportRepository.GetAllMaintenanceRequestsByRequestHandler();
            return reports;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    public IEnumerable<Report> GetAllMaintenanceRequestsByCategory()
    {
        try
        {
            IEnumerable<Report> reports = _reportRepository.GetAllMaintenanceRequestsByCategory();
            return reports;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    #endregion
}