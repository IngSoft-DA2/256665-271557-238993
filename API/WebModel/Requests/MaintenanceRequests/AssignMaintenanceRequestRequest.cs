namespace WebModel.Requests.MaintenanceRequests;

public class AssignMaintenanceRequestRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BuildingId { get; set; }
    public string? Description { get; set; }
    public Guid FlatId { get; set; }
    public Guid Category { get; set; }
    public StatusEnumMaintenanceRequest RequestStatus { get; set; }
    public Guid WorkerId { get; set; }
}