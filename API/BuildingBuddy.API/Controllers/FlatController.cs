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

        #region GetAllFlats
        //INCLUIR EN BUILDING Y QUITAR DE AQUI
        [HttpGet]
        public IActionResult GetAllFlats()
        {
            try
            {
                return Ok(_flatAdapter.GetAllFlats());
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion

        #region GetFlatById

        [HttpGet("{idOfFlatToFind}")]
        public IActionResult GetFlatById([FromRoute] Guid idOfFlatToFind)
        {
            try
            {
                return Ok(_flatAdapter.GetFlatById(idOfFlatToFind));
            }
            catch (ObjectNotFoundException exceptionCaught)
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

        //INCLUIR EN BUILDING Y QUITAR DE AQUI
        #region CreateFlat

        [HttpPost]
        public IActionResult CreateFlat([FromBody] CreateFlatRequest flatToCreate)
        {
            try
            {
                return Ok(_flatAdapter.CreateFlat(flatToCreate));
            }
            catch (ObjectErrorException exceptionCaught)
            {
                return BadRequest(exceptionCaught.Message);
            }
            catch (ObjectNotFoundException)
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