using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.BuildingRequests;
using WebModel.Responses.BuildingResponses;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/buildings")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        
        #region Constructor and atributes
        
        private readonly IBuildingAdapter _buildingAdapter;

        public BuildingController(IBuildingAdapter buildingAdapter)
        {
            _buildingAdapter = buildingAdapter;
        }

        #endregion

        #region Get All Buildings
        
        [HttpGet]
        public IActionResult GetAllBuildings([FromQuery] Guid userId)
        {
            try
            {
                return Ok(_buildingAdapter.GetAllBuildings(userId));
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("User id was not found in database");
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        #endregion

        #region Get Building By Id
        
        [HttpGet]
        [Route("{buildingId:Guid}")]
        public IActionResult GetBuildingById([FromRoute] Guid buildingId)
        {
            try
            {
                return Ok(_buildingAdapter.GetBuildingById(buildingId));
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("Building was not found in database");
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        #endregion

        #region Update Building 
        
        [HttpPut]
        [Route("{buildingId:Guid}")]
        public IActionResult UpdateBuildingById([FromRoute] Guid buildingId, [FromBody] UpdateBuildingRequest buildingWithUpdates)
        {
            try
            {
                _buildingAdapter.UpdateBuildingById(buildingId, buildingWithUpdates);
                return NoContent();
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("Building was not found in database");
            }
            catch (ObjectErrorAdapterException exceptionCaught)
            {
                return BadRequest(exceptionCaught.Message);
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        #endregion
        
        #region Create Building

        [HttpPost]
        public IActionResult CreateBuilding([FromBody] CreateBuildingRequest request)
        {
            try
            {
                CreateBuildingResponse response = _buildingAdapter.CreateBuilding(request);
                return CreatedAtAction(nameof(CreateBuilding), new { id = response.Id }, response);
            }
            catch (ObjectErrorAdapterException exceptionCaught)
            {
                return BadRequest(exceptionCaught.Message);
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        #endregion

        #region Delete Building

        [HttpDelete]
        [Route("{buildingId:Guid}")]
        public IActionResult DeleteBuildingById([FromRoute] Guid buildingId)
        {
            try
            {
                _buildingAdapter.DeleteBuildingById(buildingId);
                return NoContent();
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("Building was not found in database");
            }
            catch (Exception exceptionCaugth)
            {
                Console.WriteLine(exceptionCaugth.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        #endregion
    }
}