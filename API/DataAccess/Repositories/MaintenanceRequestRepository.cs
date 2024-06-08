using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

namespace DataAccess.Repositories;

public class MaintenanceRequestRepository : IMaintenanceRequestRepository
{
    #region Constructor and attributes

    private readonly DbContext _context;

    public MaintenanceRequestRepository(DbContext context)
    {
        _context = context;
    }

    #endregion

    #region Get All Maintenance Requests

    public IEnumerable<MaintenanceRequest> GetAllMaintenanceRequests(Guid? managerId)
    {
        try
        {
            if (managerId != null)
            {
                return _context.Set<MaintenanceRequest>()
                    .Where(maintenanceRequest => maintenanceRequest.ManagerId == managerId)
                    .Include(maintenanceRequest => maintenanceRequest.Category)
                    .Include(maintenanceRequest => maintenanceRequest.Flat).ThenInclude(flat => flat.OwnerAssigned)
                    .Include(maintenanceRequest => maintenanceRequest.RequestHandler)
                    .ToList();
            }
            else
            {
                return _context.Set<MaintenanceRequest>()
                    .Where(maintenanceRequest => maintenanceRequest.ManagerId == Guid.Empty)
                    .Include(maintenanceRequest => maintenanceRequest.Category)
                    .Include(maintenanceRequest => maintenanceRequest.Flat).ThenInclude(flat => flat.OwnerAssigned)
                    .Include(maintenanceRequest => maintenanceRequest.RequestHandler)
                    .ToList();
            }
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    #endregion

    #region Get Maintenance Request By Category

    public IEnumerable<MaintenanceRequest> GetMaintenanceRequestByCategory(Guid categoryId)
    {
        try
        {
            return _context.Set<MaintenanceRequest>()
                .Where(maintenanceRequest => maintenanceRequest.CategoryId == categoryId).ToList();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    #endregion

    #region Create Maintenance Request

    public void CreateMaintenanceRequest(MaintenanceRequest requestToCreate)
    {
        try
        {
            _context.Set<MaintenanceRequest>().Add(requestToCreate);
            _context.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    #endregion  

    #region Update Maintenance Request

    public void UpdateMaintenanceRequest(Guid isAny, MaintenanceRequest maintenanceRequestWithUpdates)
    {
        try
        {
            MaintenanceRequest maintenanceRequestOnDb = _context.Set<MaintenanceRequest>().Find(maintenanceRequestWithUpdates.Id);
            if(maintenanceRequestOnDb != null)
            {
                _context.Set<MaintenanceRequest>().Entry(maintenanceRequestOnDb).CurrentValues.SetValues(maintenanceRequestWithUpdates);
                _context.SaveChanges();
            }
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    #endregion

    #region Get Maintenance Request By Id

    public MaintenanceRequest GetMaintenanceRequestById(Guid idToUpdate)
    {
        try
        {
            return _context.Set<MaintenanceRequest>().Find(idToUpdate);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    #endregion

    #region Get Maintenance Requests By Request Handler

    public IEnumerable<MaintenanceRequest> GetMaintenanceRequestsByRequestHandler(Guid? requestHandlerId)
    {
        try
        {
            return _context.Set<MaintenanceRequest>()
                .Where(maintenanceRequest => maintenanceRequest.RequestHandlerId == requestHandlerId).ToList();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    #endregion
}