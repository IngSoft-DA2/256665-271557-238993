using WebModels.Responses;

namespace IAdapters;

public interface IInvitationAdapter
{
    public IEnumerable<GetInvitationResponse> GetAllInvitations();
    public GetInvitationResponse GetInvitationById(Guid idOfInvitation);
    
}