using Domain;

namespace IRepository;

public interface IOwnerRepository
{
    public IEnumerable<Owner> GetAllOwners();
}