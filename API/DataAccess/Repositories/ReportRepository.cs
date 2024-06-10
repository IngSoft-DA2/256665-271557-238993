using System.Collections;
using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

namespace DataAccess.Repositories;

public class ReportRepository : IReportRepository
{
    #region Constructor and Atributtes

    private readonly DbContext _dbContext;

    public ReportRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Get Maintenance Report By Building

    public IEnumerable<MaintenanceRequest> GetMaintenanceReportByBuilding(Guid personId, Guid buildingId)
    {
        try
        {
            if (!(Guid.Empty == buildingId))
            {
                return _dbContext.Set<MaintenanceRequest>()
                    .Where(mr => mr.Flat.BuildingId == buildingId && mr.ManagerId == personId)
                    .Include(mr => mr.Flat)
                    .Include(mr => mr.Manager)
                    .Include(mr => mr.RequestHandler)
                    .ToList();
            }
            else
            {
                return _dbContext.Set<MaintenanceRequest>()
                    .Where(mr => mr.ManagerId == personId)
                    .Include(mr => mr.Flat)
                    .Include(mr => mr.Manager)
                    .Include(mr => mr.RequestHandler).ToList();
            }
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    #endregion

    #region Get Maintenance Report By Request Handler

    public IEnumerable<MaintenanceRequest> GetMaintenanceReportByRequestHandler(Guid? requestHandlerId, Guid buildingId,
        Guid personId)
    {
        try
        {
            
            if (requestHandlerId != null && !(Guid.Empty == requestHandlerId))
            {
                return _dbContext.Set<MaintenanceRequest>()
                    .Where(mr =>
                        mr.Flat.BuildingId == buildingId && mr.RequestHandlerId == requestHandlerId &&
                        mr.ManagerId == personId)
                    .Include(mr => mr.Flat)
                    .Include(mr => mr.Manager)
                    .Include(mr => mr.RequestHandler).ToList();
            }
            else
            {
                if (buildingId == Guid.Empty)
                {
                    return _dbContext.Set<MaintenanceRequest>()
                        .Where(mr => mr.ManagerId == personId)
                        .Include(mr => mr.Flat)
                        .Include(mr => mr.Manager)
                        .Include(mr => mr.RequestHandler).ToList();
                }
                else
                {
                    return _dbContext.Set<MaintenanceRequest>()
                        .Where(mr => mr.ManagerId == personId && mr.Flat.BuildingId == buildingId)
                        .Include(mr => mr.Flat)
                        .Include(mr => mr.Manager)
                        .Include(mr => mr.RequestHandler).ToList();   
                }
            }
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    #endregion

    #region Get Maintenance Report By Category

    public IEnumerable<MaintenanceRequest> GetMaintenanceReportByCategory(Guid buildingId, Guid categoryId)
    {
        try
        {
            if (!(Guid.Empty == categoryId))
            {
                return _dbContext.Set<MaintenanceRequest>()
                    .Where(mr => mr.Flat.BuildingId == buildingId && mr.CategoryId == categoryId)
                    .Include(mr => mr.Flat)
                    .Include(mr => mr.Manager)
                    .Include(mr => mr.RequestHandler).ToList();
            }
            else
            {
                return _dbContext.Set<MaintenanceRequest>().Where(mr => mr.Flat.BuildingId == buildingId)
                    .Include(mr => mr.Flat)
                    .Include(mr => mr.Manager)
                    .Include(mr => mr.RequestHandler).ToList();
            }
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    #endregion

    #region Get Flat Requests Report By Building

    public IEnumerable<MaintenanceRequest> GetFlatRequestsReportByBuilding(Guid buildingId)
    {
        try
        {
            if (!(Guid.Empty == buildingId))
            {
                return _dbContext.Set<MaintenanceRequest>()
                    .Where(mr => mr.Flat.BuildingId == buildingId)
                    .Include(mr => mr.Flat).ThenInclude(mr => mr.OwnerAssigned)
                    .Include(mr => mr.Manager)
                    .Include(mr => mr.RequestHandler).ToList();
            }
            else
            {
                IEnumerable<MaintenanceRequest> response = new List<MaintenanceRequest>();
                return response;
            }
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    #endregion
}