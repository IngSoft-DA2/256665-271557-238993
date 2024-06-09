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
                    UserCreatorId = constructionCompany.UserCreatorId,
                    BuildingsId = constructionCompany.Buildings.Select(building => building.Id).ToList()
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
                UserCreatorId = constructionCompanyInDb.UserCreatorId,
                BuildingsId = constructionCompanyInDb.Buildings.Select(building => building.Id).ToList()
            };

            return constructionCompanyToReturn;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Construction company was not found");
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }

    

    #endregion

    #region Get construction company response by user creator id
    public GetConstructionCompanyResponse GetConstructionCompanyByUserCreatorId(Guid userId)
    {
        try
        {
            ConstructionCompany constructionCompanyFound =
                _constructionCompanyService.GetConstructionCompanyByUserCreatorId(userId);


            GetConstructionCompanyResponse constructionCompanyToReturn = new GetConstructionCompanyResponse
            {
                Id = constructionCompanyFound.Id,
                Name = constructionCompanyFound.Name,
                UserCreatorId = constructionCompanyFound.UserCreatorId,
                BuildingsId = constructionCompanyFound.Buildings.Select(building => building.Id).ToList()
            };

            return constructionCompanyToReturn;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Construction company was not found");
        }
        catch (UnknownServiceException exceptionCaught)
        {
            throw new UnknownAdapterException(exceptionCaught.Message);
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
                Name = createConstructionCompanyRequest.Name,
                UserCreatorId = createConstructionCompanyRequest.UserCreatorId
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

    public void UpdateConstructionCompany(Guid id, UpdateConstructionCompanyRequest request)
    {
        try
        {
            ConstructionCompany constructionCompanyToUpdate = new ConstructionCompany
            {
                Id = id,
                Name = request.Name
            };
            _constructionCompanyService.UpdateConstructionCompany(constructionCompanyToUpdate);
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Construction company was not found");
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }

    #endregion
}