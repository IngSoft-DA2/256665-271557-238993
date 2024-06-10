using Domain;

namespace IServiceLogic;

public interface IConstructionCompanyService
{
    public IEnumerable<ConstructionCompany> GetAllConstructionCompanies();
    public ConstructionCompany GetConstructionCompanyById(Guid idOfConstructionCompany);
    public void CreateConstructionCompany(ConstructionCompany constructionCompanyToAdd);
    public void UpdateConstructionCompany(ConstructionCompany constructionCompanyWithUpdates);
    public ConstructionCompany GetConstructionCompanyByUserCreatorId(Guid idOfUserCreator);
}