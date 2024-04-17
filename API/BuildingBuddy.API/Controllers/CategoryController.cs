using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryAdapter _categoryAdapter;

        public CategoryController(ICategoryAdapter categoryAdapter)
        {
            _categoryAdapter = categoryAdapter;
        }
        public IActionResult GetAllCategories()
        {
            return Ok(_categoryAdapter.GetAllCategories());
        }
    }
}
