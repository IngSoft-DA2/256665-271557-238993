using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

namespace DataAccess.Repositories;

public class ConstructionCompanyAdminRepository : IConstructionCompanyAdminRepository
{
    private readonly DbContext _dbContext;

    public ConstructionCompanyAdminRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateConstructionCompanyAdmin(ConstructionCompanyAdmin constructionCompanyAdminToAdd)
    {
        try
        {
            _dbContext.Set<ConstructionCompanyAdmin>().Add(constructionCompanyAdminToAdd);
            _dbContext.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public IEnumerable<ConstructionCompanyAdmin> GetAllConstructionCompanyAdmins()
    {
        throw new NotImplementedException();
    }
}