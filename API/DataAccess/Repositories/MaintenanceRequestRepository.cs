using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

namespace DataAccess.Repositories;

public class MaintenanceRequestRepository : IMaintenanceRequestRepository
{
    private readonly DbContext _context;
    
    public MaintenanceRequestRepository(DbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<MaintenanceRequest> GetAllMaintenanceRequests()
    {
        try
        {
            return _context.Set<MaintenanceRequest>().ToList();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }

    }

    public IEnumerable<MaintenanceRequest> GetMaintenanceRequestByCategory(Guid categoryId)
    {
        try
        {
            return _context.Set<MaintenanceRequest>()
                .Where(maintenanceRequest => maintenanceRequest.Category == categoryId).ToList();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public void CreateMaintenanceRequest(MaintenanceRequest requestToCreate)
    {
        throw new NotImplementedException();
    }

    public void UpdateMaintenanceRequest(Guid isAny, MaintenanceRequest maintenanceRequestSample)
    {
        throw new NotImplementedException();
    }

    public MaintenanceRequest GetMaintenanceRequestById(Guid idToUpdate)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MaintenanceRequest> GetMaintenanceRequestsByRequestHandler(Guid requestHandlerId)
    {
        throw new NotImplementedException();
    }
}