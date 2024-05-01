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
        
        #region CreateFlat

        [HttpPost]
        public IActionResult CreateFlat([FromBody] CreateFlatRequest flatToCreate)
        {
            try
            {
                _flatAdapter.CreateFlat(flatToCreate);
                return Accepted(new {message = "Flat accepted, and it will be processed on the building creation stage"});
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