using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;

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
        return _dbContext.Set<Building>().Where(building => building.ManagerId == managerId).ToList();
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