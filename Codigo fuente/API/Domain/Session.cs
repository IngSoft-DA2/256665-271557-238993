using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain;

public class Session
{
    [Key] 
    public Guid SessionString { get; set; }
    public Guid UserId { get; set; }
    public SystemUserRoleEnum UserRole { get; set; }

    public Session()
    {
        SessionString = Guid.NewGuid();
    }

    public override bool Equals(object? obj)
    {
        Session objToCompareWith = obj as Session;
     
        return SessionString == objToCompareWith.SessionString && UserId == objToCompareWith.UserId && UserRole == objToCompareWith.UserRole;
    }
}