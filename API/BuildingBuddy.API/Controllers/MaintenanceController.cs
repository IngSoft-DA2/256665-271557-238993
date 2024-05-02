using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.MaintenanceRequests;
using WebModel.Responses.MaintenanceResponses;

namespace BuildingBuddy.API.Controllers
{
    [CustomExceptionFilter]
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
            return Ok(_maintenanceAdapter.GetAllMaintenanceRequests());
            
        }
        
        #endregion
        
        #region Create Maintenance Request
        
        [HttpPost]
        public IActionResult CreateMaintenanceRequest([FromBody] CreateRequestMaintenanceRequest request)
        {
            CreateRequestMaintenanceResponse response = _maintenanceAdapter.CreateMaintenanceRequest(request);
                return CreatedAtAction(nameof(CreateMaintenanceRequest), new { id = response.Id }, response);
            
        }
        
        #endregion
        
        #region Get Maintenance Request By Category Id

        [HttpGet]
        [Route("/category/requests")]
        public IActionResult GetMaintenanceRequestByCategory([FromQuery] Guid categoryId)
        {
            return Ok(_maintenanceAdapter.GetMaintenanceRequestByCategory(categoryId));
            
        }
        
        #endregion
        
        #region Assign Maintenance Request
        
        [HttpPut]
        [Route("/request-handler/requests")]
        public IActionResult AssignMaintenanceRequest(Guid idOfRequestToUpdate, Guid idOfWorker)
        {
            
            _maintenanceAdapter.AssignMaintenanceRequest(idOfRequestToUpdate, idOfWorker);
            return NoContent();
            
        }
        
        #endregion
        
        #region Get Maintenance Requests By Request Handler
        
        [HttpGet]
        [Route("/request-handler/{handlerId:Guid}/requests")]
        public IActionResult GetMaintenanceRequestByRequestHandler([FromRoute] Guid handlerId)
        {
            
            return Ok(_maintenanceAdapter.GetMaintenanceRequestsByRequestHandler(handlerId));
            
        }
        
        #endregion
        
        #region Update Maintenance Request
        
        [HttpPut]
        [Route("/requests/{id:Guid}")]
        public IActionResult UpdateMaintenanceRequestStatus([FromRoute] Guid id,
            [FromBody] UpdateMaintenanceRequestStatusRequest request)
        {
            
            _maintenanceAdapter.UpdateMaintenanceRequestStatus(id, request);
            return NoContent();
            
        }
        
        #endregion
        
        #region Get Maintenance Request By Id
        
        [HttpGet]
        [Route("/requests/{id:Guid}")]
        public IActionResult GetMaintenanceRequestById(Guid id)
        {
           
            return Ok(_maintenanceAdapter.GetMaintenanceRequestById(id));
            
        }
        
        #endregion
    }
}