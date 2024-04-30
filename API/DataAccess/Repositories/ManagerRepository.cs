using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

namespace DataAccess.Repositories;

public class ManagerRepository : IManagerRepository
{
    private readonly DbContext _dbContext;

    public ManagerRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Manager> GetAllManagers()
    {
        try
        {
            return _dbContext.Set<Manager>().Include(manager => manager.Buildings).ToList();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public Manager GetManagerById(Guid managerId)
    {
        try
        {
            return _dbContext.Set<Manager>().Find(managerId);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public void CreateManager(Manager manager)
    {
        try
        {
            _dbContext.Set<Manager>().Add(manager);
            _dbContext.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public void DeleteManager(Manager managerToDelete)
    {
        _dbContext.Set<Manager>().Remove(managerToDelete);
        _dbContext.Entry(managerToDelete).State = EntityState.Deleted;
        _dbContext.SaveChanges();
    }
}