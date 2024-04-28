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

    public MaintenanceRequest GetMaintenanceRequestByCategory(Guid id)
    {
        MaintenanceRequest maintenanceRequest;
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
            MapProperties(maintenanceRequestNotUpdated, maintenanceRequestUpdated);
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

    private void MapProperties(MaintenanceRequest maintenanceRequestNotUpdated, MaintenanceRequest maintenanceRequestUpdated)
    {
        foreach (PropertyInfo property in maintenanceRequestNotUpdated.GetType().GetProperties())
        {
            object? originalValue = property.GetValue(maintenanceRequestNotUpdated);
            object? updatedValue = property.GetValue(maintenanceRequestUpdated);

            if (updatedValue == null && originalValue != null)
            {
                property.SetValue(maintenanceRequestUpdated, originalValue);
            }
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
    public MaintenanceRequest GetMaintenanceRequestById(Guid id)
    {
        MaintenanceRequest maintenanceRequest;
        try
        {
            maintenanceRequest = _maintenanceRequestRepository.GetMaintenanceRequestById(id);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    
        if (maintenanceRequest is null) throw new ObjectNotFoundServiceException();
        return maintenanceRequest;
    }

}