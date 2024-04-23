using Domain;
using IServiceLogic;
using WebModel.Responses.ConstructionCompanyResponses;

namespace Adapter;

public class ConstructionCompanyAdapter
{
    private readonly IConstructionCompanyService _constructionCompanyService;
    public ConstructionCompanyAdapter(IConstructionCompanyService constructionCompanyService)
    {
        _constructionCompanyService = constructionCompanyService;
    }

    public IEnumerable<GetConstructionCompanyResponse> GetAllConstructionCompanies()
    {
        IEnumerable<ConstructionCompany> constructionCompaniesInDb =
            _constructionCompanyService.GetAllConstructionCompanies();
        
        IEnumerable<GetConstructionCompanyResponse> constructionCompaniesToReturn = 
            constructionCompaniesInDb.Select(constructionCompany => new GetConstructionCompanyResponse
            {
                Id = constructionCompany.Id,
                Name = constructionCompany.Name,
            });
        return constructionCompaniesToReturn;
    }
}