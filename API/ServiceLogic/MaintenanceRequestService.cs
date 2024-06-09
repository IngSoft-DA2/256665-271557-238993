using System.Reflection;
using Domain;
using Domain.Enums;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class MaintenanceRequestService : IMaintenanceRequestService
{
    #region Constructor and Dependency Injection

    private readonly IMaintenanceRequestRepository _maintenanceRequestRepository;

    public MaintenanceRequestService(IMaintenanceRequestRepository maintenanceRequestRepository)
    {
        _maintenanceRequestRepository = maintenanceRequestRepository;
    }

    #endregion

    public IEnumerable<MaintenanceRequest> GetAllMaintenanceRequests(Guid? managerId, Guid categoryId)
    {
        try
        {
            IEnumerable<MaintenanceRequest> maintenanceRequests =
                _maintenanceRequestRepository.GetAllMaintenanceRequests(managerId, categoryId);
            return maintenanceRequests;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    public IEnumerable<MaintenanceRequest> GetMaintenanceRequestByCategory(Guid id)
    {
        IEnumerable<MaintenanceRequest> maintenanceRequest;
        try
        {
            maintenanceRequest = _maintenanceRequestRepository.GetMaintenanceRequestByCategory(id);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }

        if (maintenanceRequest is null) throw new ObjectNotFoundServiceException();

        return maintenanceRequest;
    }

    public void CreateMaintenanceRequest(MaintenanceRequest maintenanceRequest)
    {
        try
        {
            maintenanceRequest.MaintenanceRequestValidator();
            _maintenanceRequestRepository.CreateMaintenanceRequest(maintenanceRequest);
        }
        catch (InvalidMaintenanceRequestException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (Exception exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
    }

    public void UpdateMaintenanceRequest(Guid idToUpdate, MaintenanceRequest maintenanceRequestUpdated)
    {
        MaintenanceRequest maintenanceRequestNotUpdated =
            _maintenanceRequestRepository.GetMaintenanceRequestById(idToUpdate);
        try
        {
            if (maintenanceRequestNotUpdated is null) throw new ObjectNotFoundServiceException();
            MapProperties(maintenanceRequestUpdated, maintenanceRequestNotUpdated);
            maintenanceRequestUpdated.MaintenanceRequestValidator();
            if(maintenanceRequestUpdated.RequestStatus == RequestStatusEnum.Closed)
            {
                maintenanceRequestUpdated.ClosedDate = DateTime.Now;
            }
            _maintenanceRequestRepository.UpdateMaintenanceRequest(idToUpdate, maintenanceRequestUpdated);//
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundServiceException();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    private static void MapProperties(MaintenanceRequest maintenanceWithUpdates, MaintenanceRequest maintenanceNotUpdated)
    {
        if (maintenanceWithUpdates.Equals(maintenanceNotUpdated))
        {
            throw new ObjectRepeatedServiceException();
        }

        foreach (PropertyInfo property in typeof(MaintenanceRequest).GetProperties())
        {
            object? originalValue = property.GetValue(maintenanceNotUpdated);
            object? updatedValue = property.GetValue(maintenanceWithUpdates);

            if (Guid.TryParse(updatedValue?.ToString(), out Guid id))
            {
                if (id == Guid.Empty)
                {
                    property.SetValue(maintenanceWithUpdates, originalValue);
                }
            }

            if (updatedValue == null && originalValue != null)
            {
                property.SetValue(maintenanceWithUpdates, originalValue);
            }
        }
    }

    public void AssignMaintenanceRequest(Guid idToUpdate, Guid idOfWorker)
    {
        try
        {
            MaintenanceRequest maintenanceRequest = GetMaintenanceRequestById(idToUpdate);

            ValidateStatusOfRequestInDb(maintenanceRequest);

            maintenanceRequest.RequestHandlerId = idOfWorker;
            maintenanceRequest.RequestStatus = RequestStatusEnum.InProgress;
            maintenanceRequest.OpenedDate = DateTime.Now;
            _maintenanceRequestRepository.UpdateMaintenanceRequest(idToUpdate, maintenanceRequest);
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundServiceException();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    private static void ValidateStatusOfRequestInDb(MaintenanceRequest maintenanceRequest)
    {
        if (maintenanceRequest.RequestStatus == RequestStatusEnum.InProgress || maintenanceRequest.RequestStatus == RequestStatusEnum.Closed)
        {
            throw new ObjectErrorServiceException("Request is already in progress by other request handler or the request is closed");
        }
    }

    public IEnumerable<MaintenanceRequest> GetMaintenanceRequestsByRequestHandler(Guid? requestHandlerId)
    {
        IEnumerable<MaintenanceRequest> maintenanceRequests;
        try
        {
            maintenanceRequests =
                _maintenanceRequestRepository.GetMaintenanceRequestsByRequestHandler(requestHandlerId);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }

        if (maintenanceRequests is null) throw new ObjectNotFoundServiceException();
        return maintenanceRequests;
    }

    public MaintenanceRequest GetMaintenanceRequestById(Guid id)
    {
        try
        {
            MaintenanceRequest maintenanceRequest = _maintenanceRequestRepository.GetMaintenanceRequestById(id);
            if (maintenanceRequest is null) throw new ObjectNotFoundServiceException();
            return maintenanceRequest;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundServiceException();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }
}