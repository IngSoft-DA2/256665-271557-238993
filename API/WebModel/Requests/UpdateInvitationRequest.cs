namespace WebModel.Requests;

public class UpdateInvitationRequest
{
    public StatusEnumRequest Status { get; set; }
    public DateTime ExpirationDate { get; set; }
}