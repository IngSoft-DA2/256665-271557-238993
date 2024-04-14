namespace WebModels.Responses;

public class CreateInvitationResponse
{
    public Guid Id { get; set; }
    public StatusEnumResponse Status { get; set; }
    
    
    public override bool Equals(object objectToCompare)
    {
        CreateInvitationResponse? toCompare = objectToCompare as CreateInvitationResponse;

        if (toCompare is null) return false;

        return (Id == toCompare.Id && Status.Equals(toCompare.Status));
    }
    
}