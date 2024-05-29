using Domain.CustomExceptions;

namespace Domain;

public class ConstructionCompanyAdmin : SystemUser
{
    public string Lastname { get; set; }
    public ConstructionCompany? ConstructionCompany { get; set; }

    public void ConstructionCompanyAdminValidator()
    {
        try
        {
            PersonValidator();
            PasswordValidator();
        }
        catch (InvalidPersonException eexceptionCaught)
        {
            throw new InvalidConstructionCompanyAdminException(eexceptionCaught.Message);
        }
        catch (InvalidSystemUserException exceptionCaught)
        {
            throw new InvalidConstructionCompanyAdminException(exceptionCaught.Message);
        }

        LastnameValidator();
    }

    private void LastnameValidator()
    {
        if (string.IsNullOrEmpty(Lastname))
        {
            throw new InvalidConstructionCompanyAdminException("Lastname is required");
        }
    }
}