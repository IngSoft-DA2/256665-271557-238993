namespace WebModel.Responses.ReportResponses;

public class GetMaintenanceReportByBuildingResponse : GetMaintenanceReportResponse
{
    public Guid Building { get; set; }
}