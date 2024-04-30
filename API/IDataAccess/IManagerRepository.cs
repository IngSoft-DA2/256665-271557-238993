using Domain;

namespace IRepository;

public interface IManagerRepository
{
    public IEnumerable<Manager> GetAllManagers();
    public void CreateManager(Manager manager);
    public void DeleteManagerById(Guid managerId);
    public Manager GetManagerById(Guid managerId);

}