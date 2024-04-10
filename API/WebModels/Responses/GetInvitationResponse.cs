using Domain;

namespace WebModels.Responses;

public class GetInvitationResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Firstname { get; set; }
    public string Email { get; set; }
    public StatusEnumResponse Status { get; set; }
    public DateTime ExpirationDate { get; set; }

    public GetInvitationResponse(Invitation invitationWithData)
    {
        Id = invitationWithData.Id;
        Firstname = invitationWithData.Firstname;
        Email = invitationWithData.Email;
        Status = (StatusEnumResponse)invitationWithData.Status;
        ExpirationDate = invitationWithData.ExpirationDate;
        
    }
}