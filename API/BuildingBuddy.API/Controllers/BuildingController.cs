using Adapter.CustomExceptions;
using BuildingBuddy.API.Filters;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.BuildingRequests;
using WebModel.Responses.BuildingResponses;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/buildings")]
    [ApiController]
    [CustomExceptionFilter]
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
            return Ok(_buildingAdapter.GetAllBuildings(userId));
            
        }
        
        #endregion

        #region Get Building By Id
        
        [HttpGet]
        [Route("{buildingId:Guid}")]
        public IActionResult GetBuildingById([FromRoute] Guid buildingId)
        { 
            return Ok(_buildingAdapter.GetBuildingById(buildingId));
        }
        
        #endregion

        #region Update Building 
        
        [HttpPut]
        [Route("{buildingId:Guid}")]
        public IActionResult UpdateBuildingById([FromRoute] Guid buildingId, [FromBody] UpdateBuildingRequest buildingWithUpdates)
        {
           
                _buildingAdapter.UpdateBuildingById(buildingId, buildingWithUpdates);
                return NoContent();
            
        }
        
        #endregion
        
        #region Create Building

        [HttpPost]
        public IActionResult CreateBuilding([FromBody] CreateBuildingRequest request)
        {
          
                CreateBuildingResponse response = _buildingAdapter.CreateBuilding(request);
                return CreatedAtAction(nameof(CreateBuilding), new { id = response.Id }, response);
            
        }
        
        #endregion

        #region Delete Building

        [HttpDelete]
        [Route("{buildingId:Guid}")]
        public IActionResult DeleteBuildingById([FromRoute] Guid buildingId)
        {
            _buildingAdapter.DeleteBuildingById(buildingId);
            return NoContent();
        }
        
        #endregion
    }
}