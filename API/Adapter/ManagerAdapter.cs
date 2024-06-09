using Adapter.CustomExceptions;
using Domain;
using Domain.Enums;
using IAdapter;
using IServiceLogic;
using Microsoft.AspNetCore.Mvc;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.ManagerRequests;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.ConstructionCompanyResponses;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.ManagerResponses;

namespace Adapter;

public class ManagerAdapter : IManagerAdapter
{
    #region Constructor and attributes

    private readonly IManagerService _managerServiceLogic;
    private readonly IInvitationService _invitationServiceLogic;

    public ManagerAdapter(IManagerService managerServiceLogic, IInvitationService invitationServiceLogic)
    {
        _managerServiceLogic = managerServiceLogic;
        _invitationServiceLogic = invitationServiceLogic;
    }

    #endregion

    #region Get All Managers

    public IEnumerable<GetManagerResponse> GetAllManagers()
    {
        try
        {
            IEnumerable<Manager> serviceResponse = _managerServiceLogic.GetAllManagers();

            IEnumerable<GetManagerResponse> adapterResponse = serviceResponse.Select(manager =>
                new GetManagerResponse
                {
                    Id = manager.Id,
                    Email = manager.Email,
                    Name = manager.Firstname,
                    BuildingsId = manager.Buildings.Select(building => building.Id).ToList(),
                    MaintenanceRequestsId = manager.Requests.Select(maintenanceRequest => maintenanceRequest.Id).ToList()
                }
            ).ToList();
            return adapterResponse;
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    #endregion
    
    #region Get Manager By Id

    public GetManagerResponse GetManagerById(Guid managerId)
    {
        try
        {
            Manager managerFound = _managerServiceLogic.GetManagerById(managerId);

            GetManagerResponse adapterResponse = new GetManagerResponse
            {
                Id = managerFound.Id,
                Email = managerFound.Email,
                Name = managerFound.Firstname,
                BuildingsId = managerFound.Buildings.Select(building => building.Id).ToList(),
                MaintenanceRequestsId = managerFound.Requests.Select(maintenanceRequest => maintenanceRequest.Id).ToList()
            };

            return adapterResponse;
        }
        catch (ObjectNotFoundServiceException exceptionCaught)
        {
            throw new ObjectNotFoundAdapterException(exceptionCaught.Message);
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    #endregion

    #region Delete Manager By Id

    public void DeleteManagerById(Guid id)
    {
        try
        {
            _managerServiceLogic.DeleteManagerById(id);
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Manager was not found");
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    #endregion

    #region Create Manager

    public CreateManagerResponse CreateManager(CreateManagerRequest createRequest, Guid idOfInvitationToAccept)
    {
        try
        {
            Manager manager = new Manager
            {
                Id = Guid.NewGuid(),
                Email = createRequest.Email,
                Password = createRequest.Password,
                Role = SystemUserRoleEnum.Manager
            };

            Invitation invitationToAccept = _invitationServiceLogic.GetInvitationById(idOfInvitationToAccept);

            _managerServiceLogic.CreateManager(manager, invitationToAccept);

            CreateManagerResponse adapterResponse = new CreateManagerResponse
            {
                Id = manager.Id,
                Firstname = invitationToAccept.Firstname
            };

            return adapterResponse;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Invitation was not found");
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownAdapterException(exceptionCaught.Message);
        }
    }
    
    #endregion
}