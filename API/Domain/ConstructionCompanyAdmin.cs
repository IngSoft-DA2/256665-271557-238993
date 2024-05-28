namespace Domain;

public class ConstructionCompanyAdmin : SystemUser
{
    public string Lastname { get; set; }
    public ConstructionCompany? ConstructionCompany { get; set; }

    public void ConstructionCompanyAdminValidator()
    {
        PersonValidator();
    }
}