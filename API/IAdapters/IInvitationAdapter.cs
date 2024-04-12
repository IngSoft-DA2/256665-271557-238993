using WebModels.Responses;

namespace IAdapters;

public interface IInvitationAdapter
{
    public IEnumerable<GetInvitationResponse> GetAllInvitations();
}