using Adapter.CustomExceptions;
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
        try
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
        catch (Exception exceptionCaught)
        {
            Console.WriteLine(exceptionCaught.Message);
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }
}