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
        #region Constructor and attributes
        
        private readonly IMaintenanceRequestAdapter _maintenanceAdapter;

        public MaintenanceController(IMaintenanceRequestAdapter maintenanceAdapter)
        {
            _maintenanceAdapter = maintenanceAdapter;
        }
        
        #endregion
        
        #region Get All Maintenance Requests

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
        
        #endregion
        
        #region Create Maintenance Request

        [HttpPost]
        public IActionResult CreateMaintenanceRequest([FromBody] CreateRequestMaintenanceRequest request)
        {
            try
            {
                CreateRequestMaintenanceResponse response = _maintenanceAdapter.CreateMaintenanceRequest(request);
                return CreatedAtAction(nameof(CreateMaintenanceRequest), new { id = response.Id }, response);
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
        
        #region Get Maintenance Request By Category Id

        [HttpGet]
        [Route("/category/requests")]
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
        
        #endregion
        
        #region Assign Maintenance Request

        [HttpPut]
        [Route("/request-handler/requests")]
        public IActionResult AssignMaintenanceRequest(Guid idOfRequestToUpdate, Guid idOfWorker)
        {
            try
            {
                _maintenanceAdapter.AssignMaintenanceRequest(idOfRequestToUpdate, idOfWorker);
                return NoContent();
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
        
        #endregion
        
        #region Get Maintenance Requests By Request Handler

        [HttpGet]
        [Route("/request-handler/{handlerId:Guid}/requests")]
        public IActionResult GetMaintenanceRequestByRequestHandler([FromRoute] Guid handlerId)
        {
            try
            {
                return Ok(_maintenanceAdapter.GetMaintenanceRequestsByRequestHandler(handlerId));
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
        
        #endregion
        
        #region Update Maintenance Request

        [HttpPut]
        [Route("/requests/{id:Guid}")]
        public IActionResult UpdateMaintenanceRequestStatus([FromRoute] Guid id,
            [FromBody] UpdateMaintenanceRequestStatusRequest request)
        {
            try
            {
                _maintenanceAdapter.UpdateMaintenanceRequestStatus(id, request);
                return NoContent();
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
        
        #endregion
        
        #region Get Maintenance Request By Id

        [HttpGet]
        [Route("/requests/{id:Guid}")]
        public IActionResult GetMaintenanceRequestById(Guid id)
        {
            try
            {
                return Ok(_maintenanceAdapter.GetMaintenanceRequestById(id));
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
        
        #endregion
    }
}