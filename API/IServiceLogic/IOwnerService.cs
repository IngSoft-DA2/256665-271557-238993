using Domain;
using WebModel.Requests.OwnerRequests;

namespace IServiceLogic;

public interface IOwnerService
{
    public IEnumerable<Owner> GetAllOwners();
    public Owner GetOwnerById(Guid ownerId);
    public void CreateOwner(Owner ownerToCreate);
    public void UpdateOwnerById(Owner ownerWithUpdates);
}