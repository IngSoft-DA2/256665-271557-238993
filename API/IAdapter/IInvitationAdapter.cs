using WebModels.Responses;

namespace IAdapter;

public interface IInvitationAdapter
{
    public IEnumerable<GetInvitationResponse> GetAllInvitations();
    public GetInvitationResponse GetInvitationById(Guid idOfInvitation);
    
}