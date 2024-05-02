using Domain;

namespace IRepository;

public interface IConstructionCompanyRepository
{
    public IEnumerable<ConstructionCompany> GetAllConstructionCompanies();
    public ConstructionCompany GetConstructionCompanyById(Guid idOfConstructionCompany);
    public void CreateConstructionCompany(ConstructionCompany constructionCompanyToAdd);
}