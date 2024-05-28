using Domain;
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
            _constructionCompanyAdminRepository.CreateConstructionCompanyAdmin(constructionCompanyAdminToCreate);
        }
        catch (InvalidPersonException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
    }
}