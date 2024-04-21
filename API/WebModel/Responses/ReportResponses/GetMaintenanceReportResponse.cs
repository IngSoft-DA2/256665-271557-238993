namespace WebModel.Responses.ReportResponses;

public class GetMaintenanceReportResponse
{
    public int OpenRequests { get; set; }
    public int ClosedRequests { get; set; }
    public int OnAttendanceRequests { get; set; }
    public Guid IdOfResourceToReport { get; set; }
}