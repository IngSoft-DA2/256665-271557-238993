using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class ConstructionCompanyAdminRepository: IConstructionCompanyAdminRepository
{

    private readonly DbContext _dbContext;

    public ConstructionCompanyAdminRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void CreateConstructionCompanyAdmin(ConstructionCompanyAdmin constructionCompanyAdminToAdd)
    {
        _dbContext.Set<ConstructionCompanyAdmin>().Add(constructionCompanyAdminToAdd);
        _dbContext.SaveChanges();
    }

    public IEnumerable<ConstructionCompanyAdmin> GetAllConstructionCompanyAdmins()
    {
        throw new NotImplementedException();
    }
}