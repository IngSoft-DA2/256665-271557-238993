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
                    Buildings = manager.Buildings.Select(building => building.Id).ToList(),
                    MaintenanceRequests = manager.Requests.Select(maintenanceRequest => maintenanceRequest.Id).ToList()
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

#region Delete Manager By Id

public void DeleteManagerById(Guid id)
{
    try
    {
        _managerServiceLogic.DeleteManagerById(id);
    }
    catch (ObjectNotFoundServiceException)
    {
        throw new ObjectNotFoundAdapterException();
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
            Password = createRequest.Password
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
        throw new ObjectNotFoundAdapterException();
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