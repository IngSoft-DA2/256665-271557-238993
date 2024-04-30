using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

namespace DataAccess.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly DbContext _dbContext;

    public ReportRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<MaintenanceRequest> GetMaintenanceReportByBuilding(Guid personId, Guid buildingId)
    {
        try
        {
            if (!(Guid.Empty == buildingId))
            {
                return _dbContext.Set<MaintenanceRequest>()
                    .Where(mr => mr.Flat.BuildingId == buildingId && mr.ManagerId == personId);
            }
            else
            {
                return _dbContext.Set<MaintenanceRequest>().Where(mr => mr.ManagerId == personId);
            }
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public IEnumerable<MaintenanceRequest> GetMaintenanceReportByRequestHandler(Guid requestHandlerId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MaintenanceRequest> GetMaintenanceReportByCategory(Guid buildingId, Guid categoryId)
    {
        throw new NotImplementedException();
    }
}