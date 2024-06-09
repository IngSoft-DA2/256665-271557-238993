using Adapter.CustomExceptions;
using Domain;
using IAdapter;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.FlatRequests;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.OwnerResponses;

namespace Adapter;

public class FlatAdapter : IFlatAdapter
{
    #region Constructor and attributes

    private readonly IOwnerService _ownerService;
    private readonly IFlatService _flatService;

    public FlatAdapter(IOwnerService ownerService, IFlatService flatService)
    {
        _ownerService = ownerService;
        _flatService = flatService;
    }

    #endregion

    #region Create Flat

    public void CreateFlat(CreateFlatRequest flat)
    {
        try
        {
            Owner ownerAssigned = _ownerService.GetOwnerById(flat.OwnerAssignedId);

            Flat flatToCreate = new Flat
            {
                Id = Guid.NewGuid(),
                Floor = flat.Floor,
                RoomNumber = flat.RoomNumber,
                OwnerAssigned = ownerAssigned,
                TotalRooms = flat.TotalRooms,
                TotalBaths = flat.TotalBaths,
                HasTerrace = flat.HasTerrace
            };

            _flatService.CreateFlat(flatToCreate);
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException("Flat was not created because the owner assigned was not found.");
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    #endregion
}