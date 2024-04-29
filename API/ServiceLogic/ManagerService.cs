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
    }

    private void CheckIfEmailIsAlreadyRegistered(Manager manager)
    {
        IEnumerable<Manager> managers = _managerRepository.GetAllManagers();
        if (managers.Any(m => m.Email == manager.Email))
        {
            throw new ObjectRepeatedServiceException();
        }
    }
}