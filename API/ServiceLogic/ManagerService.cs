using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class ManagerService :IManagerService
{
    private readonly IManagerRepository _managerRepository;

    public ManagerService(IManagerRepository managerRepository)
    {
        _managerRepository = managerRepository;
    }

    public IEnumerable<Manager> GetAllManagers()
    {
        try
        {
            return _managerRepository.GetAllManagers();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    public void CreateManager(Manager manager)
    {
        try
        {
            manager.ManagerValidator();
            CheckIfEmailIsAlreadyRegistered(manager);

            _managerRepository.CreateManager(manager);
        }
        catch (InvalidPersonException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (InvalidManagerException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (ObjectRepeatedServiceException)
        {
            throw new ObjectRepeatedServiceException();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    private void CheckIfEmailIsAlreadyRegistered(Manager manager)
    {
        IEnumerable<Manager> managers = _managerRepository.GetAllManagers();
        if (managers.Any(m => m.Email == manager.Email))
        {
            throw new ObjectRepeatedServiceException();
        }
    }

    public void DeleteManagerById(Guid managerId)
    {
        _managerRepository.DeleteManagerById(managerId);
    }
}