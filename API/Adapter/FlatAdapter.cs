using Domain;
using IServiceLogic;
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
    
}