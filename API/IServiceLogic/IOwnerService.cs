using Domain;

namespace IServiceLogic;

public interface IOwnerService
{
    public Owner? GetOwnerById(Guid ownerId);

    public IEnumerable<Owner> GetAllOwners();
}