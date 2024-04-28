using Domain;

namespace IRepository;

public interface IOwnerRepository
{
    public IEnumerable<Owner> GetAllOwners();
    public Owner GetOwnerById(Guid ownerIdToObtain);
    public void CreateOwner(Owner ownerToCreate);
    public void UpdateOwnerById(Owner ownerWithUpdates);
}