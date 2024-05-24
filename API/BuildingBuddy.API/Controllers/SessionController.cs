using BuildingBuddy.API.Filters;
using IServiceLogic;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.SessionRequests;

namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
    [Route("api/v1/sessions")]
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
            
            return Ok(sessionString);
        }

        [ServiceFilter(typeof(AuthenticationFilter))]
        [HttpDelete]
        public IActionResult Logout([FromHeader] Guid sessionId)
        {
            _sessionService.Logout(sessionId);
            return Ok();
        }
    }

}