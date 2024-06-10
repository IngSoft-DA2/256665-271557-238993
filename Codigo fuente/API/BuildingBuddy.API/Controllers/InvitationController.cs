using BuildingBuddy.API.Filters;
using Domain.Enums;
using IAdapter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.InvitationRequests;
using WebModel.Responses.InvitationResponses;

namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
    [Route("api/v2/invitations")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        #region Constructor and attributes

        private readonly IInvitationAdapter _invitationAdapter;

        public InvitationController(IInvitationAdapter invitationAdapter)
        {
            _invitationAdapter = invitationAdapter;
        }

        #endregion

        #region Get All Invitations

        [HttpGet]
        [AuthenticationFilter(SystemUserRoleEnum.Admin)]
        public IActionResult GetAllInvitations()
        {
            return Ok(_invitationAdapter.GetAllInvitations());
        }

        [HttpGet]
        [Route("guest")]
        public IActionResult GetInvitationsByEmail([FromQuery] string email)
        {
            return Ok(_invitationAdapter.GetAllInvitationsByEmail(email));
        }

        #endregion

        #region Get Invitation By Id
        
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetInvitationById([FromRoute] Guid id)
        {
            return Ok(_invitationAdapter.GetInvitationById(id));
        }

        #endregion

        #region Create Invitation

        [HttpPost]
        [AuthenticationFilter(SystemUserRoleEnum.Admin)]
        public IActionResult CreateInvitation([FromBody] CreateInvitationRequest request)
        {
            CreateInvitationResponse response = _invitationAdapter.CreateInvitation(request);
            return CreatedAtAction(nameof(CreateInvitation), new { id = response.Id }, response);
        }

        #endregion

        #region Update Invitation

        [HttpPut]
        [AuthenticationFilter(SystemUserRoleEnum.Admin)]
        [Route("{id:Guid}")]
        public IActionResult UpdateInvitation([FromRoute] Guid id, [FromBody] UpdateInvitationRequest request)
        {
            _invitationAdapter.UpdateInvitation(id, request);
            return NoContent();
        }

        #endregion

        #region Delete Invitation

        [HttpDelete]
        [AuthenticationFilter(SystemUserRoleEnum.Admin)]
        [Route("{id:Guid}")]
        public IActionResult DeleteInvitation([FromRoute] Guid id)
        {
            _invitationAdapter.DeleteInvitation(id);
            return NoContent();
        }

        #endregion
    }
}