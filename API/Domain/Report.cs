namespace Domain;

public class Report
{
    public int ClosedRequests { get; set; }
    public int OpenRequests { get; set; }
    public int OnAttendanceRequests { get; set; }
    public Guid IdOfResourceToReport { get; set; }

    public override bool Equals(object? obj)
    {
        Report? objectToCompare = obj as Report;
        
        return IdOfResourceToReport == objectToCompare.IdOfResourceToReport &&
               ClosedRequests == objectToCompare.ClosedRequests &&
               OpenRequests == objectToCompare.OpenRequests &&
               OnAttendanceRequests == objectToCompare.OnAttendanceRequests;
    }
}