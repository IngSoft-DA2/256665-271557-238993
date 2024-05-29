using WebModel.Requests.ConstructionCompanyAdminRequests;
using WebModel.Responses.ConstructionCompanyAdminResponses;

namespace IAdapter;

public interface IConstructionCompanyAdminAdapter
{
    public CreateConstructionCompanyAdminResponse CreateConstructionCompanyAdminByInvitation(
        CreateConstructionCompanyAdminRequest createRequest, Guid invitationIdToAccept);

    public CreateConstructionCompanyAdminResponse CreateConstructionCompanyAdminByAnotherAdmin(CreateConstructionCompanyAdminRequest createRequest);
}