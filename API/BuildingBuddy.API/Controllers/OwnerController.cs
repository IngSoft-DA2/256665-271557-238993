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
        public IActionResult GetOwners()
        {
            try
            {
                return Ok(_ownerAdapter.GetOwners());
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
        
        #region ModifyOwner
        
        [HttpPut("{id}")]
        public IActionResult UpdateOwner([FromRoute] Guid id, [FromBody] UpdateOwnerRequest updateOwnerRequest)
        {
            try
            {
                _ownerAdapter.UpdateOwner(id, updateOwnerRequest);
                return NoContent();
            }
            catch (ObjectErrorException exceptionCaught)
            {
                return BadRequest(exceptionCaught.Message);
            }
            catch(ObjectNotFoundException exceptionCaught)
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
