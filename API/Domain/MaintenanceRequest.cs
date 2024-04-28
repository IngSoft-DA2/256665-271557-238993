using Domain.Enums;

namespace Domain;

public class MaintenanceRequest
{
    public Guid Id = Guid.NewGuid();
    public Guid BuildingId { get; set; }
    public String Description { get; set; }
    public Guid FlatId { get; set; }
    public DateTime? OpenedDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public Guid RequestHandlerId { get; set; }
    public Guid Category { get; set; }
    public StatusEnum RequestStatus { get; set; }
    
    public override bool Equals(object? obj)
    {
        MaintenanceRequest? maintenanceRequestToCompare = obj as MaintenanceRequest;
        if (maintenanceRequestToCompare == null)
        {
            return false;
        }

        return Id == maintenanceRequestToCompare.Id && BuildingId == maintenanceRequestToCompare.BuildingId &&
               Description == maintenanceRequestToCompare.Description && FlatId == maintenanceRequestToCompare.FlatId &&
               OpenedDate == maintenanceRequestToCompare.OpenedDate && ClosedDate == maintenanceRequestToCompare.ClosedDate &&
               RequestHandlerId == maintenanceRequestToCompare.RequestHandlerId && Category == maintenanceRequestToCompare.Category &&
               RequestStatus == maintenanceRequestToCompare.RequestStatus;
    }

    public void MaintenanceRequestValidator()
    {
        DescriptionValidation();
        DateValidation();
        
        
    }

    private void DateValidation()
    {
        if (OpenedDate is null)
        {
            throw new InvalidMaintenanceRequestException("Opened date is required");
        }
    }

    private void DescriptionValidation()
    {
        if (String.IsNullOrEmpty(Description))
        {
            throw new InvalidMaintenanceRequestException("Description is required");
        }
    }
}