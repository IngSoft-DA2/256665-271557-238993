using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;

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
        return _context.Set<MaintenanceRequest>().ToList();
        
    }

    public IEnumerable<MaintenanceRequest> GetMaintenanceRequestByCategory(Guid categoryId)
    {
        throw new NotImplementedException();
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