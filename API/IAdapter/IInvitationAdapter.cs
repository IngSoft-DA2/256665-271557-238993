using WebModel.Requests;
using WebModel.Requests.InvitationRequests;
using WebModel.Responses.InvitationResponses;

namespace IAdapter;

public interface IInvitationAdapter
{
    public IEnumerable<GetInvitationResponse> GetAllInvitations(string email);
    public GetInvitationResponse GetInvitationById(Guid idOfInvitation);
    public CreateInvitationResponse CreateInvitation(CreateInvitationRequest invitationToCreate);
    public void UpdateInvitation(Guid id, UpdateInvitationRequest invitationWithUpdates);
    public void DeleteInvitation(Guid idOfInvitationToDelete);
    public GetInvitationResponse GetAllInvitationsByEmail(string email);
}