namespace WebModel.Responses.MaintenanceResponses;

public class AssignMaintenanceRequestResponse
{
    public Guid Id { get; set; }
    public Guid WorkerId { get; set; }

    public override bool Equals(object objectToCompare)
    {
        AssignMaintenanceRequestResponse? toCompare = objectToCompare as AssignMaintenanceRequestResponse;

        if (toCompare is null) return false;
        
        return Id == toCompare.Id &&
               WorkerId == toCompare.WorkerId;
    }
}