using Domain;
using Domain.Enums;

namespace IServiceLogic;

public interface IConstructionCompanyAdminService
{
    public void CreateConstructionCompanyAdminByInvitation(ConstructionCompanyAdmin constructionCompanyAdminToCreate, Guid? invitationId);
    
    public void CreateConstructionCompanyAdminForAdmins(ConstructionCompanyAdmin constructionCompanyAdminToCreate);
}