using Domain;
using Domain.CustomExceptions;
using IDataAccess;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class ConstructionCompanyAdminService : IConstructionCompanyAdminService
{
    private readonly IConstructionCompanyAdminRepository _constructionCompanyAdminRepository;

    public ConstructionCompanyAdminService(IConstructionCompanyAdminRepository constructionCompanyAdminRepository)
    {
        _constructionCompanyAdminRepository = constructionCompanyAdminRepository;
    }

    public void CreateConstructionCompanyAdmin(ConstructionCompanyAdmin constructionCompanyAdminToCreate)
    {
        try
        {
            constructionCompanyAdminToCreate.ConstructionCompanyAdminValidator();

            IEnumerable<ConstructionCompanyAdmin> allConstructionCompanyAdmins =
                _constructionCompanyAdminRepository.GetAllConstructionCompanyAdmins();

            CheckIfEmailAlreadyExists(constructionCompanyAdminToCreate, allConstructionCompanyAdmins);
            _constructionCompanyAdminRepository.CreateConstructionCompanyAdmin(constructionCompanyAdminToCreate);
        }
        catch (InvalidPersonException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (InvalidConstructionCompanyAdminException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (ObjectRepeatedServiceException)
        {
            throw new ObjectRepeatedServiceException();
        }
    }

    private static void CheckIfEmailAlreadyExists(ConstructionCompanyAdmin constructionCompanyAdminToCheck,
        IEnumerable<ConstructionCompanyAdmin> allConstructionCompanyAdmins)
    {
        if (allConstructionCompanyAdmins.Any(a => a.Email == constructionCompanyAdminToCheck.Email))
        {
            throw new ObjectRepeatedServiceException();
        }
    }
}