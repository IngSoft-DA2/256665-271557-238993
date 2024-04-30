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
            return _dbContext.Set<Building>().Include(building => building.Flats).Where(building => building.ManagerId == managerId).ToList();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public Building GetBuildingById(Guid buildingId)
    {
        throw new NotImplementedException();
    }

    public void CreateBuilding(Building building)
    {
        throw new NotImplementedException();
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