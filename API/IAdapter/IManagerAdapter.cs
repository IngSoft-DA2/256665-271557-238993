using WebModel.Responses.ManagerResponses;

namespace IAdapter;

public interface IManagerAdapter
{
    public IEnumerable<GetManagerResponse> GetAllManagers();
}