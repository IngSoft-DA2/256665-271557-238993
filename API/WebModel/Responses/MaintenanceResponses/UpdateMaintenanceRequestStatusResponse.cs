namespace WebModel.Responses.MaintenanceResponses;

public class UpdateMaintenanceRequestStatusResponse
{
    public Guid Id { get; set; }
    public Guid BuildingId { get; set; }
    public string? Description { get; set; }
    public Guid FlatId { get; set; }
    public Guid Category { get; set; }
    public DateTime? OpenedDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public StatusEnumMaintenanceResponse RequestStatus { get; set; }
    
    public override bool Equals(object objectToCompare)
    {
        UpdateMaintenanceRequestStatusResponse? toCompare = objectToCompare as UpdateMaintenanceRequestStatusResponse;

        if (toCompare is null) return false;
        
        return Id == toCompare.Id &&
               BuildingId == toCompare.BuildingId &&
               Description == toCompare.Description &&
               FlatId == toCompare.FlatId &&
               Category == toCompare.Category &&
               RequestStatus == toCompare.RequestStatus;
    }
}