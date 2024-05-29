using Domain;

namespace IServiceLogic;

public interface IConstructionCompanyAdminService
{
    public void CreateConstructionCompanyAdmin(ConstructionCompanyAdmin constructionCompanyAdminToCreate, Guid invitationId);
    
}