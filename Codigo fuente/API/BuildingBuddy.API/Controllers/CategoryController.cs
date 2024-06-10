using BuildingBuddy.API.Filters;
using Domain.Enums;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.CategoryRequests;
using WebModel.Responses.CategoryResponses;

namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
    [Route("api/v2/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Constructor and Dependency Injector

        private ICategoryAdapter _categoryAdapter;

        public CategoryController(ICategoryAdapter categoryAdapter)
        {
            _categoryAdapter = categoryAdapter;
        }

        #endregion

        #region Get All Categories

        [HttpGet]
        [AuthenticationFilter(SystemUserRoleEnum.Admin, SystemUserRoleEnum.Manager, SystemUserRoleEnum.RequestHandler)]
        public IActionResult GetAllCategories()
        {
            return Ok(_categoryAdapter.GetAllCategories());
        }

        #endregion

        #region Get Category By Id

        [HttpGet]
        [Route("{id:Guid}")]
        [AuthenticationFilter(SystemUserRoleEnum.Admin, SystemUserRoleEnum.Manager, SystemUserRoleEnum.RequestHandler)]
        public IActionResult GetCategoryById([FromRoute] Guid id)
        {
            return Ok(_categoryAdapter.GetCategoryById(id));
        }

        #endregion

        #region Create Category

        [HttpPost]
        [AuthenticationFilter(SystemUserRoleEnum.Admin)]
        public IActionResult CreateCategory([FromBody] CreateCategoryRequest categoryToCreate)
        {
            CreateCategoryResponse response = _categoryAdapter.CreateCategory(categoryToCreate);

            return CreatedAtAction(nameof(CreateCategory), new { id = response.Id }, response);
        }

        #endregion
    }
}