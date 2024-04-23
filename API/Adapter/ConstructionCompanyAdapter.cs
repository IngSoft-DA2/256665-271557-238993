using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.ConstructionCompanyRequests;
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

    public GetConstructionCompanyResponse GetConstructionCompanyById(Guid idOfConstructionCompany)
    {
        try
        {
            ConstructionCompany constructionCompanyInDb =
                _constructionCompanyService.GetConstructionCompanyById(idOfConstructionCompany);
            GetConstructionCompanyResponse constructionCompanyToReturn = new GetConstructionCompanyResponse
            {
                Id = constructionCompanyInDb.Id,
                Name = constructionCompanyInDb.Name,
            };

            return constructionCompanyToReturn;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }

    public CreateConstructionCompanyResponse CreateConstructionCompany(
        CreateConstructionCompanyRequest createConstructionCompanyRequest)
    {
        ConstructionCompany constructionCompanyToCreate = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = createConstructionCompanyRequest.Name
        };
        
        _constructionCompanyService.CreateConstructionCompany(constructionCompanyToCreate);

        CreateConstructionCompanyResponse response = new CreateConstructionCompanyResponse
        {
            Id = constructionCompanyToCreate.Id
        };
        return response;
    }
}