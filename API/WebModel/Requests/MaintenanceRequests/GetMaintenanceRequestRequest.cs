namespace WebModel.Requests.MaintenanceRequests;

public class GetMaintenanceRequestRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
}