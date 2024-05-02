using IServiceLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuildingBuddy.API;

public class AuthenticationFilter : Attribute, IAuthorizationFilter

{
    private readonly ISessionService _sessionService;

    public AuthenticationFilter(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public virtual void OnAuthorization(AuthorizationFilterContext context)
    {
        var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
        Guid sessionString = Guid.Empty;

        if (string.IsNullOrEmpty(authorizationHeader) || !Guid.TryParse(authorizationHeader, out sessionString))
        {
            context.Result = new ObjectResult(new { Message = "Authorization header is missing" })
            {
                StatusCode = 401
            };
        }
        else
        {
            var currentUser = _sessionService.GetCurrentUser(sessionString);

            if (currentUser == null)
            {
                context.Result = new ObjectResult(new { Message = "Unauthorized" })
                {
                    StatusCode = 403
                };
            }
        }
    }
}