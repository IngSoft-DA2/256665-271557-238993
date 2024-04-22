using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.OwnerRequests;
using WebModel.Responses.OwnerResponses;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/owners")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        #region Constructor and atributes

        private readonly IOwnerAdapter _ownerAdapter;

        public OwnerController(IOwnerAdapter ownerAdapter)
        {
            _ownerAdapter = ownerAdapter;
        }

        #endregion

        #region GetAllOwners

        [HttpGet]
        public IActionResult GetAllOwners()
        {
            try
            {
                return Ok(_ownerAdapter.GetAllOwners());
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion
        
        #region CreateOwner
        
        [HttpPost]
        public IActionResult CreateOwner([FromBody] CreateOwnerRequest createOwnerRequest)
        {
            try
            {
                CreateOwnerResponse response = _ownerAdapter.CreateOwner(createOwnerRequest);
                return CreatedAtAction(nameof(CreateOwner), new { id = response.Id }, response);
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
        
        #region UpdateOwner
        
        [HttpPut("{id:Guid}")]
        public IActionResult UpdateOwner([FromRoute] Guid id, [FromBody] UpdateOwnerRequest updateOwnerRequest)
        {
            try
            {
                _ownerAdapter.UpdateOwner(id, updateOwnerRequest);
                return NoContent();
            }
            catch (ObjectErrorAdapterException exceptionCaught)
            {
                return BadRequest(exceptionCaught.Message);
            }
            catch(ObjectNotFoundAdapterException exceptionCaught)
            {
                return NotFound("The specific owner was not found in Database");
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
