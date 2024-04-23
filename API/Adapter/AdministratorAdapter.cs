using Adapter.CustomExceptions;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.AdministratorResponses;

namespace Adapter;

public class AdministratorAdapter
{
    #region Constructor and Attributes
    
    private readonly IAdministratorService _administratorAdapter;
    
    public AdministratorAdapter(IAdministratorService administratorAdapter)
    {
        _administratorAdapter = administratorAdapter;
    }
    
    #endregion
    
    #region Create Administrator
    
    public CreateAdministratorResponse CreateAdministrator(Guid administratorId)
    {
        try
        {
            _administratorAdapter.CreateAdministrator(administratorId);
            CreateAdministratorResponse adapterResponse = new CreateAdministratorResponse()
            {
                Id = administratorId
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