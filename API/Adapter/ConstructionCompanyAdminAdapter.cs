using Adapter.CustomExceptions;
using Domain;
using Domain.Enums;
using IAdapter;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.ConstructionCompanyAdminRequests;
using WebModel.Responses.ConstructionCompanyAdminResponses;

namespace Adapter;

public class ConstructionCompanyAdminAdapter : IConstructionCompanyAdminAdapter
{
    #region Constructor And Dependency Injection

    private readonly IConstructionCompanyAdminService _constructionCompanyAdminService;

    public ConstructionCompanyAdminAdapter(IConstructionCompanyAdminService constructionCompanyAdminService)
    {
        _constructionCompanyAdminService = constructionCompanyAdminService;
    }

    #endregion

    #region Create Construction Company Admin

    public CreateConstructionCompanyAdminResponse CreateConstructionCompanyAdmin(
        CreateConstructionCompanyAdminRequest createRequest)
    {
        try
        {
            ConstructionCompanyAdmin constructionCompanyAdminToCreate = new ConstructionCompanyAdmin
            {
                Id = Guid.NewGuid(),
                Firstname = createRequest.Firstname,
                Lastname = createRequest.Lastname,
                Email = createRequest.Email,
                Password = createRequest.Password,
                ConstructionCompany = null,
                Role = SystemUserRoleEnum.ConstructionCompanyAdmin
            };

            _constructionCompanyAdminService.CreateConstructionCompanyAdmin(constructionCompanyAdminToCreate);

            CreateConstructionCompanyAdminResponse constructionCompanyAdminResponse =
                new CreateConstructionCompanyAdminResponse
                {
                    Id = constructionCompanyAdminToCreate.Id
                };

            return constructionCompanyAdminResponse;
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (ObjectRepeatedServiceException)
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