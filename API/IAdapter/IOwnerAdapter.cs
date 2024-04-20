using WebModel.Responses.OwnerResponses;

namespace IAdapter;

public interface IOwnerAdapter
{
    public IEnumerable<OwnerResponse> GetOwners();
}