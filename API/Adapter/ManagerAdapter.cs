using Adapter.CustomExceptions;
using Domain;
using IAdapter;
using IServiceLogic;
using Microsoft.AspNetCore.Mvc;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.ManagerRequests;
using WebModel.Responses.ManagerResponses;

namespace Adapter;

public class ManagerAdapter : IManagerAdapter
{
    #region Constructor and attributes

    private readonly IManagerService _managerServiceLogic;

    public ManagerAdapter(IManagerService managerServiceLogic)
    {
        _managerServiceLogic = managerServiceLogic;
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
                });

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

    public CreateManagerResponse CreateManager(CreateManagerRequest createRequest)
    {
        try
        {
            Manager manager = new Manager
            {
                Id = Guid.NewGuid(),
                Firstname = createRequest.FirstName,
                Email = createRequest.Email,
                Password = createRequest.Password,
            };

            _managerServiceLogic.CreateManager(manager);

            CreateManagerResponse adapterResponse = new CreateManagerResponse
            {
                Id = manager.Id
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