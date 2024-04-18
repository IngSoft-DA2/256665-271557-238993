using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBuddy.API.Controllers
{
    public class FlatController : Controller
    {
        #region Constructor and atributes

        private readonly IFlatAdapter _flatAdapter;

        public FlatController(IFlatAdapter flatAdapter)
        {
            _flatAdapter = flatAdapter;
        }

        #endregion

        #region GetAllFlats

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

        #region CreateFlat

        [HttpPost]
        public IActionResult CreateFlat([FromBody] Guid idOfBuilding)
        {
            try
            {
                return Ok(_flatAdapter.CreateFlat(idOfBuilding));
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

        #endregion
    }
}
