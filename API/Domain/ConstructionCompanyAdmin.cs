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
        catch (InvalidPersonException exceptionCaught)
        {
            throw new InvalidConstructionCompanyAdminException(exceptionCaught.Message);
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

    public override bool Equals(object? objToCompare)
    {
        ConstructionCompanyAdmin constructionCompanyAdminToCompare = objToCompare as ConstructionCompanyAdmin;

        return Id == constructionCompanyAdminToCompare.Id && Firstname == constructionCompanyAdminToCompare.Firstname &&
               Lastname == constructionCompanyAdminToCompare.Lastname &&
               Email == constructionCompanyAdminToCompare.Email &&
               Password == constructionCompanyAdminToCompare.Password &&
               ConstructionCompany == constructionCompanyAdminToCompare.ConstructionCompany &&
               Role == constructionCompanyAdminToCompare.Role;
    }
}