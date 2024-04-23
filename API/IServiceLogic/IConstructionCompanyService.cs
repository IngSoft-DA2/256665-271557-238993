using Domain;
using WebModel.Responses.ConstructionCompanyResponses;

namespace IServiceLogic;

public interface IConstructionCompanyService
{
    public IEnumerable<ConstructionCompany> GetAllConstructionCompanies();
    public ConstructionCompany GetConstructionCompanyById(Guid idOfConstructionCompany);
    public void CreateConstructionCompany(ConstructionCompany constructionCompanyToAdd);
}