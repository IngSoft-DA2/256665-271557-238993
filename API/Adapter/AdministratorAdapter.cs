using IServiceLogic;
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
        _administratorAdapter.CreateAdministrator(administratorId);
        CreateAdministratorResponse adapterResponse = new CreateAdministratorResponse()
        {
            Id = administratorId
        };
        
        return adapterResponse;
    }
}