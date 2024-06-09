using BuildingBuddy.API.Filters;
using Domain;
using Domain.Enums;
using IServiceLogic;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.SessionRequests;
using WebModel.Responses.LoginResponses;

namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
    [Route("api/v2/sessions")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        #region Constructor and Dependency Injector

        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        #endregion

        #region Login

        [HttpPost]
        public IActionResult Login([FromBody] SystemUserLoginRequest userLoginModel)
        {
            Session sessionForUser = _sessionService.Authenticate(userLoginModel.Email, userLoginModel.Password);

            LoginResponse loginResponse = new LoginResponse
            {
                UserId = sessionForUser.UserId,
                SessionString = sessionForUser.SessionString,
                UserRole = sessionForUser.UserRole
            };

            return Ok(loginResponse);
        }

        #endregion

        #region Logout

        [HttpDelete]
        [AuthenticationFilter(SystemUserRoleEnum.Admin, SystemUserRoleEnum.Manager, SystemUserRoleEnum.RequestHandler,
            SystemUserRoleEnum.ConstructionCompanyAdmin)]
        public IActionResult Logout([FromHeader(Name = "Authorization")] Guid sessionId)
        {
            _sessionService.Logout(sessionId);
            return NoContent();
        }

        #endregion
    }
}