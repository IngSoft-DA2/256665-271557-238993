using Adapter.CustomExceptions;
using Domain;
using IAdapter;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.ConstructionCompanyRequests;
using WebModel.Responses.ConstructionCompanyResponses;

namespace Adapter;

public class ConstructionCompanyAdapter : IConstructionCompanyAdapter
{
    #region Constructor and atributes

    private readonly IConstructionCompanyService _constructionCompanyService;

    public ConstructionCompanyAdapter(IConstructionCompanyService constructionCompanyService)
    {
        _constructionCompanyService = constructionCompanyService;
    }

    #endregion

    #region Get all construction companies responses

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

    #endregion

    #region Get construction company response by id

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

    #endregion

    #region Create construction company response

    public CreateConstructionCompanyResponse CreateConstructionCompany(
        CreateConstructionCompanyRequest createConstructionCompanyRequest)
    {
        try
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
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (ObjectRepeatedServiceException exceptionCaught)
        {
            throw new ObjectRepeatedAdapterException();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }

    #endregion
}