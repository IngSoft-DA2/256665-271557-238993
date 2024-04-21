using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/managers")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerAdapter _managerAdapter;

        public ManagerController(IManagerAdapter managerAdapter)
        {
            _managerAdapter = managerAdapter;
        }

        [HttpGet]
        public IActionResult GetAllManagers()
        {
            try
            {
                return Ok(_managerAdapter.GetAllManagers());
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete]
        [Route("{managerId:Guid}")]
        public IActionResult DeleteManagerById([FromRoute] Guid managerId)
        {
            _managerAdapter.DeleteManagerById(managerId);
            return NoContent();
        }
    }
}