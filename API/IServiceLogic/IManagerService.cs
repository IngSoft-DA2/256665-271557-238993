using Domain;

namespace IServiceLogic;

public interface IManagerService
{
    public IEnumerable<Manager> GetAllManagers();
}