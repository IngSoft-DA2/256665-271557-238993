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
        _dbContext.Set<Building>().Add(building);
        _dbContext.SaveChanges();
    }

    public void UpdateBuilding(Building building)
    {
        throw new NotImplementedException();
    }

    public void DeleteBuilding(Building buildingId)
    {
        throw new NotImplementedException();
    }
}