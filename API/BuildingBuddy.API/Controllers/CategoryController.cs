using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.CategoryRequests;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/categories")]
    [ApiController]
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
            try
            {
                return Ok(_categoryAdapter.GetAllCategories());
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetCategoryById([FromRoute] Guid id)
        {
            try
            {
                return Ok(_categoryAdapter.GetCategoryById(id));
            }
            catch (ObjectNotFoundAdapterException)
            {
                return NotFound("Category was not found in database");
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
            
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CreateCategoryRequest categoryToCreate)
        {
            try
            {
                return Ok(_categoryAdapter.CreateCategory(categoryToCreate));
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
    }
}
