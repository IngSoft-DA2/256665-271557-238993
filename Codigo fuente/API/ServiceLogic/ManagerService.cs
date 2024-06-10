using Domain;
using Domain.CustomExceptions;
using Domain.Enums;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class ManagerService : IManagerService
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
        try
        {
            Manager managerFound = _managerRepository.GetManagerById(managerId);

            if (managerFound is null)
            {
                throw new ObjectNotFoundServiceException();
            }

            return managerFound;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    #endregion

    #region Create Manager

    public void CreateManager(Manager manager, Invitation invitationToAccept)
    {
        try
        {
            manager.ManagerValidator();
            CheckIfEmailIsAlreadyRegistered(manager);
            CheckIfEmailIsTheSameAsInvitationEmail(manager, invitationToAccept);
            Invitation invitationAccepted = AcceptInvitation(manager, invitationToAccept);

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
        catch (ObjectErrorServiceException exceptionCaught)
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

    private void CheckIfEmailIsTheSameAsInvitationEmail(Manager manager, Invitation invitationToAccept)
    {
        if (manager.Email != invitationToAccept.Email)
        {
            throw new ObjectErrorServiceException("Email is not the same as the invitation email");
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
    
    private Invitation AcceptInvitation(Manager manager, Invitation invitationToAccept)
    {
        Invitation invitationAccepted = new Invitation
        {
            Id = invitationToAccept.Id,
            Status = StatusEnum.Accepted,
            ExpirationDate = invitationToAccept.ExpirationDate
        };
        manager.Firstname = invitationToAccept.Firstname;

        return invitationAccepted;
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