using Adapter.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuildingBuddy.API.Filters;

public class CustomExceptionFilter : ExceptionFilterAttribute
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is ObjectNotFoundAdapterException)
        {
            context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
            {
                StatusCode = 404
            };
        } else if (context.Exception is ObjectErrorAdapterException)
        {
            context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
            {
                StatusCode = 400
            };
        }
        
        else if (context.Exception is Exception)
        {
            context.Result = new ObjectResult(new { ErrorMessage = $"Something went wrong. See: {context.Exception.Message}" })
            {
                StatusCode = 500
            };
        }
    }
}