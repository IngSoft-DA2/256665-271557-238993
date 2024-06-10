namespace WebModel.Requests.MaintenanceRequests;

public class CreateRequestMaintenanceRequest
{
    public string Description { get; set; }
    public Guid FlatId { get; set; }
    public Guid Category { get; set; }
    public Guid ManagerId { get; set; }
}