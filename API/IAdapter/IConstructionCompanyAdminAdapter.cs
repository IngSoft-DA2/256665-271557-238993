using Domain.Enums;
using WebModel.Requests.ConstructionCompanyAdminRequests;
using WebModel.Responses.ConstructionCompanyAdminResponses;

namespace IAdapter;

public interface IConstructionCompanyAdminAdapter
{
    public CreateConstructionCompanyAdminResponse CreateConstructionCompanyAdmin(
        CreateConstructionCompanyAdminRequest createRequest, SystemUserRoleEnum? userRole);
}