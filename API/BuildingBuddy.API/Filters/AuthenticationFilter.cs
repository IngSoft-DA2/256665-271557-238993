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
            ObjectResult objectResult =  new ObjectResult("Authorization header is required");
            objectResult.StatusCode = 401;
            context.Result = objectResult;
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
                    
                    ObjectResult objectResult =  new ObjectResult("Session is not valid");
                    objectResult.StatusCode = 401;
                    context.Result = objectResult;
                }
                //Here ends Authorization, now we check if the user has the required roles (Authentication)
                
                SystemUserRoleEnum userRole = sessionService.GetUserRoleBySessionString(sessionStringOfUser);
                
                if (!_roles.Contains(userRole))
                {
                    ObjectResult objectResult =  new ObjectResult("Access denied");
                    objectResult.StatusCode = 403;
                    context.Result = objectResult;
                }
                context.HttpContext.Items.Add("UserRole", userRole);
            }
            catch (Exception)
            {
                ObjectResult objectResult =  new ObjectResult("Invalid authorization token");
                objectResult.StatusCode = 401;
                context.Result = objectResult;
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}