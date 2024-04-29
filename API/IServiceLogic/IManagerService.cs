using Domain;

namespace IServiceLogic;

public interface IManagerService
{
    public IEnumerable<Manager> GetAllManagers();
    public void DeleteManagerById(Guid id);
    public void CreateManager(Manager manager);
}