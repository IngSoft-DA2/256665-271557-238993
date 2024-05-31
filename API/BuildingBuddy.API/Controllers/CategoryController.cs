using BuildingBuddy.API.Filters;
using Domain.Enums;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.CategoryRequests;
using WebModel.Responses.CategoryResponses;

namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
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
            CreateCategoryResponse response = _categoryAdapter.CreateCategory(categoryToCreate);
            return CreatedAtAction(nameof(CreateCategory), new { id = response.Id }, response);
        }
    }
}