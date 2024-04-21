using IAdapter;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceAdapter _maintenanceAdapter;

        public MaintenanceController(IMaintenanceAdapter maintenanceAdapter)
        {
            _maintenanceAdapter = maintenanceAdapter;
        }

        [HttpGet]
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
    }
}