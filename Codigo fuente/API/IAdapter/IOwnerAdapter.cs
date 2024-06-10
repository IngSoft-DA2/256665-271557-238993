using WebModel.Requests.OwnerRequests;
using WebModel.Responses.OwnerResponses;

namespace IAdapter;

public interface IOwnerAdapter
{
    public IEnumerable<GetOwnerResponse> GetAllOwners();
    public GetOwnerResponse GetOwnerById(Guid ownerId);
    public CreateOwnerResponse CreateOwner(CreateOwnerRequest createOwnerRequest);
    public void UpdateOwnerById(Guid id, UpdateOwnerRequest ownerWithUpdates);
}