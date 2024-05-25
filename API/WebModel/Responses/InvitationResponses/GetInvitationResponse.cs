using Domain.Enums;

namespace WebModel.Responses.InvitationResponses;

public class GetInvitationResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public StatusEnumResponse Status { get; set; }
    public DateTime ExpirationDate { get; set; }
    public SystemUserRoleEnumResponse Role { get; set; }

    public override bool Equals(object objectToCompare)
    {
        GetInvitationResponse? toCompare = objectToCompare as GetInvitationResponse;

        return Id == toCompare.Id &&
               Firstname == toCompare.Firstname &&
               Lastname == toCompare.Lastname &&
               Email == toCompare.Email &&
               Status == toCompare.Status &&
               ExpirationDate == toCompare.ExpirationDate &&
               Role == toCompare.Role;
    }
}