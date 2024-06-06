using BuildingBuddy.API.Filters;
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
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        
        [HttpPost]
        public IActionResult Login([FromBody] SystemUserLoginRequest userLoginModel)
        {
            Guid sessionString = _sessionService.Authenticate(userLoginModel.Email, userLoginModel.Password);
            string userRole = _sessionService.GetUserRoleBySessionString(sessionString).ToString();
            
            LoginResponse loginResponse = new LoginResponse
            {
                SessionString = sessionString,
                UserRole = userRole
            };
            
            return Ok(loginResponse);
        }
        
        [HttpDelete]
        public IActionResult Logout([FromHeader] Guid sessionId)
        {
            _sessionService.Logout(sessionId);
            return NoContent();
        }
    }

}