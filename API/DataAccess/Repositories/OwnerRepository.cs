using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly DbContext _dbContext;

    public OwnerRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public IEnumerable<Owner> GetAllOwners()
    {
        return _dbContext.Set<Owner>().Include(owner => owner.Flats).ToList();
    }

    public Owner GetOwnerById(Guid ownerIdToObtain)
    {
        throw new NotImplementedException();
    }

    public void CreateOwner(Owner ownerToCreate)
    {
        throw new NotImplementedException();
    }

    public void UpdateOwnerById(Owner ownerWithUpdates)
    {
        throw new NotImplementedException();
    }
}