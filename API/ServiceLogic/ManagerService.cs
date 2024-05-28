using Domain;
using Domain.CustomExceptions;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class ManagerService :IManagerService
{
    #region Constructor and Atributes
    
    private readonly IManagerRepository _managerRepository;
    private readonly IInvitationService _invitationService;

    public ManagerService(IManagerRepository managerRepository, IInvitationService invitationService)
    {
        _managerRepository = managerRepository;
        _invitationService = invitationService;
    }
    
    #endregion
    
    #region Get All Managers

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
    
    #endregion

    #region Get Manager By Id
    
    public Manager GetManagerById(Guid managerId)
    {
        return _managerRepository.GetManagerById(managerId);
    }

    #endregion
    
    #region Create Manager

    public void CreateManager(Manager manager, Invitation invitationAccepted)
    {
        try
        {
            manager.ManagerValidator();
            CheckIfEmailIsAlreadyRegistered(manager);
            _invitationService.UpdateInvitation(invitationAccepted.Id, invitationAccepted);
            _managerRepository.CreateManager(manager);
        }
        catch (InvalidSystemUserException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (InvalidPersonException exceptionCaught)
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
    
    
    #endregion
    
    #region Delete Manager

    public void DeleteManagerById(Guid managerId)
    {
        try
        {
            Manager managerToDelete = _managerRepository.GetManagerById(managerId);
            
            if (managerToDelete is null) throw new ObjectNotFoundServiceException();
            _managerRepository.DeleteManager(managerToDelete);
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundServiceException();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }
    
    #endregion
}