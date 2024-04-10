using Domain;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebModels.Responses;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly IInvitationService _invitationService;
        public InvitationsController(IInvitationService invitationService)
        {
            _invitationService = invitationService;
        }

        [HttpGet]
        public IActionResult GetAllInvitations()
        {
            return Ok(_invitationService.GetAllInvitations().
                Select(invitation=> new GetInvitationResponse(invitation)).ToList());
        }
    }
}
