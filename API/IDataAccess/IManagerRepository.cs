using Domain;

namespace IRepository;

public interface IManagerRepository
{
    public IEnumerable<Manager> GetAllManagers();
    public void CreateManager(Manager manager);
    public void DeleteManager(Manager managerToDelete);
    public Manager GetManagerById(Guid managerId);

}