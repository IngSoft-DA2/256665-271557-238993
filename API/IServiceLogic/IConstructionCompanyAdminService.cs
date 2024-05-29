using Domain;

namespace IServiceLogic;

public interface IConstructionCompanyAdminService
{
    public void CreateConstructionCompanyAdminByInvitation(ConstructionCompanyAdmin constructionCompanyAdminToCreate, Guid invitationId);
    public void CreateConstructionCompanyAdminByAnotherAdmin(ConstructionCompanyAdmin constructionCompanyAdminToCreate);
    
}