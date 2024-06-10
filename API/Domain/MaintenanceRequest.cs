using Domain.Enums;

namespace Domain;

public class MaintenanceRequest
{
    public Guid Id { get; set; }
    public String Description { get; set; }
    public Flat Flat { get; set; }
    public Guid FlatId { get; set; }
    public DateTime? OpenedDate { get; set; }
    public DateTime? ClosedDate { get; set; }
    public RequestHandler? RequestHandler { get; set; }
    public Guid? RequestHandlerId { get; set; }
    public Manager Manager { get; set; }
    public Guid ManagerId { get; set; }
    public Category Category { get; set; }
    public Guid CategoryId { get; set; }
    public RequestStatusEnum RequestStatus { get; set; }

    public override bool Equals(object? obj)
    {
        MaintenanceRequest? maintenanceRequestToCompare = obj as MaintenanceRequest;

        return Id == maintenanceRequestToCompare.Id &&
               Description == maintenanceRequestToCompare.Description && 
               FlatId == maintenanceRequestToCompare.FlatId &&
               OpenedDate == maintenanceRequestToCompare.OpenedDate &&
               ClosedDate == maintenanceRequestToCompare.ClosedDate &&
               RequestHandlerId == maintenanceRequestToCompare.RequestHandlerId &&
               CategoryId == maintenanceRequestToCompare.CategoryId &&
               ManagerId == maintenanceRequestToCompare.ManagerId && 
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
        if (string.IsNullOrEmpty(Description))
        {
            throw new InvalidMaintenanceRequestException("Description is required");
        }
    }
}