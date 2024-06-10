using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class AdministratorService : IAdministratorService
{
    #region Constructor and Attributes
    
    private readonly IAdministratorRepository _administratorRepository;

    public AdministratorService(IAdministratorRepository administratorRepository)
    {
        _administratorRepository = administratorRepository;
    }
    
    #endregion
    
    #region Create Administrator

    public void CreateAdministrator(Administrator administratorToAdd)
    {
        try
        {
            administratorToAdd.AdministratorValidator();
            IEnumerable<Administrator> allAdministrators = _administratorRepository.GetAllAdministrators();
            CheckIfEmailAlreadyExists(administratorToAdd, allAdministrators);
            _administratorRepository.CreateAdministrator(administratorToAdd);
        }
        catch (InvalidAdministratorException exceptionCaught)
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

    private static void CheckIfEmailAlreadyExists(Administrator administratorToAdd, IEnumerable<Administrator> allAdministrators)
    {
        if (allAdministrators.Any(a => a.Email == administratorToAdd.Email))
        {
            throw new ObjectRepeatedServiceException();
        }
    }
    
    #endregion
}