using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.OwnerResponses;

namespace Adapter;

public class FlatAdapter
{
    private readonly IFlatService _flatService;

    public FlatAdapter(IFlatService flatService)
    {
        _flatService = flatService;
    }


    public IEnumerable<GetFlatResponse> GetAllFlats(Guid buildingId)
    {
        try
        {
            IEnumerable<Flat> flats = _flatService.GetAllFlats(buildingId);

            IEnumerable<GetFlatResponse> flatsToReturn = flats.Select(flat => new GetFlatResponse
            {
                Id = flat.Id,
                Floor = flat.Floor,
                RoomNumber = flat.RoomNumber,
                GetOwnerAssigned = new GetOwnerAssignedResponse
                {
                    Id = flat.OwnerAssigned.Id,
                    Firstname = flat.OwnerAssigned.Firstname,
                    Lastname = flat.OwnerAssigned.Lastname,
                    Email = flat.OwnerAssigned.Email
                },
                TotalRooms = flat.TotalRooms,
                TotalBaths = flat.TotalBaths,
                HasTerrace = flat.HasTerrace
            });

            return flatsToReturn;
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

    public GetFlatResponse GetFlatById(Guid buildingId, Guid flatId)
    {
        try
        {
            Flat flatFound = _flatService.GetFlatById(buildingId, flatId);

            GetFlatResponse flatToReturn = new GetFlatResponse
            {
                Id = flatFound.Id,
                Floor = flatFound.Floor,
                RoomNumber = flatFound.RoomNumber,
                GetOwnerAssigned = new GetOwnerAssignedResponse
                {
                    Id = flatFound.OwnerAssigned.Id,
                    Firstname = flatFound.OwnerAssigned.Firstname,
                    Lastname = flatFound.OwnerAssigned.Lastname,
                    Email = flatFound.OwnerAssigned.Email
                },
                TotalRooms = flatFound.TotalRooms,
                TotalBaths = flatFound.TotalBaths,
                HasTerrace = flatFound.HasTerrace
            };

            return flatToReturn;
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
}