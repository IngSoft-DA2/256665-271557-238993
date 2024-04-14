using Microsoft.AspNetCore.Mvc;
using WebModel.Requests;
using WebModels.Responses;

namespace IAdapter;

public interface IInvitationAdapter
{
    public IEnumerable<GetInvitationResponse> GetAllInvitations();
    public GetInvitationResponse GetInvitationById(Guid idOfInvitation);
    public CreateInvitationResponse CreateInvitation(CreateInvitationRequest invitationToCreate);
}