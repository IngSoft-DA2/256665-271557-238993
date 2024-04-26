using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.MaintenanceRequests;
using WebModel.Responses.MaintenanceResponses;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceAdapter _maintenanceAdapter;

        public MaintenanceController(IMaintenanceAdapter maintenanceAdapter)
        {
            _maintenanceAdapter = maintenanceAdapter;
        }
        [HttpGet]
        [Route("requests")]
        public IActionResult GetAllMaintenanceRequests()
        {
            try
            {
                return Ok(_maintenanceAdapter.GetAllMaintenanceRequests());
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpPost]
        public IActionResult CreateMaintenanceRequest([FromBody] CreateRequestMaintenanceRequest request)
        {
            try
            {
                CreateRequestMaintenanceResponse response = _maintenanceAdapter.CreateMaintenanceRequest(request);
                return CreatedAtAction(nameof(CreateMaintenanceRequest), new {id = response.Id}, response);
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
        
        [HttpGet]
        [Route("/category/{categoryId:Guid}/requests")]
        public IActionResult GetMaintenanceRequestByCategory([FromQuery] Guid categoryId)
        {
            try
            {
                return Ok(_maintenanceAdapter.GetMaintenanceRequestByCategory(categoryId));
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("Maintenance request was not found, reload the page");
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpPut]
        [Route("/request-handler/requests")]
        public IActionResult AssignMaintenanceRequest([FromBody] AssignMaintenanceRequestRequest request)
        {
            try
            {
                return Ok(_maintenanceAdapter.AssignMaintenanceRequest(request));
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("Maintenance request was not found, reload the page");
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

        [HttpGet]
        [Route("/request-handler/{handlerId:Guid}/requests")]
        public IActionResult GetMaintenanceRequestByRequestHandler([FromRoute] Guid handlerId)
        {
            try
            {
                return Ok(_maintenanceAdapter.GetMaintenanceRequestByRequestHandler(handlerId));
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("Maintenance request was not found, reload the page");
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [Route("/requests/{id:Guid}")]
        public IActionResult UpdateMaintenanceRequestStatus([FromRoute] Guid id, [FromBody] UpdateMaintenanceRequestStatusRequest request)
        {
            try
            {
                return Ok(_maintenanceAdapter.UpdateMaintenanceRequestStatus(id, request));
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("Maintenance request was not found, reload the page");
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
    }
}