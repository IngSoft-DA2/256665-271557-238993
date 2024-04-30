using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;

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
        return _dbContext.Set<Manager>().Include(manager => manager.Buildings).ToList();
    }

    public Manager GetManagerById(Guid managerId)
    {
        return _dbContext.Set<Manager>().Find(managerId);
    }

    public void CreateManager(Manager manager)
    {
        throw new NotImplementedException();
    }

    public void DeleteManagerById(Guid id)
    {
        throw new NotImplementedException();
    }
}