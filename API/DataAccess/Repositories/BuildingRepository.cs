using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

namespace DataAccess.Repositories;

public class BuildingRepository : IBuildingRepository
{
    private readonly DbContext _dbContext;

    public BuildingRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Building> GetAllBuildings(Guid managerId)
    {
        try
        {
            return _dbContext.Set<Building>().Include(building => building.Flats)
                .Where(building => building.ManagerId == managerId).ToList();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public Building GetBuildingById(Guid buildingId)
    {
        try
        {
            return _dbContext.Set<Building>().Include(building => building.Flats)
                .Where(building => building.Id == buildingId).FirstOrDefault();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public void CreateBuilding(Building building)
    {
        try
        {
            _dbContext.Set<Building>().Add(building);
            _dbContext.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public void UpdateBuilding(Building buildingWithUpdates)
    {
        try
        {
            var buildingInDb = _dbContext.Set<Building>()
                .Include(b => b.Flats).Include(b => b.ConstructionCompany)
                .FirstOrDefault(b => b.Id == buildingWithUpdates.Id);

            _dbContext.Entry(buildingInDb).CurrentValues.SetValues(buildingWithUpdates);

            _dbContext.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public void DeleteBuilding(Building buildingToDelete)
    {
        _dbContext.Set<Building>().Remove(buildingToDelete);
        _dbContext.Entry(buildingToDelete).State = EntityState.Deleted;
        _dbContext.SaveChanges();
    }
}