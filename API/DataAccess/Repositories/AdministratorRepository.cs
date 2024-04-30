using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

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
        _dbContext.Set<Administrator>().Add(administratorToAdd);
        _dbContext.SaveChanges();
    }

    public IEnumerable<Administrator> GetAllAdministrators()
    {
        try
        {
            IEnumerable<Administrator> administratorsInDb = _dbContext.Set<Administrator>().ToList();
            return administratorsInDb;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }
}