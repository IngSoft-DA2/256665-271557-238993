﻿using WebModel.Requests.ReportRequests;
using WebModel.Responses.ReportResponses;

namespace IAdapter;

public interface IReportAdapter
{
    public IEnumerable<GetMaintenanceReportByBuildingResponse> GetMaintenanceReportByBuilding(
        Guid buildingId);
    public IEnumerable<GetMaintenanceReportByRequestHandlerResponse> GetMaintenanceReportByRequestHandler(Guid requestHandlerId);
    public IEnumerable<GetMaintenanceReportByCategoryResponse> GetMaintenanceReportByCategory(Guid categoryId);
    public IEnumerable<GetMaintenanceReportByBuildingResponse> GetAllBuildingMaintenanceReports();
    public IEnumerable<GetMaintenanceReportByRequestHandlerResponse> GetAllMaintenanceRequestsByRequestHandler();
    public IEnumerable<GetMaintenanceReportByCategoryResponse> GetAllMaintenanceRequestsByCategory();
}