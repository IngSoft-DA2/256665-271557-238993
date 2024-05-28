namespace WebModel.Responses.ReportResponses;

public class GetMaintenanceReportByCategoryResponse
{
    public int OpenRequests { get; set; }
    public int ClosedRequests { get; set; }
    public int OnAttendanceRequests { get; set; }
    public Guid CategoryId { get; set; }
    
    public override bool Equals(object? obj)
    {
        GetMaintenanceReportByCategoryResponse? objectToCompare = obj as GetMaintenanceReportByCategoryResponse;
        return OpenRequests == objectToCompare.OpenRequests && ClosedRequests == objectToCompare.ClosedRequests && OnAttendanceRequests == objectToCompare.OnAttendanceRequests && CategoryId == objectToCompare.CategoryId;
    }
}
