using IAdapter;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBuddy.API.Controllers
{
    public class FlatController : Controller
    {
        private readonly IFlatAdapter _flatAdapter;

        public FlatController(IFlatAdapter flatAdapter)
        {
            _flatAdapter = flatAdapter;
        }
        public IActionResult GetAllFlats()
        {
            return Ok(_flatAdapter.GetAllFlats());
        }
    }
}
