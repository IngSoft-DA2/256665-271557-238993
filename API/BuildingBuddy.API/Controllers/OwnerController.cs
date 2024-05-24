using BuildingBuddy.API.Filters;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.OwnerRequests;
using WebModel.Responses.OwnerResponses;

namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
    [AuthenticationFilter(["Manager"])]
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
           
            return Ok(_ownerAdapter.GetAllOwners());
            
        }

        #endregion

        #region CreateOwner

        [HttpPost]
        public IActionResult CreateOwner([FromBody] CreateOwnerRequest createOwnerRequest)
        {
           
            CreateOwnerResponse response = _ownerAdapter.CreateOwner(createOwnerRequest);
            return CreatedAtAction(nameof(CreateOwner), new { id = response.Id }, response);
          
        }

        #endregion

        #region UpdateOwner

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateOwnerById([FromRoute] Guid id, [FromBody] UpdateOwnerRequest updateOwnerRequest)
        {
           
            _ownerAdapter.UpdateOwnerById(id, updateOwnerRequest);
            return NoContent();
           
        }

        #endregion

        #region Get owner by Id

        [HttpGet]
        [Route("{ownerId:Guid}")]
        public IActionResult GetOwnerById(Guid ownerId)
        {
           
            return Ok(_ownerAdapter.GetOwnerById(ownerId));
           
        }

        #endregion
    }
}