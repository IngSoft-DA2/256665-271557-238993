using Domain.CustomExceptions;

namespace Domain;

public class ConstructionCompanyAdmin : SystemUser
{
    public string Lastname { get; set; }
    public ConstructionCompany? ConstructionCompany { get; set; }

    public void ConstructionCompanyAdminValidator()
    {
        PersonValidator();
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