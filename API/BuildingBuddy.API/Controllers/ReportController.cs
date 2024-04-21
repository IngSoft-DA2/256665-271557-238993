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
            return Ok(_reportAdapter.GetRequestsByBuilding(buildingId, getMaintenanceReportRequestByBuilding));
        }
        
        #endregion
    }
}
