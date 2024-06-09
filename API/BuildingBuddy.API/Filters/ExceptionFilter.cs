using System.Security.Authentication;
using Adapter.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuildingBuddy.API.Filters;

public class ExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is ObjectErrorAdapterException exceptionCaught)
        {
            context.Result = new BadRequestObjectResult(exceptionCaught.Message);
        }
        else if (context.Exception is ObjectNotFoundAdapterException exceptionNotFoundCaught)
        {
            context.Result = new NotFoundObjectResult(exceptionNotFoundCaught.Message);
        }
        else if (context.Exception is ObjectRepeatedAdapterException)
        {
            context.Result = new ConflictObjectResult("This already exists");
        }
        else if (context.Exception is InvalidCredentialException)
        {
            context.Result = new ObjectResult(new { Message = "Invalid credentials" }) { StatusCode = 401 };
        }
        else
        {
            context.Result = new ObjectResult(new { Message = "Internal server error" }) { StatusCode = 500 };
            // Log the exception (optional)
            Console.WriteLine(context.Exception.Message);
        }
        context.ExceptionHandled = true;
    }
}