using Domain.Enums;

namespace WebModel.Requests.ConstructionCompanyAdminRequests;

public class CreateConstructionCompanyAdminRequest
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Guid? InvitationId { get; set; } = null;

    public SystemUserRoleEnum? UserRole { get; set; } = null;
}