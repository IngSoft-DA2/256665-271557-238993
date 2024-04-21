using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.FlatRequests;

namespace BuildingBuddy.API.Controllers
{
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
            try
            {
                return Ok(_flatAdapter.GetAllFlats(buildingId));
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("Building id was not found");
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion
        
        #region GetFlatById

        [HttpGet("{id:Guid}")]
        public IActionResult GetFlatById([FromQuery] Guid buildingId, [FromRoute] Guid idOfFlatToFind)
        {
            try
            {
                return Ok(_flatAdapter.GetFlatById(buildingId,idOfFlatToFind));
            }
            catch (ObjectNotFoundAdapterException exceptionCaught)
            {
                return NotFound("Flat was not found, reload the page");
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion
        
        #region CreateFlat

        [HttpPost]
        public IActionResult CreateFlat([FromBody] CreateFlatRequest flatToCreate)
        {
            try
            {
                return Ok(_flatAdapter.CreateFlat(flatToCreate));
            }
            catch (ObjectErrorAdapterException exceptionCaught)
            {
                return BadRequest(exceptionCaught.Message);
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("Owner was not found in database");
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