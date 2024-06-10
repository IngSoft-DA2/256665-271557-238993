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

    public IEnumerable<Report> GetMaintenanceReportByBuilding(Guid personId, Guid buildingId)
    {
        try
        {
            List<MaintenanceRequest> maintenanceRequestsFilteredBySelectedBuilding =
                _reportRepository.GetMaintenanceReportByBuilding(personId, buildingId).ToList();

            Dictionary<Guid, Report> reportsDictionary = new Dictionary<Guid, Report>();

            foreach (MaintenanceRequest request in maintenanceRequestsFilteredBySelectedBuilding)
            {
                if (!reportsDictionary.ContainsKey(request.Flat.BuildingId))
                {
                    reportsDictionary[request.Flat.BuildingId] = new Report
                    {
                        IdOfResourceToReport = request.Flat.BuildingId
                    };
                }

                Report buildingReport = reportsDictionary[request.Flat.BuildingId];
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

    public IEnumerable<RequestHandlerReport> GetMaintenanceReportByRequestHandler(Guid reportHandlerId,
        Guid buildingId, Guid personId)
    {
        try
        {
            List<MaintenanceRequest> maintenanceRequestsFilteredBySelectedBuilding =
                _reportRepository.GetMaintenanceReportByRequestHandler(reportHandlerId, buildingId, personId).ToList();

                Dictionary<Guid, RequestHandlerReport> reportsDictionary = new Dictionary<Guid, RequestHandlerReport>();

            foreach (MaintenanceRequest request in maintenanceRequestsFilteredBySelectedBuilding)
            {
                Guid requestHandlerId = Guid.NewGuid();
                if (request.RequestHandler != null)
                {
                    requestHandlerId = (Guid)request.RequestHandlerId;
                }
                
                if (!reportsDictionary.ContainsKey(requestHandlerId))
                {
                    reportsDictionary[requestHandlerId] = new RequestHandlerReport
                    {
                        IdOfResourceToReport = requestHandlerId
                    };
                }

                RequestHandlerReport handlerReport = reportsDictionary[requestHandlerId];
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
                if (!reportsDictionary.ContainsKey(request.CategoryId))
                {
                    reportsDictionary[request.CategoryId] = new Report
                    {
                        IdOfResourceToReport = request.CategoryId
                    };
                }

                Report categoryReport = reportsDictionary[request.CategoryId];
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
    
    #region Get flat requests by building report
    
    public IEnumerable<FlatRequestReport> GetFlatRequestsByBuildingReport(Guid buildingId)
    {
        try
        {
            List<MaintenanceRequest> maintenanceRequestsFilteredBySelectedBuilding =
                _reportRepository.GetFlatRequestsReportByBuilding(buildingId).ToList();

            Dictionary<Guid, FlatRequestReport> reportsDictionary = new Dictionary<Guid, FlatRequestReport>();

            foreach (MaintenanceRequest request in maintenanceRequestsFilteredBySelectedBuilding)
            {
                if (!reportsDictionary.ContainsKey(request.FlatId))
                {
                    reportsDictionary[request.FlatId] = new FlatRequestReport
                    {
                        IdOfResourceToReport = request.FlatId,
                        OwnerName = request.Flat.OwnerAssigned.Firstname,
                        FlatNumber = request.Flat.RoomNumber,
                        BuildingId = request.Flat.BuildingId
                    };
                }

                FlatRequestReport flatReport = reportsDictionary[request.FlatId];
                AddByCorespondingStatus(request, flatReport);
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