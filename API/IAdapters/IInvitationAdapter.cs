using WebModels.Responses;

namespace IAdapters;

public interface IInvitationAdapter
{
    public ICollection<GetInvitationResponse> GetAllInvitations();
}