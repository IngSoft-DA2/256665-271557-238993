using WebModel.Requests.AdministratorRequests;
using WebModel.Responses.AdministratorResponses;

namespace IAdapter;

public interface IAdministratorAdapter
{
    public CreateAdministratorResponse CreateAdministrator(CreateAdministratorRequest request);
}