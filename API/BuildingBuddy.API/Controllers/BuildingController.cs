using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/buildings")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingAdapter _buildingAdapter;
        public BuildingController(IBuildingAdapter buildingAdapter)
        {
            _buildingAdapter = buildingAdapter;
        }


        [HttpGet]
        public IActionResult GetBuildings([FromQuery] Guid userId)
        {
            try
            {
                return Ok(_buildingAdapter.GetBuildings(userId));
            }
            catch (ObjectNotFoundException)
            {
                return NotFound("User id was not found in database");
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
            
        }
    }
}
