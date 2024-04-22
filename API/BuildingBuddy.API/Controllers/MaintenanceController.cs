using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.MaintenanceRequests;

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
                return Ok(_maintenanceAdapter.CreateMaintenanceRequest(request));
            }
            catch (ObjectErrorException exceptionCaught)
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
        [Route("{categoryId:Guid}")]
        public IActionResult GetMaintenanceRequestByCategory([FromRoute] Guid categoryId)
        {
            try
            {
                return Ok(_maintenanceAdapter.GetMaintenanceRequestByCategory(categoryId));
            }
            catch (ObjectNotFoundException)
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
        public IActionResult AssignMaintenanceRequest([FromBody] AssignMaintenanceRequestRequest request)
        {
            try
            {
                return Ok(_maintenanceAdapter.(request));
            }
            catch (ObjectNotFoundException)
            {
                return NotFound("Maintenance request was not found, reload the page");
            }
            catch (ObjectErrorException exceptionCaught)
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