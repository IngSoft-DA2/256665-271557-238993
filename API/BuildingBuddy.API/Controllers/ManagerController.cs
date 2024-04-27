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
        private readonly IManagerAdapter _managerAdapter;

        public ManagerController(IManagerAdapter managerAdapter)
        {
            _managerAdapter = managerAdapter;
        }

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

        [HttpPost]
        public IActionResult CreateManager(CreateManagerRequest createRequest)
        {
            try
            {
                CreateManagerResponse adapterReponse = _managerAdapter.CreateManager(createRequest);
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
        }
    }
}