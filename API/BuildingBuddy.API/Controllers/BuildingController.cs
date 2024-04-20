using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.BuildingRequests;
using WebModel.Responses.BuildingResponses;

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

        [HttpGet]
        [Route("{buildingId:Guid}")]
        public IActionResult GetBuildingById([FromRoute] Guid buildingId)
        {
            try
            {
                return Ok(_buildingAdapter.GetBuildingById(buildingId));
            }
            catch (ObjectNotFoundException)
            {
                return NotFound("Building was not found in database");
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [Route("{buildingId:Guid}")]
        public IActionResult UpdateBuilding([FromRoute] Guid buildingId,
            [FromBody] UpdateBuildingRequest buildingWithUpdates)
        {
            try
            {
                _buildingAdapter.UpdateBuilding(buildingId, buildingWithUpdates);
                return NoContent();
            }
            catch (ObjectNotFoundException)
            {
                return NotFound("Building was not found in database");
            }
            catch (ObjectErrorException exceptionCaught)
            {
                return BadRequest(exceptionCaught.Message);
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        public IActionResult CreateBuilding([FromBody] CreateBuildingRequest request)
        {
            CreateBuildingResponse response = _buildingAdapter.CreateBuilding(request);
            return CreatedAtAction(nameof(CreateBuilding), new { id = response.Id }, response);
        }
    }
}