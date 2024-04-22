using Adapter.CustomExceptions;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.AdministratorResponses;

namespace Adapter;

public class AdministratorAdapter
{
    private readonly IAdministratorService _administratorAdapter;
    
    public AdministratorAdapter(IAdministratorService administratorAdapter)
    {
        _administratorAdapter = administratorAdapter;
    }
    
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
        catch (ObjectRepeatedServiceException exceptionCaught)
        {
            throw new ObjectRepeatedAdapterException(exceptionCaught.Message);
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        
    }
}