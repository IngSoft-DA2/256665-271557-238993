using Microsoft.AspNetCore.Mvc;

namespace WebModel.Responses.ReportResponses;

public class GetMaintenanceReportByRequestHandlerResponse
{
    public Guid RequestHandlerId { get; set; }
    public int OpenRequests { get; set; }
    public int ClosedRequests { get; set; }
    public int OnAttendanceRequests { get; set; }
    public TimeSpan AverageTimeToCloseRequest { get; set; }

    public override bool Equals(object obj)
    {
        GetMaintenanceReportByRequestHandlerResponse? objectToCompare = obj as GetMaintenanceReportByRequestHandlerResponse;
        
        return RequestHandlerId == objectToCompare.RequestHandlerId &&
               OpenRequests == objectToCompare.OpenRequests &&
               ClosedRequests == objectToCompare.ClosedRequests &&
               OnAttendanceRequests == objectToCompare.OnAttendanceRequests &&
               AverageTimeToCloseRequest == objectToCompare.AverageTimeToCloseRequest;
    }
}