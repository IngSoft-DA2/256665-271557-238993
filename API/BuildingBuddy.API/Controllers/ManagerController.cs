using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.ManagerRequests;
using WebModel.Responses.ManagerResponses;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/managers")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        #region Constructor and attributes
        
        private readonly IManagerAdapter _managerAdapter;

        public ManagerController(IManagerAdapter managerAdapter)
        {
            _managerAdapter = managerAdapter;
        }
        
        #endregion
        
        #region Get All Managers

        [HttpGet]
        public IActionResult GetAllManagers()
        {
            try
            {
                return Ok(_managerAdapter.GetAllManagers());
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        #endregion

        #region Delete Manager
        
        [HttpDelete]
        [Route("{managerId:Guid}")]
        public IActionResult DeleteManagerById([FromRoute] Guid managerId)
        {
            try
            {
                _managerAdapter.DeleteManagerById(managerId);
                return NoContent();
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("Manager was not found in database");
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        #endregion
        
        #region Create Manager

        [HttpPost]
        public IActionResult CreateManager(CreateManagerRequest createRequest, [FromQuery] Guid idOfInvitationAccepted)
        {
            try
            {
                CreateManagerResponse adapterReponse = _managerAdapter.CreateManager(createRequest, idOfInvitationAccepted);
                return CreatedAtAction(nameof(CreateManager), new { id = adapterReponse.Id }, adapterReponse);
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("Manager was not found in database");
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
    }
}