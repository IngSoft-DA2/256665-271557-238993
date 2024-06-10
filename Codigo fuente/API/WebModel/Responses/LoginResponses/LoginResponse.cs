using Domain.Enums;

namespace WebModel.Responses.LoginResponses;

public class LoginResponse
{
    public Guid UserId { get; set; }
    public Guid SessionString { get; set; }
    public SystemUserRoleEnum UserRole { get; set; }
    
    public override bool Equals(object? obj)
    {
        LoginResponse objToCompareWith = obj as LoginResponse;
      
        return UserId == objToCompareWith.UserId && SessionString == objToCompareWith.SessionString && UserRole == objToCompareWith.UserRole;
    }
}