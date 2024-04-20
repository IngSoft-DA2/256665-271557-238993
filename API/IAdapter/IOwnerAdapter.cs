using WebModel.Requests.OwnerRequests;
using WebModel.Responses.OwnerResponses;

namespace IAdapter;

public interface IOwnerAdapter
{
    public IEnumerable<OwnerResponse> GetOwners();
    public CreateOwnerResponse CreateOwner(CreateOwnerRequest createOwnerRequest);
    public void UpdateOwner(Guid id, UpdateOwnerRequest req);
}