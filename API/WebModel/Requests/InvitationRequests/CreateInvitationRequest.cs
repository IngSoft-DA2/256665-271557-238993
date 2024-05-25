using Domain.Enums;

namespace WebModel.Requests.InvitationRequests;

public class CreateInvitationRequest
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public DateTime ExpirationDate { get; set; }
    public SystemUserRoleEnumRequest Role { get; set; }
}