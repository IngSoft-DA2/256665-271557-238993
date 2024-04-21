using WebModel.Responses.MaintenanceResponses;

namespace IAdapter;

public interface IMaintenanceAdapter
{
    public IEnumerable<GetMaintenanceRequestResponse> GetAllMaintenanceRequests();
    
}