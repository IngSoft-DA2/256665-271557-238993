using BuildingBuddy.API.Filters;
using Domain.Enums;
using IAdapter;
using Microsoft.AspNetCore.Authorization;
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
        #region Constructor And Dependency Injector

        private IConstructionCompanyAdminAdapter _constructionCompanyAdminAdapter;

        public ConstructionCompanyAdminController(IConstructionCompanyAdminAdapter constructionCompanyAdminAdapter)
        {
            _constructionCompanyAdminAdapter = constructionCompanyAdminAdapter;
        }

        #endregion

        #region Create Construction Company Admin

        [HttpPost]
        public IActionResult CreateConstructionCompanyAdmin(
            [FromBody] CreateConstructionCompanyAdminRequest createRequest)
        {
            if (createRequest.UserRole != null && createRequest.UserRole != SystemUserRoleEnum.ConstructionCompanyAdmin)
            {
                var notAllowedResponse = new ObjectResult("Only ConstructionCompanyAdmin people are allowed.")
                {
                    StatusCode = 403
                };
                return notAllowedResponse;
            }

            CreateConstructionCompanyAdminResponse response =
                _constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(createRequest, createRequest.UserRole);
            return CreatedAtAction(nameof(CreateConstructionCompanyAdmin), new { id = response.Id }, response);
        }

        #endregion
    }
}