using BuildingBuddy.API.Filters;
using Domain.Enums;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.ConstructionCompanyAdminRequests;
using WebModel.Responses.ConstructionCompanyAdminResponses;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v2/ConstructionCompanyAdmins")]
    [ExceptionFilter]
    [ApiController]
    public class ConstructionCompanyAdminController : ControllerBase
    {
        private IConstructionCompanyAdminAdapter _constructionCompanyAdminAdapter;

        public ConstructionCompanyAdminController(IConstructionCompanyAdminAdapter constructionCompanyAdminAdapter)
        {
            _constructionCompanyAdminAdapter = constructionCompanyAdminAdapter;
        }

        [HttpPost]
        public IActionResult CreateConstructionCompanyAdmin(
            [FromBody] CreateConstructionCompanyAdminRequest createRequest)
        {
            SystemUserRoleEnum? userRole = null;

            if (HttpContext.Items.TryGetValue("UserRole", out var userRoleObj))
            {
                if (userRoleObj is SystemUserRoleEnum enumValue) userRole = enumValue;
            }

            if (userRole != null && userRole != SystemUserRoleEnum.ConstructionCompanyAdmin)
            {
                var notAllowedResponse = new ObjectResult("Only ConstructionCompanyAdmin people are allowed.")
                {
                    StatusCode = 403
                };
                return notAllowedResponse;
            }

            CreateConstructionCompanyAdminResponse response =
                _constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(createRequest, userRole);
            return CreatedAtAction(nameof(CreateConstructionCompanyAdmin), new { id = response.Id }, response);
        }
    }
}