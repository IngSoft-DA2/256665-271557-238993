using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using WebModel.Requests.CategoryRequests;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/categories")]
    [ApiController]
    [CustomExceptionFilter]
    public class CategoryController : ControllerBase
    {
        private ICategoryAdapter _categoryAdapter;

        public CategoryController(ICategoryAdapter categoryAdapter)
        {
            _categoryAdapter = categoryAdapter;
        }
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(_categoryAdapter.GetAllCategories());
        }
        
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetCategoryById([FromRoute] Guid id)
        {
          
            return Ok(_categoryAdapter.GetCategoryById(id));
           
            
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CreateCategoryRequest categoryToCreate)
        {
            return Ok(_categoryAdapter.CreateCategory(categoryToCreate));
        }
    }
}
