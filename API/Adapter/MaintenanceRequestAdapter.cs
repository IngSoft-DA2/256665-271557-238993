using Adapter.CustomExceptions;
using Domain;
using IAdapter;
using IServiceLogic;
using WebModel.Responses.MaintenanceResponses;

namespace Adapter;

public class MaintenanceRequestAdapter
{
    private readonly IMaintenanceRequestService _maintenanceRequestService;

    public MaintenanceRequestAdapter(IMaintenanceRequestService maintenanceRequestService)
    {
        _maintenanceRequestService = maintenanceRequestService;
    }

    public IEnumerable<GetMaintenanceRequestResponse> GetAllMaintenanceRequests()
    {
        IEnumerable<MaintenanceRequest> maintenanceRequestsInDb =
            _maintenanceRequestService.GetAllMaintenanceRequests();

        IEnumerable<GetMaintenanceRequestResponse> maintenanceRequestsToReturn =
            maintenanceRequestsInDb.Select(maintenanceRequest => new GetMaintenanceRequestResponse
            {
                Id = maintenanceRequest.Id,
                Description = maintenanceRequest.Description,
                BuildingId = maintenanceRequest.BuildingId,
                RequestHandlerId = maintenanceRequest.RequestHandlerId,
                Category = maintenanceRequest.Category,
                RequestStatus = (StatusEnumMaintenanceResponse)maintenanceRequest.RequestStatus,
                OpenedDate = maintenanceRequest.OpenedDate,
                ClosedDate = maintenanceRequest.ClosedDate,
                FlatId = maintenanceRequest.FlatId
            });
        return maintenanceRequestsToReturn;
    }
}