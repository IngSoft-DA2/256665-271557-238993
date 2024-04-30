using IServiceLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuildingBuddy.API.Filters;

public class AuthenticationFilter : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string header = context.HttpContext.Request.Headers["Authorization"];
        if (header is null)
        {
            context.Result = new ObjectResult("Authorization header is required.")
            {
                StatusCode = 401
            };
        }
        else
        {
            try 
            {
                Guid sessionString = Guid.Parse(header);
                VerifySessionString(sessionString, context);
            }
            catch (FormatException)
            {
                context.Result = new ObjectResult("Invalid token format.")
                {
                    StatusCode = 401
                };
            }
        }
    }

    private void VerifySessionString(Guid headerValidationString, AuthorizationFilterContext context)
    {
        var sessionService = GetSessionService(context);
        if (!sessionService.IsValidToken(headerValidationString))
        {
            context.Result = new ObjectResult("Invalid token.")
            {
                StatusCode = 401
            };
        }
        else
        {
            var user = sessionService.GetUserBySessionString(headerValidationString);
            context.HttpContext.Items.Add("user", user);
        }
    }

    private ISessionService GetSessionService(AuthorizationFilterContext context)
    {
        return context.HttpContext.RequestServices.GetService(typeof(ISessionService)) as ISessionService;
    }
}