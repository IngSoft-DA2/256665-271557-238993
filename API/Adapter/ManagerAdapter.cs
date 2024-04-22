using Domain;
using IServiceLogic;
using WebModel.Responses.ManagerResponses;

namespace Adapter;

public class ManagerAdapter
{
    private readonly IManagerService _managerServiceLogic;

    public ManagerAdapter(IManagerService managerServiceLogic)
    {
        _managerServiceLogic = managerServiceLogic;
    }

    public IEnumerable<GetManagerResponse> GetAllManagers()
    {
        IEnumerable<Manager> serviceResponse = _managerServiceLogic.GetAllManagers();

        IEnumerable<GetManagerResponse> adapterResponse = serviceResponse.Select(manager =>
            new GetManagerResponse
            {
                Id = manager.Id,
                Email = manager.Email,
                Name = manager.Name,
            });

        return adapterResponse;
    }
}