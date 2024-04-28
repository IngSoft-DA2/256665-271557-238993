namespace Domain;

public class Report
{
    public int ClosedRequests { get; set; }
    public int OpenRequests { get; set; }
    public int OnAttendanceRequests { get; set; }
    public Guid IdOfResourceToReport { get; set; }
}