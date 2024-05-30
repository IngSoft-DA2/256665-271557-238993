using Domain;

namespace IDataAccess;

public interface IConstructionCompanyAdminRepository
{
    public void CreateConstructionCompanyAdmin(ConstructionCompanyAdmin constructionCompanyAdminToAdd);
    public IEnumerable<ConstructionCompanyAdmin> GetAllConstructionCompanyAdmins();
}