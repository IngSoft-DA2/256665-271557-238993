namespace WebModel.Responses.ReportResponses;

public class GetMaintenanceReportByBuildingResponse
{
    public int OpenRequests { get; set; }
    public int ClosedRequests { get; set; }
    public int OnAttendanceRequests { get; set; }
    public Guid BuildingId { get; set; }
    
    public override bool Equals(object? obj)
    {
        GetMaintenanceReportByBuildingResponse? objectToCompare = obj as GetMaintenanceReportByBuildingResponse;
        
        return OpenRequests == objectToCompare.OpenRequests && ClosedRequests == objectToCompare.ClosedRequests && OnAttendanceRequests == objectToCompare.OnAttendanceRequests && BuildingId == objectToCompare.BuildingId;

    }
    
}