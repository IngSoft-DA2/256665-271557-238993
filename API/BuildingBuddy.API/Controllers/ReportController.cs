using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.ReportRequests;
using WebModel.Responses.ReportResponses;

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
        public IActionResult GetMaintenanceRequestsByBuilding([FromQuery] Guid buildingId)
        {
            try
            {
                return Ok(_reportAdapter.GetMaintenanceRequestsByBuilding(buildingId));
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
        public IActionResult GetMaintenanceRequestsByRequestHandler([FromQuery] Guid requestHandlerId)
        {
            try{
                return Ok(_reportAdapter.GetMaintenanceRequestsByRequestHandler(requestHandlerId));
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
        public IActionResult GetMaintenanceRequestsByCategory([FromQuery] Guid categoryId)
        {
            try
            {
                return Ok(_reportAdapter.GetMaintenanceRequestsByCategory(categoryId));
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
