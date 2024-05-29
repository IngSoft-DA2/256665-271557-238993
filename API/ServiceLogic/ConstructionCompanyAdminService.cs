using Domain;
using Domain.CustomExceptions;
using IDataAccess;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class ConstructionCompanyAdminService : IConstructionCompanyAdminService
{
    #region Constructor and Dependency Injection

    private readonly IConstructionCompanyAdminRepository _constructionCompanyAdminRepository;

    public ConstructionCompanyAdminService(IConstructionCompanyAdminRepository constructionCompanyAdminRepository)
    {
        _constructionCompanyAdminRepository = constructionCompanyAdminRepository;
    }

    #endregion

    #region Create Construction Company Admin

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

    #endregion
    
    #region Get All Construction Company Admins
    
    public IEnumerable<ConstructionCompanyAdmin> GetAllConstructionCompanyAdmins()
    {
        return _constructionCompanyAdminRepository.GetAllConstructionCompanyAdmins();
    }
    
    #endregion
    
}