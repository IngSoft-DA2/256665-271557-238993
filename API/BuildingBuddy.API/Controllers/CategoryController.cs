using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.CategoryRequests;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryAdapter _categoryAdapter;

        public CategoryController(ICategoryAdapter categoryAdapterg)
        {
            _categoryAdapter = categoryAdapter;
        }
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

        [HttpPost]
        public IActionResult CreateCategory([FromBody] CreateCategoryRequest categoryToCreate)
        {
            try
            {
                return Ok(_categoryAdapter.CreateCategory(categoryToCreate));
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
    }
}
