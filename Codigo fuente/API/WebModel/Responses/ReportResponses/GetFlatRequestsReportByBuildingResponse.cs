using Domain;

namespace WebModel.Responses.ReportResponses;

public class GetFlatRequestsReportByBuildingResponse
{
    public string FlatNumber { get; set; }
    public string OwnerName { get; set; }
    public int OpenRequests { get; set; }
    public int ClosedRequests { get; set; }
    public int OnAttendanceRequests { get; set; }
    public Guid BuildingId { get; set; }
    
    public override bool Equals(object obj)
    {
        GetFlatRequestsReportByBuildingResponse? objectToCompare = obj as GetFlatRequestsReportByBuildingResponse;
        
        return FlatNumber == objectToCompare.FlatNumber &&
               OpenRequests == objectToCompare.OpenRequests &&
               ClosedRequests == objectToCompare.ClosedRequests &&
               OnAttendanceRequests == objectToCompare.OnAttendanceRequests &&
               OwnerName == objectToCompare.OwnerName;
    }
    
}