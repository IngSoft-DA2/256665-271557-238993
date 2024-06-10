using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.ManagerRequests;
using WebModel.Responses.ManagerResponses;

namespace IAdapter;

public interface IManagerAdapter
{
    public IEnumerable<GetManagerResponse> GetAllManagers();
    
    public void DeleteManagerById(Guid managerId);
    public CreateManagerResponse CreateManager(CreateManagerRequest createRequest, Guid idOfInvitationToAccept);
    public GetManagerResponse GetManagerById(Guid managerId);
}