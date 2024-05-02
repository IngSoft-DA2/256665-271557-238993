using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebModel.Responses.ReportResponses;

namespace BuildingBuddy.API.Controllers
{
    [CustomExceptionFilter]
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

        [Route("/buildings/maintenance-requests/reports")]
        [HttpGet]
        public IActionResult GetMaintenanceRequestsByBuilding([FromQuery] Guid personId, [FromQuery] Guid buildingId)
        {
          
            return Ok(_reportAdapter.GetMaintenanceReportByBuilding(personId, buildingId));
          
        }
        
        #endregion

        #region  GetMaintenanceRequestsByRequestHandler
        
        [Route("/request-handler/maintenance-requests/reports")]
        [HttpGet]
        public IActionResult GetMaintenanceRequestsByRequestHandler([FromQuery] Guid requestHandlerId, [FromQuery] Guid buildingId, [FromQuery] Guid personId)
        {
            return Ok(_reportAdapter.GetMaintenanceReportByRequestHandler(requestHandlerId, buildingId, personId));
            
        }
        
        #endregion

        #region  GetMaintenanceRequestsByCategory
        
        [Route("/categories/maintenance-requests/reports")]
        [HttpGet]
        public IActionResult GetMaintenanceRequestsByCategory([FromQuery] Guid buildingId, [FromQuery] Guid categoryId)
        {
           
            return Ok(_reportAdapter.GetMaintenanceReportByCategory(buildingId, categoryId));
            
        }
        
        #endregion
        
    }
}
