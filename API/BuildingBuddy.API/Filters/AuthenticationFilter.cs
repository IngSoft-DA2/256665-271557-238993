using Domain;
using IServiceLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuildingBuddy.API.Filters;

public class AuthenticationFilter : Attribute, IActionFilter
{
    private readonly List<string> _roles;

    public AuthenticationFilter(string[] roles)
    {
        _roles = new List<string>(roles);
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        string authHeader = context.HttpContext.Request.Headers["Authorization"];

        if (authHeader is null)
        {
            context.Result = new UnauthorizedObjectResult("Authorization header is required");
        }
        else
        {
            try
            {
                Guid userToken = Guid.Parse(authHeader);
                var sessionManagerObject = context.HttpContext.RequestServices.GetService(typeof(ISessionService))!;
           
                ISessionService sessionService = sessionManagerObject as ISessionService;
                
                SystemUser? systemUser = sessionService.GetCurrentUser(userToken);
                if (systemUser is null)
                {
                    context.Result = new UnauthorizedObjectResult("User was not found");
                }
                
                else if (!_roles.Contains(systemUser.Role))
                {
                    context.Result = new ForbidResult("Access denied");
                    context.HttpContext.Items.Add("UserId", systemUser.Id);
                }
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedObjectResult("Invalid authorization token");
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}