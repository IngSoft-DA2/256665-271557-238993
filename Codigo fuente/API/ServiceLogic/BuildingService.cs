using System.Reflection;
using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class BuildingService : IBuildingService
{
    #region Constructor and Atributtes
    
    private readonly IBuildingRepository _buildingRepository;
    private readonly ISessionService _sessionService;

    public BuildingService(IBuildingRepository buildingRepository, ISessionService sessionService)
    {
        _buildingRepository = buildingRepository;
        _sessionService = sessionService;
    }
    
    #endregion
    
    #region Get All Buildings

    public IEnumerable<Building> GetAllBuildings(Guid userId)
    {
        try
        {
            return _buildingRepository.GetAllBuildings(userId);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }
    
    #endregion
    
    #region Get Building By Id

    public Building GetBuildingById(Guid buildingId)
    {
        Building buildingFound;
        try
        {
            buildingFound = _buildingRepository.GetBuildingById(buildingId);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }

        if (buildingFound == null)
        {
            throw new ObjectNotFoundServiceException();
        }

        return buildingFound;
    }
    
    #endregion
    
    #region Create Building

    public void CreateBuilding(Building building)
    {
        try
        {
            building.BuildingValidator();
            IEnumerable<Building> buildings = GetAllBuildings(building.ManagerId);
            CheckIfNameAlreadyExists(building, buildings);
            CheckIfLocationAndAddressAlreadyExists(building, buildings);
            CheckIfHasValidManager(building.Manager);
            _buildingRepository.CreateBuilding(building);
        }
        catch (InvalidBuildingException exception)
        {
            throw new ObjectErrorServiceException(exception.Message);
        }
        catch (ObjectRepeatedServiceException)
        {
            throw new ObjectRepeatedServiceException();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    private static void CheckIfLocationAndAddressAlreadyExists(Building building, IEnumerable<Building> buildings)
    {
        double minGap = 0.000001;
        if (buildings.Any(b => Math.Abs(b.Location.Latitude - building.Location.Latitude) < minGap &&
                               Math.Abs(b.Location.Longitude - building.Location.Longitude) < minGap &&
                               b.Address == building.Address))
        {
            throw new ObjectRepeatedServiceException();
        }
    }

    private static void CheckIfNameAlreadyExists(Building building, IEnumerable<Building> buildings)
    {
        if (buildings.Any(b => b.Name == building.Name))
        {
            throw new ObjectRepeatedServiceException();
        }
    }
    
    private void CheckIfHasValidManager(Manager manager)
    {
        if (manager == null)
        {
            throw new ObjectErrorServiceException("Building must have a manager");
        }

        if(!_sessionService.IsUserAuthenticated(manager.Email))
        {
            throw new ObjectErrorServiceException("Manager must be a registered user");
        }
    }
    
    #endregion
    
    #region Update Building

    public void UpdateBuilding(Building buildingWithUpdates)
    {
        try
        {
            Building buildingNotUpdated = _buildingRepository.GetBuildingById(buildingWithUpdates.Id);

            if (buildingNotUpdated is null)
            {
                throw new ObjectNotFoundServiceException();
            }
            
            MapProperties(buildingWithUpdates, buildingNotUpdated);

            buildingWithUpdates.BuildingValidator();

            _buildingRepository.UpdateBuilding(buildingWithUpdates);
        }
        catch (InvalidBuildingException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (ObjectRepeatedServiceException)
        {
            throw new ObjectRepeatedServiceException();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    private static void MapProperties(Building buildingWithUpdates, Building buildingNotUpdated)
    {
        if (buildingWithUpdates.Equals(buildingNotUpdated))
        {
            throw new ObjectRepeatedServiceException();
        }

        foreach (PropertyInfo property in typeof(Building).GetProperties())
        {
            object? originalValue = property.GetValue(buildingNotUpdated);
            object? updatedValue = property.GetValue(buildingWithUpdates);

            if (Guid.TryParse(updatedValue?.ToString(), out Guid id))
            {
                if (id == Guid.Empty)
                {
                    property.SetValue(buildingWithUpdates, originalValue);
                }
            }

            if (updatedValue == null && originalValue != null)
            {
                property.SetValue(buildingWithUpdates, originalValue);
            }
        }
    }
    
    #endregion
    
    #region Delete Building

    public void DeleteBuilding(Guid buildingIdToDelete)
    {
        try
        {
            Building buildingToDelete = GetBuildingById(buildingIdToDelete);
            _buildingRepository.DeleteBuilding(buildingToDelete);
        }
        catch(ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundServiceException();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }
    
    #endregion
    
}