using Adapter.CustomExceptions;
using BuildingBuddy.API.Filters;
using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.FlatRequests;

namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
    [AuthenticationFilter(["Manager"])]
    [Route("api/v1/flats")]
    [ApiController]
    public class FlatController : ControllerBase
    {
        #region Constructor and atributes

        private readonly IFlatAdapter _flatAdapter;

        public FlatController(IFlatAdapter flatAdapter)
        {
            _flatAdapter = flatAdapter;
        }

        #endregion
        
        #region Get All Flats
        
        [HttpGet]
        [Route("/flats")]
        public IActionResult GetAllFlats([FromRoute] Guid buildingId)
        {
           
            return Ok(_flatAdapter.GetAllFlats(buildingId));
            
        }

        #endregion
        
        #region GetFlatById

        [HttpGet("{id:Guid}")]
        public IActionResult GetFlatById([FromQuery] Guid buildingId, [FromRoute] Guid idOfFlatToFind)
        {
           
            return Ok(_flatAdapter.GetFlatById(buildingId,idOfFlatToFind));
            
        }

        #endregion
        
        #region CreateFlat

        [HttpPost]
        public IActionResult CreateFlat([FromBody] CreateFlatRequest flatToCreate)
        {
            
            _flatAdapter.CreateFlat(flatToCreate);
            return Accepted(new {message = "Flat accepted, and it will be processed on the building creation stage"});
            
        }

        #endregion
    }
}