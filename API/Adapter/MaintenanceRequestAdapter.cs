using Adapter.CustomExceptions;
using Domain;
using Domain.Enums;
using IAdapter;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.MaintenanceRequests;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.MaintenanceResponses;
using WebModel.Responses.OwnerResponses;
using WebModel.Responses.RequestHandlerResponses;

namespace Adapter;

public class MaintenanceRequestAdapter : IMaintenanceRequestAdapter
{
    #region Constructor and attributes

    private readonly IMaintenanceRequestService _maintenanceRequestService;

    public MaintenanceRequestAdapter(IMaintenanceRequestService maintenanceRequestService)
    {
        _maintenanceRequestService = maintenanceRequestService;
    }

    #endregion

    #region Get All Maintenance Requests

public IEnumerable<GetMaintenanceRequestResponse> GetAllMaintenanceRequests(Guid? managerId, Guid categoryId)
{
    try
    {
        IEnumerable<MaintenanceRequest> maintenanceRequestsInDb =
            _maintenanceRequestService.GetAllMaintenanceRequests(managerId, categoryId);

        IEnumerable<GetMaintenanceRequestResponse> maintenanceRequestsToReturn =
            maintenanceRequestsInDb.Select(maintenanceRequest => new GetMaintenanceRequestResponse
            {
                Id = maintenanceRequest.Id,
                Description = maintenanceRequest.Description,
                RequestHandlerId = maintenanceRequest.RequestHandlerId,
                Category = maintenanceRequest.CategoryId,
                RequestStatus = (StatusEnumMaintenanceResponse)maintenanceRequest.RequestStatus,
                OpenedDate = maintenanceRequest.OpenedDate,
                ClosedDate = maintenanceRequest.ClosedDate,
                RequestHandler = maintenanceRequest.RequestHandler != null ? new GetRequestHandlerResponse
                {
                    Id = maintenanceRequest.RequestHandler.Id,
                    Name = maintenanceRequest.RequestHandler.Firstname,
                    LastName = maintenanceRequest.RequestHandler.LastName,
                    Email = maintenanceRequest.RequestHandler.Email,
                } : null,
                FlatId = maintenanceRequest.FlatId,
                Flat = maintenanceRequest.Flat != null ? new GetFlatResponse
                {
                    Id = maintenanceRequest.Flat.Id,
                    Floor = maintenanceRequest.Flat.Floor,
                    TotalRooms = maintenanceRequest.Flat.TotalRooms,
                    TotalBaths = maintenanceRequest.Flat.TotalBaths,
                    HasTerrace = maintenanceRequest.Flat.HasTerrace,
                    RoomNumber = maintenanceRequest.Flat.RoomNumber,
                    OwnerAssigned = maintenanceRequest.Flat.OwnerAssigned != null ? new GetOwnerResponse
                    {
                        Id = maintenanceRequest.Flat.OwnerAssigned.Id,
                        Firstname = maintenanceRequest.Flat.OwnerAssigned.Firstname,
                        Lastname = maintenanceRequest.Flat.OwnerAssigned.Lastname,
                        Email = maintenanceRequest.Flat.OwnerAssigned.Email,
                    } : null,
                } : null
            });

        return maintenanceRequestsToReturn;
    }
    catch (Exception exceptionCaught)
    {
        throw new Exception(exceptionCaught.Message);
    }
}


    #endregion

    #region Get Maintenance Request By Category Id

    public IEnumerable<GetMaintenanceRequestResponse> GetMaintenanceRequestByCategory(Guid id)
    {
        try
        {
            IEnumerable<MaintenanceRequest> maintenanceRequestsInDb = _maintenanceRequestService.GetMaintenanceRequestByCategory(id);
            
            IEnumerable<GetMaintenanceRequestResponse> maintenanceRequestToReturn = maintenanceRequestsInDb
                .Select(maintenanceRequest => new GetMaintenanceRequestResponse
                {
                    Id = maintenanceRequest.Id,
                    Description = maintenanceRequest.Description,
                    RequestHandlerId = maintenanceRequest.RequestHandlerId,
                    Category = maintenanceRequest.CategoryId,
                    RequestStatus = (StatusEnumMaintenanceResponse)maintenanceRequest.RequestStatus,
                    OpenedDate = maintenanceRequest.OpenedDate,
                    ClosedDate = maintenanceRequest.ClosedDate,
                    FlatId = maintenanceRequest.FlatId
                });

            return maintenanceRequestToReturn;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Maintenance request was not found");
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    #endregion

    #region Create Maintenance Request

    public CreateRequestMaintenanceResponse CreateMaintenanceRequest(
        CreateRequestMaintenanceRequest maintenanceRequestToCreate)
    {
        try
        {
            MaintenanceRequest maintenanceRequest = new MaintenanceRequest
            {
                Id = Guid.NewGuid(),
                OpenedDate = DateTime.Now,
                RequestStatus = RequestStatusEnum.Open,
                Description = maintenanceRequestToCreate.Description,
                FlatId = maintenanceRequestToCreate.FlatId,
                CategoryId = maintenanceRequestToCreate.Category,
                ManagerId = maintenanceRequestToCreate.ManagerId
            };

            _maintenanceRequestService.CreateMaintenanceRequest(maintenanceRequest);

            CreateRequestMaintenanceResponse maintenanceRequestResponse = new CreateRequestMaintenanceResponse
                { Id = maintenanceRequest.Id };

            return maintenanceRequestResponse;
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    #endregion

    #region Update Maintenance Request

    public void UpdateMaintenanceRequestStatus(Guid id, UpdateMaintenanceRequestStatusRequest maintenanceRequestToUpdate)
    {
        try
        {
            MaintenanceRequest maintenanceRequestWithUpdates = new MaintenanceRequest
            {
                Id = id,
                RequestStatus = (RequestStatusEnum)maintenanceRequestToUpdate.RequestStatus
            };

            _maintenanceRequestService.UpdateMaintenanceRequest(id, maintenanceRequestWithUpdates);
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Maintenance request was not found");
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    #endregion

    #region Assign Maintenance Request

    public void AssignMaintenanceRequest(Guid idToUpdate, Guid idOfWorker)
    {
        try
        {
            _maintenanceRequestService.AssignMaintenanceRequest(idToUpdate, idOfWorker);
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Maintenance request was not found");
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    #endregion

    #region Get Maintenance Requests By Request Handler

    public IEnumerable<GetMaintenanceRequestResponse> GetMaintenanceRequestsByRequestHandler(Guid? requestHandlerId)
    {
        try
        {
            IEnumerable<GetMaintenanceRequestResponse> maintenanceRequestFromHandler = _maintenanceRequestService
                .GetMaintenanceRequestsByRequestHandler(requestHandlerId)
                .Select(maintenanceRequest => new GetMaintenanceRequestResponse
                {
                    Id = maintenanceRequest.Id,
                    Description = maintenanceRequest.Description,
                    RequestHandlerId = maintenanceRequest.RequestHandlerId,
                    Category = maintenanceRequest.CategoryId,
                    RequestStatus = (StatusEnumMaintenanceResponse)maintenanceRequest.RequestStatus,
                    OpenedDate = maintenanceRequest.OpenedDate,
                    ClosedDate = maintenanceRequest.ClosedDate,
                    RequestHandler = maintenanceRequest.RequestHandler != null ? new GetRequestHandlerResponse
                    {
                        Id = maintenanceRequest.RequestHandler.Id,
                        Name = maintenanceRequest.RequestHandler.Firstname,
                        LastName = maintenanceRequest.RequestHandler.LastName,
                        Email = maintenanceRequest.RequestHandler.Email,
                    } : null,
                    FlatId = maintenanceRequest.FlatId,
                    Flat = maintenanceRequest.Flat != null ? new GetFlatResponse
                    {
                        Id = maintenanceRequest.Flat.Id,
                        Floor = maintenanceRequest.Flat.Floor,
                        TotalRooms = maintenanceRequest.Flat.TotalRooms,
                        TotalBaths = maintenanceRequest.Flat.TotalBaths,
                        HasTerrace = maintenanceRequest.Flat.HasTerrace,
                        RoomNumber = maintenanceRequest.Flat.RoomNumber,
                        OwnerAssigned = maintenanceRequest.Flat.OwnerAssigned != null ? new GetOwnerResponse
                        {
                            Id = maintenanceRequest.Flat.OwnerAssigned.Id,
                            Firstname = maintenanceRequest.Flat.OwnerAssigned.Firstname,
                            Lastname = maintenanceRequest.Flat.OwnerAssigned.Lastname,
                            Email = maintenanceRequest.Flat.OwnerAssigned.Email
                        } : null,
                    } : null
                });

            return maintenanceRequestFromHandler;
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    #endregion

    #region Get Maintenance Request By Id
    public GetMaintenanceRequestResponse GetMaintenanceRequestById(Guid id)
    {
        try
        {
            MaintenanceRequest maintenanceRequestInDb = _maintenanceRequestService.GetMaintenanceRequestById(id);

            GetMaintenanceRequestResponse maintenanceRequestToReturn = new GetMaintenanceRequestResponse
            {
                Id = maintenanceRequestInDb.Id,
                Description = maintenanceRequestInDb.Description,
                RequestHandlerId = maintenanceRequestInDb.RequestHandlerId,
                Category = maintenanceRequestInDb.CategoryId,
                RequestStatus = (StatusEnumMaintenanceResponse)maintenanceRequestInDb.RequestStatus,
                OpenedDate = maintenanceRequestInDb.OpenedDate,
                ClosedDate = maintenanceRequestInDb.ClosedDate,
                FlatId = maintenanceRequestInDb.FlatId
            };

            return maintenanceRequestToReturn;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Maintenance request was not found");
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }
    
    #endregion
}