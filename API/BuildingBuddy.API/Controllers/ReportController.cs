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

        [Route("/maintenanceReportByBuilding")]
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

        #region  GetReportsByJanitor
        
        [Route("/maintenanceReportByMaintenancePerson")]
        
        [HttpGet]
        public IActionResult GetRequestsByJanitor([FromQuery] Guid maintenancePersonId, [FromBody] GetMaintenanceReportRequest getMaintenanceReportRequestByJanitor)
        {
            try{
                return Ok(_reportAdapter.GetRequestsByJanitor(maintenancePersonId, getMaintenanceReportRequestByJanitor));
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        #endregion
        
        [Route("/maintenanceReportByCategory")]
        [HttpGet]
        public IActionResult GetRequestsByCategory([FromQuery] Guid categoryId, [FromBody] GetMaintenanceReportRequest getMaintenanceReportRequestByCategory)
        {
            try
            {
                return Ok(_reportAdapter.GetRequestsByCategory(categoryId, getMaintenanceReportRequestByCategory));
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
