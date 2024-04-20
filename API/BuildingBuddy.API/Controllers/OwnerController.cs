using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(_ownerAdapter.GetOwners());
        }

        #endregion

    }
}
