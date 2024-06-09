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
    private readonly IInvitationService _invitationService;

    public ConstructionCompanyAdminAdapter(IConstructionCompanyAdminService constructionCompanyAdminService)
    {
        _constructionCompanyAdminService = constructionCompanyAdminService;
    }

    #endregion

    #region Create Construction Company Admin

    public CreateConstructionCompanyAdminResponse CreateConstructionCompanyAdmin(
        CreateConstructionCompanyAdminRequest createRequest, SystemUserRoleEnum? userRole)
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

            if (userRole is null)
            {
                _constructionCompanyAdminService.CreateConstructionCompanyAdminByInvitation(
                    constructionCompanyAdminToCreate, createRequest.InvitationId);
            }
            else _constructionCompanyAdminService.CreateConstructionCompanyAdminForAdmins(constructionCompanyAdminToCreate);
            
            CreateConstructionCompanyAdminResponse constructionCompanyAdminResponse = new CreateConstructionCompanyAdminResponse
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
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Construction company admin was not found");
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }

    #endregion
}