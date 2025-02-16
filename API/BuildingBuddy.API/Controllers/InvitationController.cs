using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests;
using WebModel.Requests.InvitationRequests;
using WebModel.Responses.InvitationResponses;

namespace BuildingBuddy.API.Controllers
{
    [CustomExceptionFilter]
    [Route("api/v1/invitations")]
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
        [AllowAnonymous]
        public IActionResult GetAllInvitations([FromQuery] string email)
        {

                if (!string.IsNullOrEmpty(email))
                {
                    return Ok(_invitationAdapter.GetAllInvitationsByEmail(email));
                }
                return Ok(_invitationAdapter.GetAllInvitations());
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
        public IActionResult CreateInvitation([FromBody] CreateInvitationRequest request)
        {
                CreateInvitationResponse response = _invitationAdapter.CreateInvitation(request);
                return CreatedAtAction(nameof(CreateInvitation), new { id = response.Id }, response);
           
        }

        #endregion

        #region Update Invitation

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateInvitation([FromRoute] Guid id, [FromBody] UpdateInvitationRequest request)
        {
           
            _invitationAdapter.UpdateInvitation(id, request);
            return NoContent();
           
        }

        #endregion

        #region Delete Invitation

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteInvitation([FromRoute] Guid id)
        {
          
            _invitationAdapter.DeleteInvitation(id);
            return NoContent();
           
        }

        #endregion
    }
}