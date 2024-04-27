using WebModel.Requests.MaintenanceRequests;
using WebModel.Responses.MaintenanceResponses;

namespace IAdapter;

public interface IMaintenanceAdapter
{
    public IEnumerable<GetMaintenanceRequestResponse> GetAllMaintenanceRequests();
    public GetMaintenanceRequestResponse GetMaintenanceRequestByCategory(Guid categoryId);
    public CreateRequestMaintenanceResponse CreateMaintenanceRequest(CreateRequestMaintenanceRequest requestToCreate);
    public void AssignMaintenanceRequest(Guid idToUpdate, Guid idOfWorker);
    public IEnumerable<GetMaintenanceRequestResponse> GetMaintenanceRequestsByRequestHandler(Guid requestHandlerId);
    public void UpdateMaintenanceRequestStatus(Guid isAny, UpdateMaintenanceRequestStatusRequest updateMaintenanceRequestStatusRequest);
    public GetMaintenanceRequestResponse GetMaintenanceRequestById(Guid idFromRoute);
}