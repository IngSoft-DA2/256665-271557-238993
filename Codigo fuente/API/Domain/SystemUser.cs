using Domain.CustomExceptions;
using Domain.Enums;

namespace Domain;

public abstract class SystemUser : Person
{
    public string Password { get; set; }
    public SystemUserRoleEnum Role { get; set; }
    
    public void PasswordValidator()
    {
        if (string.IsNullOrEmpty(Password) || Password.Length < 8)
        {
            throw new InvalidSystemUserException("Password must have at least 8 characters");
        }
    }

}