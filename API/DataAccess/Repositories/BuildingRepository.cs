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

    public IEnumerable<Building> GetAllBuildings(Guid userId)
    {
        try
        {
            if (Guid.Empty == userId)
            {
                return _dbContext.Set<Building>()
                    .Include(building => building.Flats).ThenInclude(flat => flat.OwnerAssigned)
                    .Include(building => building.ConstructionCompany)
                    .Include(building => building.Manager)
                    .Include(building => building.Location).ToList();
            }
            else
            {
                return _dbContext.Set<Building>()
                    .Include(building => building.Flats).ThenInclude(flat => flat.OwnerAssigned)
                    .Include(building => building.ConstructionCompany)
                    .Include(building => building.Manager)
                    .Include(building => building.Location)
                    .Where(building => building.ConstructionCompany.UserCreatorId == userId).ToList();
            }
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
            return _dbContext.Set<Building>()
                .Include(building => building.Flats).ThenInclude(flat => flat.OwnerAssigned)
                .Include(building => building.ConstructionCompany)
                .Include(building => building.Manager)
                .Include(building => building.Location)
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
                .Include(b => b.Flats)
                .Include(b => b.ConstructionCompany)
                .Include(b => b.Manager)
                .Include(b => b.Location)
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
        try
        {
            _dbContext.Set<Building>().Remove(buildingToDelete);
            _dbContext.Entry(buildingToDelete).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    
    }
}