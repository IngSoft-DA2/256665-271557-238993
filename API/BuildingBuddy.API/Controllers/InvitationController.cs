using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests;
using WebModel.Requests.InvitationRequests;
using WebModel.Responses.InvitationResponses;

namespace BuildingBuddy.API.Controllers
{
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
        public IActionResult GetAllInvitations([FromQuery] string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    return Ok(_invitationAdapter.GetAllInvitations(email));
                }
                else
                {
                    return Ok(_invitationAdapter.GetAllInvitationsByEmail(email));
                }
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("No invitations were found in Database");
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        #endregion
        
        #region Get Invitation By Id

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetInvitationById([FromRoute] Guid id)
        {
            try
            {
                return Ok(_invitationAdapter.GetInvitationById(id));
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("Invitation was not found, reload the page");
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        #endregion     
        
        #region Create Invitation
        
        [HttpPost]
        public IActionResult CreateInvitation([FromBody] CreateInvitationRequest request)
        {
            try
            {
                CreateInvitationResponse response = _invitationAdapter.CreateInvitation(request);
                return CreatedAtAction(nameof(CreateInvitation), new { id = response.Id }, response);
            }
            catch (ObjectErrorAdapterException exceptionCaught)
            {
                return BadRequest(exceptionCaught.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        #endregion
        
        #region Update Invitation

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateInvitation([FromRoute] Guid id, [FromBody] UpdateInvitationRequest request)
        {
            try
            {
                _invitationAdapter.UpdateInvitation(id, request);
                return NoContent();
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("The specific invitation was not found in Database");
            }
            catch (ObjectErrorAdapterException exceptionCaught)
            {
                return BadRequest(exceptionCaught.Message);
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        
        }
        
        #endregion
        
        #region Delete Invitation

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteInvitation([FromRoute] Guid id)
        {
            try
            {
                _invitationAdapter.DeleteInvitation(id);
                return NoContent();
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("Invitation to delete was not found");
            }
            catch (ObjectErrorAdapterException exceptionCaught)
            {
                return BadRequest(exceptionCaught.Message);
            }
            catch (Exception internalException)
            {
                Console.WriteLine(internalException.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        #endregion
    }
}