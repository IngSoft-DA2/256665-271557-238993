using BuildingBuddy.API.Filters;
using Domain.Enums;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.ConstructionCompanyAdminRequests;
using WebModel.Responses.ConstructionCompanyAdminResponses;
using WebModel.Responses.ConstructionCompanyResponses;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v2/ConstructionCompanyAdmins")]
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

            if (HttpContext.Items.TryGetValue("UserRole", out var userRoleObj) && userRoleObj is string userRoleStr)
            {
                if (Enum.TryParse(userRoleStr, out SystemUserRoleEnum parsedUserRole))
                {
                    if (parsedUserRole != SystemUserRoleEnum.ConstructionCompanyAdmin)
                    {
                        return Forbid("Only ConstructionCompanyAdmin people is allowed.");
                    }
                    userRole = parsedUserRole;
                }
                else return Unauthorized();
            }

            CreateConstructionCompanyAdminResponse response = _constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(createRequest, userRole);
            return CreatedAtAction(nameof(CreateConstructionCompanyAdmin), new { id = response.Id }, response);
        }
    }
}