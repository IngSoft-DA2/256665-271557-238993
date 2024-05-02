using System.Runtime.InteropServices.JavaScript;

namespace WebModel.Responses.MaintenanceResponses;

public class GetMaintenanceRequestResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Description { get; set; }
    public Guid FlatId { get; set; }
    public Guid Category { get; set; }
    public DateTime? OpenedDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public Guid? RequestHandlerId { get; set; }
    public StatusEnumMaintenanceResponse RequestStatus { get; set; }

    public override bool Equals(object objectToCompare)
    {
        GetMaintenanceRequestResponse? toCompare = objectToCompare as GetMaintenanceRequestResponse;

        return Id == toCompare.Id &&
               Description == toCompare.Description &&
               FlatId == toCompare.FlatId &&
               Category == toCompare.Category &&
               RequestStatus == toCompare.RequestStatus &&
               OpenedDate == toCompare.OpenedDate &&
               ClosedDate == toCompare.ClosedDate &&
               RequestHandlerId == toCompare.RequestHandlerId;
    }
}