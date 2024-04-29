using Domain;
using IRepository;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class AdministratorService : IAdministratorRepository
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
            
            if (string.IsNullOrEmpty(administratorToAdd.Password) || administratorToAdd.Password.Length < 8)
            {
                throw new InvalidManagerException("Password must have at least 8 characters");
            }
            _administratorRepository.CreateAdministrator(administratorToAdd);
        }
        catch (InvalidPersonException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch(InvalidManagerException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
    }
}