using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.ReportRequests;

namespace BuildingBuddy.API.Controllers
{
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
        public IActionResult GetMaintenanceRequestsByBuilding([FromBody] GetMaintenanceReportByBuildingRequest getMaintenanceReportRequestByBuilding)
        {
            try
            {
                return Ok(_reportAdapter.GetMaintenanceRequestsByBuilding(getMaintenanceReportRequestByBuilding));
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        #endregion

        #region  GetMaintenanceRequestsByRequestHandler
        
        [Route("/request-handler/maintenance-requests/reports")]
        
        [HttpGet]
        public IActionResult GetMaintenanceRequestsByRequestHandler([FromBody] GetMaintenanceReportRequest getMaintenanceReportRequestByRequestHandler)
        {
            try{
                return Ok(_reportAdapter.GetMaintenanceRequestsByRequestHandler(getMaintenanceReportRequestByRequestHandler));
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        #endregion

        #region  GetMaintenanceRequestsByCategory
        
        [Route("/categories/maintenance-requests/reports")]
        [HttpGet]
        public IActionResult GetMaintenanceRequestsByCategory([FromQuery] Guid categoryId, [FromBody] GetMaintenanceReportRequest getMaintenanceReportRequestByCategory)
        {
            try
            {
                return Ok(_reportAdapter.GetMaintenanceRequestsByCategory(categoryId, getMaintenanceReportRequestByCategory));
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
