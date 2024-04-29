using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class AdministratorService : IAdministratorService
{
    private readonly IAdministratorRepository _administratorRepository;

    public AdministratorService(IAdministratorRepository administratorRepository)
    {
        _administratorRepository = administratorRepository;
    }

    public void CreateAdministrator(Administrator administratorToAdd)
    {
        try
        {
            administratorToAdd.PersonValidator();
            administratorToAdd.PasswordValidator();
            IEnumerable<Administrator> allAdministrators = _administratorRepository.GetAllAdministrators();
            if(allAdministrators.Any(a => a.Email == administratorToAdd.Email))
            {
                throw new ObjectRepeatedServiceException();
            }
            _administratorRepository.CreateAdministrator(administratorToAdd);
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
}