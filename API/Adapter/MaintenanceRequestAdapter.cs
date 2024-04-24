using Adapter.CustomExceptions;
using Domain;
using IAdapter;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
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
        try
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
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    public GetMaintenanceRequestResponse GetMaintenanceRequestById(Guid id)
    {
        try
        {
            MaintenanceRequest maintenanceRequestInDb = _maintenanceRequestService.GetMaintenanceRequestById(id);

            GetMaintenanceRequestResponse maintenanceRequestToReturn = new GetMaintenanceRequestResponse
            {
                Id = maintenanceRequestInDb.Id,
                Description = maintenanceRequestInDb.Description,
                BuildingId = maintenanceRequestInDb.BuildingId,
                RequestHandlerId = maintenanceRequestInDb.RequestHandlerId,
                Category = maintenanceRequestInDb.Category,
                RequestStatus = (StatusEnumMaintenanceResponse)maintenanceRequestInDb.RequestStatus,
                OpenedDate = maintenanceRequestInDb.OpenedDate,
                ClosedDate = maintenanceRequestInDb.ClosedDate,
                FlatId = maintenanceRequestInDb.FlatId
            };

            return maintenanceRequestToReturn;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException();
        }
    }
}