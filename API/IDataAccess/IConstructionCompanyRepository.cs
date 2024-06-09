using Domain;

namespace IDataAccess;

public interface IConstructionCompanyRepository
{
    public IEnumerable<ConstructionCompany> GetAllConstructionCompanies();
    public ConstructionCompany GetConstructionCompanyById(Guid idOfConstructionCompany);
    public void CreateConstructionCompany(ConstructionCompany constructionCompanyToAdd);
    public void UpdateConstructionCompany(ConstructionCompany constructionCompanyToUpdate);
    public ConstructionCompany GetConstructionCompanyByUserCreatorId(Guid idOfUserCreator);
}