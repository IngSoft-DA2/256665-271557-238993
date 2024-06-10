using Adapter.CustomExceptions;
using Domain;
using Domain.Enums;
using IAdapter;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.AdministratorRequests;
using WebModel.Responses.AdministratorResponses;

namespace Adapter;

public class AdministratorAdapter : IAdministratorAdapter
{
    #region Constructor and Attributes

    private readonly IAdministratorService _administratorService;

    public AdministratorAdapter(IAdministratorService administratorService)
    {
        _administratorService = administratorService;
    }

    #endregion

    #region Create Administrator

    public CreateAdministratorResponse CreateAdministrator(CreateAdministratorRequest request)
    {
        try
        {
            Administrator administrator = new Administrator()
            {
                Id = Guid.NewGuid(),
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Email = request.Email,
                Password = request.Password,
                Role = SystemUserRoleEnum.Admin
            };

            _administratorService.CreateAdministrator(administrator);

            CreateAdministratorResponse adapterResponse = new CreateAdministratorResponse()
            {
                Id = administrator.Id
            };

            return adapterResponse;
        }
        catch (ObjectRepeatedServiceException)
        {
            throw new ObjectRepeatedAdapterException();
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    #endregion
}