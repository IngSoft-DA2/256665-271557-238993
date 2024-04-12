using IAdapters;
using Microsoft.AspNetCore.Mvc;
using WebModels.Responses;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly IInvitationAdapter _invitationAdapter;
        public InvitationsController(IInvitationAdapter invitationAdapter)
        {
            _invitationAdapter = invitationAdapter;
        }

        [HttpGet]
        public IActionResult GetAllInvitations()
        {
            return Ok(_invitationAdapter.GetAllInvitations());
        }
    }
}
