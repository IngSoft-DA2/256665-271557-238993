using BuildingBuddy.API.Filters;
using IAdapter;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
    [Route("api/v1/")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        #region Constructor and atributes

        private readonly IReportAdapter _reportAdapter;

        public ReportController(IReportAdapter reportAdapter)
        {
            _reportAdapter = reportAdapter;
        }

        #endregion

        #region GetMaintenanceRequestsByBuilding

        [AuthenticationFilter(["Manager"])]
        [Route("/buildings/maintenance-requests/reports")]
        [HttpGet]
        public IActionResult GetMaintenanceRequestsByBuilding([FromQuery] Guid personId, [FromQuery] Guid buildingId)
        {
          
            return Ok(_reportAdapter.GetMaintenanceReportByBuilding(personId, buildingId));
          
        }
        
        #endregion

        #region  GetMaintenanceRequestsByRequestHandler
        
        [AuthenticationFilter(["Manager"])]
        [Route("/request-handler/maintenance-requests/reports")]
        [HttpGet]
        public IActionResult GetMaintenanceRequestsByRequestHandler([FromQuery] Guid requestHandlerId, [FromQuery] Guid buildingId, [FromQuery] Guid personId)
        {
            return Ok(_reportAdapter.GetMaintenanceReportByRequestHandler(requestHandlerId, buildingId, personId));
            
        }
        
        #endregion

        #region  GetMaintenanceRequestsByCategory
        
        [AuthenticationFilter(["Admin"])]
        [Route("/categories/maintenance-requests/reports")]
        [HttpGet]
        public IActionResult GetMaintenanceRequestsByCategory([FromQuery] Guid buildignId, [FromQuery] Guid categoryId)
        {
           
            return Ok(_reportAdapter.GetMaintenanceReportByCategory(buildignId, categoryId));
            
        }
        
        #endregion
        
    }
}
