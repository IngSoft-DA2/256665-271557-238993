using Domain;
using IRepository;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class ManagerService
{
    private readonly IManagerRepository _managerRepository;

    public ManagerService(IManagerRepository managerRepository)
    {
        _managerRepository = managerRepository;
    }

    public IEnumerable<Manager> GetAllManagers()
    {
        return _managerRepository.GetAllManagers();
    }
}