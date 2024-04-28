using Domain;
using IRepository;
using IServiceLogic;

namespace ServiceLogic;

public class ConstructionCompanyService : IConstructionCompanyService
{
    private readonly IConstructionCompanyRepository _constructionCompanyRepository;

    public ConstructionCompanyService(IConstructionCompanyRepository constructionCompanyRepository)
    {
        _constructionCompanyRepository = constructionCompanyRepository;
    }

    public IEnumerable<ConstructionCompany> GetAllConstructionCompanies()
    {

        return _constructionCompanyRepository.GetAllConstructionCompanies();
    }

    public ConstructionCompany GetConstructionCompanyById(Guid idOfConstructionCompany)
    {
        throw new NotImplementedException();
    }

    public void CreateConstructionCompany(ConstructionCompany constructionCompanyToAdd)
    {
        throw new NotImplementedException();
    }
}