namespace WebModel.Responses.InvitationResponses;

public class CreateInvitationResponse
{
    public Guid Id { get; set; }
    public StatusEnumResponse Status { get; set; }
    
    public SystemUserRoleEnumResponse Role { get; set; }
    public override bool Equals(object objectToCompare)
    {
        CreateInvitationResponse? toCompare = objectToCompare as CreateInvitationResponse;
        
        return (Id == toCompare.Id && Status.Equals(toCompare.Status));
    }
    
}