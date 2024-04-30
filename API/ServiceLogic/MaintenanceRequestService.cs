using System.Reflection;
using Domain;
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

    public IEnumerable<MaintenanceRequest> GetAllMaintenanceRequests()
    {
        try
        {
            IEnumerable<MaintenanceRequest> maintenanceRequests = _maintenanceRequestRepository.GetAllMaintenanceRequests();
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
        catch (Exception exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
    }

    public void UpdateMaintenanceRequest(Guid idToUpdate, MaintenanceRequest maintenanceRequestUpdated)
    {
        MaintenanceRequest maintenanceRequestNotUpdated = _maintenanceRequestRepository.GetMaintenanceRequestById(idToUpdate);
        try
        {
            ValidateRequestsBeforeUpdate(maintenanceRequestNotUpdated, maintenanceRequestUpdated);
            maintenanceRequestUpdated.MaintenanceRequestValidator();
            _maintenanceRequestRepository.UpdateMaintenanceRequest(idToUpdate, maintenanceRequestUpdated);
        }
        catch (InvalidMaintenanceRequestException exceptionCaught)
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
    private void ValidateRequestsBeforeUpdate(MaintenanceRequest maintenanceRequestNotUpdated, MaintenanceRequest maintenanceRequestUpdated)
    {
        if (maintenanceRequestNotUpdated is null) throw new ObjectNotFoundServiceException();
        if (maintenanceRequestNotUpdated.BuildingId != maintenanceRequestUpdated.BuildingId) throw new InvalidMaintenanceRequestException("BuildingId cannot be changed");
        if (maintenanceRequestNotUpdated.FlatId != maintenanceRequestUpdated.FlatId) throw new InvalidMaintenanceRequestException("FlatId cannot be changed");
        if (maintenanceRequestNotUpdated.OpenedDate != maintenanceRequestUpdated.OpenedDate) throw new InvalidMaintenanceRequestException("OpenedDate cannot be changed");
        if (maintenanceRequestNotUpdated.ClosedDate != maintenanceRequestUpdated.ClosedDate) throw new InvalidMaintenanceRequestException("ClosedDate cannot be changed");
        
    }

    public void AssignMaintenanceRequest(Guid idToUpdate, Guid idOfWorker)
    {
        MaintenanceRequest maintenanceRequest = GetMaintenanceRequestById(idToUpdate);
        try
        {
            if (maintenanceRequest is null) throw new ObjectNotFoundServiceException();
            maintenanceRequest.RequestHandlerId = idOfWorker;
            _maintenanceRequestRepository.UpdateMaintenanceRequest(idToUpdate, maintenanceRequest);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    public IEnumerable<MaintenanceRequest> GetMaintenanceRequestsByRequestHandler(Guid requestHandlerId)
    {
        IEnumerable<MaintenanceRequest> maintenanceRequests;
        try
        {
            maintenanceRequests = _maintenanceRequestRepository.GetMaintenanceRequestsByRequestHandler(requestHandlerId);
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