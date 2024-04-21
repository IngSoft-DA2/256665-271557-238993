using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.ReportRequests;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/reports")]
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

        #region GetReportsByBuilding

        [HttpGet]
        public IActionResult GetRequestsByBuilding([FromQuery] Guid buildingId, [FromBody] GetMaintenanceReportRequest getMaintenanceReportRequestByBuilding)
        {
            try
            {
                return Ok(_reportAdapter.GetRequestsByBuilding(buildingId, getMaintenanceReportRequestByBuilding));
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        #endregion
        
        public IActionResult GetRequestsByJanitor([FromQuery] Guid janitorId, [FromBody] GetMaintenanceReportRequest getMaintenanceReportRequestByJanitor)
        {
            try{
                return Ok(_reportAdapter.GetRequestsByJanitor(janitorId, getMaintenanceReportRequestByJanitor));
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
