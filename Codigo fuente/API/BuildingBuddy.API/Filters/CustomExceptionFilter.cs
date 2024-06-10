using Adapter.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuildingBuddy.API.Filters;

public class CustomExceptionFilter : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        try
        {
            throw context.Exception;
        }
        catch (ObjectErrorAdapterException exceptionCaught)
        {
            context.Result = new BadRequestObjectResult(new { Message = exceptionCaught.Message }) {StatusCode = 400};
        }
        catch (ObjectNotFoundAdapterException)
        {
            context.Result = new NotFoundResult();
        }
        catch (ObjectRepeatedAdapterException)
        {
            context.Result = new ObjectResult(new { Message = "Object already exists" }) { StatusCode = 304 };
        }
        catch (Exception e)
        {
            context.Result = new ObjectResult(new { Message = "Internal server error" }) { StatusCode = 500 };
            Console.WriteLine(e.Message);
        }


    }
}