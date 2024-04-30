using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class AdministratorRepository : IAdministratorRepository
{
    private readonly DbContext _dbContext;

    public AdministratorRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateAdministrator(Administrator administratorToAdd)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Administrator> GetAllAdministrators()
    {
        IEnumerable<Administrator> administratorsInDb = _dbContext.Set<Administrator>().ToList();
        return administratorsInDb;
    }
}