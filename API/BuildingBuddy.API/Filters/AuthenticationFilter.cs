using Domain;
using Domain.Enums;
using IServiceLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuildingBuddy.API.Filters;

public class AuthenticationFilter : Attribute, IActionFilter
{
    private readonly List<SystemUserRoleEnum> _roles;

    public AuthenticationFilter(params SystemUserRoleEnum[] roles)
    {
        _roles = roles.ToList();
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
                Guid sessionStringOfUser = Guid.Parse(authHeader);
                var sessionManagerObject = context.HttpContext.RequestServices.GetService(typeof(ISessionService))!;
           
                ISessionService sessionService = sessionManagerObject as ISessionService;
                
                if(sessionService.IsSessionValid(sessionStringOfUser) == false) 
                {
                    context.Result = new UnauthorizedObjectResult("Session is not valid");
                }
                //Here ends Authorization, now we check if the user has the required roles (Authentication)
                
                SystemUserRoleEnum userRole = sessionService.GetUserRoleBySessionString(sessionStringOfUser);
                
                if (!_roles.Contains(userRole))
                {
                    context.Result = new ForbidResult("Access denied");
                }
                context.HttpContext.Items.Add("UserRole", userRole);
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