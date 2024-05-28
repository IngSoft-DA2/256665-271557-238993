using Domain;

namespace IServiceLogic;

public interface IManagerService
{
    public IEnumerable<Manager> GetAllManagers();
    public void DeleteManagerById(Guid id);
    public void CreateManager(Manager manager, Invitation invitationToAccept); 
    public Manager GetManagerById(Guid managerId);
}