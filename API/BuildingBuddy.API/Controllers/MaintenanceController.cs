using BuildingBuddy.API.Filters;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.MaintenanceRequests;
using WebModel.Responses.MaintenanceResponses;

namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
    [Route("api/v1/maintenance")]
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
        
        [AuthenticationFilter(["Manager"])]
        [HttpGet]
        [Route("requests")]
        public IActionResult GetAllMaintenanceRequests([FromQuery] Guid? managerId)
        {
            return Ok(_maintenanceAdapter.GetAllMaintenanceRequests(managerId));
        }

        #endregion

        #region Create Maintenance Request
        
        [AuthenticationFilter(["Manager"])]
        [HttpPost]
        public IActionResult CreateMaintenanceRequest([FromBody] CreateRequestMaintenanceRequest request)
        {
            CreateRequestMaintenanceResponse response = _maintenanceAdapter.CreateMaintenanceRequest(request);
            return CreatedAtAction(nameof(CreateMaintenanceRequest), new { id = response.Id }, response);

        }

        #endregion

        #region Get Maintenance Request By Category Id

        [HttpGet]
        [AuthenticationFilter(["Manager"])]
        [Route("/category/requests")]
        public IActionResult GetMaintenanceRequestByCategory([FromQuery] Guid categoryId)
        {
            return Ok(_maintenanceAdapter.GetMaintenanceRequestByCategory(categoryId));

        }

        #endregion

        #region Assign Maintenance Request
        
        [AuthenticationFilter(["Manager"])]
        [HttpPut]
        [Route("request-handler/requests")]
        public IActionResult AssignMaintenanceRequest([FromQuery] Guid idOfRequestToUpdate, [FromQuery] Guid idOfWorker)
        {

            _maintenanceAdapter.AssignMaintenanceRequest(idOfRequestToUpdate, idOfWorker);
            return NoContent();

        }

        #endregion

        #region Get Maintenance Requests By Request Handler
        
        [AuthenticationFilter(["Manager"])]
        [HttpGet]
        [Route("request-handler/{handlerId:Guid}/requests")]
        public IActionResult GetMaintenanceRequestByRequestHandler([FromRoute] Guid handlerId)
        {

            return Ok(_maintenanceAdapter.GetMaintenanceRequestsByRequestHandler(handlerId));

        }

        #endregion

        #region Update Maintenance Request
        
        [AuthenticationFilter(["RequestHandler"])]
        [HttpPut]
        [Route("requests/{id:Guid}")]
        public IActionResult UpdateMaintenanceRequestStatus([FromRoute] Guid id,
            [FromBody] UpdateMaintenanceRequestStatusRequest request)
        {

            _maintenanceAdapter.UpdateMaintenanceRequestStatus(id, request);
            return NoContent();

        }

        #endregion

        #region Get Maintenance Request By Id
        
        [AuthenticationFilter(["Manager"])]
        [HttpGet]
        [Route("requests/{id:Guid}")]
        public IActionResult GetMaintenanceRequestById(Guid id)
        {
            return Ok(_maintenanceAdapter.GetMaintenanceRequestById(id));

        }

        #endregion
    }
}