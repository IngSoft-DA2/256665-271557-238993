using BuildingBuddy.API.Filters;
using Domain.Enums;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.AdministratorRequests;
using WebModel.Responses.AdministratorResponses;

namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
    [AuthenticationFilter(SystemUserRoleEnum.Admin)]
    [Route("api/v2/administrators")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        #region Constructor and attributes

        private readonly IAdministratorAdapter _administratorAdapter;

        public AdministratorController(IAdministratorAdapter administratorAdapter)
        {
            _administratorAdapter = administratorAdapter;
        }

        #endregion

        #region Create Administrator

        [HttpPost]
        public IActionResult CreateAdministrator([FromBody] CreateAdministratorRequest request)
        {
            CreateAdministratorResponse response = _administratorAdapter.CreateAdministrator(request);
            return CreatedAtAction(nameof(CreateAdministrator), new { id = response.Id }, response);
        }

        #endregion
    }
}