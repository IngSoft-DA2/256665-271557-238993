using Domain.Enums;

namespace Domain;

public class MaintenanceRequest
{
    public Guid Id { get; set; }
    public Guid BuildingId { get; set; }
    public string? Description { get; set; }
    public Guid FlatId { get; set; }
    public DateTime? OpenedDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public Guid RequestHandlerId { get; set; }
    public Guid Category { get; set; }
    public StatusEnum RequestStatus { get; set; }
}