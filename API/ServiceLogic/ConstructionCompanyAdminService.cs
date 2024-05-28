using Domain;
using IDataAccess;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class ConstructionCompanyAdminService : IConstructionCompanyAdminService
{
    private readonly IConstructionCompanyAdminRepository _constructionCompanyAdminRepository;

    public ConstructionCompanyAdminService(IConstructionCompanyAdminRepository constructionCompanyAdminRepository )
    {
        _constructionCompanyAdminRepository = constructionCompanyAdminRepository;
    }
    public void CreateConstructionCompanyAdmin(ConstructionCompanyAdmin constructionCompanyAdminToCreate)
    {
        throw new NotImplementedException();
    }
}