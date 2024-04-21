using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {

        private readonly IManagerAdapter _managerAdapter;
        public ManagerController(IManagerAdapter managerAdapter)
        {
            _managerAdapter = managerAdapter;
        }

        public IActionResult GetAllManagers()
        {
           return Ok(_managerAdapter.GetAllManagers());
        }
    }
}
