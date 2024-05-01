using IServiceLogic;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.SessionRequests;


namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [CustomExceptionFilter]
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
            return Ok(new { sessionId = sessionString });
        }
    }

}