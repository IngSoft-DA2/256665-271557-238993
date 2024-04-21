using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.RequestHandlerRequests;
using WebModel.Responses.RequestHandlerResponses;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/request-handlers")]
    [ApiController]
    public class RequestHandlerController : ControllerBase
    {
        private readonly IRequestHandlerAdapter _requestHandlerAdapter;

        public RequestHandlerController(IRequestHandlerAdapter requestHandlerAdapter)
        {
            _requestHandlerAdapter = requestHandlerAdapter;
        }

        [HttpPost]
        public IActionResult CreateRequestHandler([FromBody] CreateRequestHandlerRequest requestHandlerRequest)
        {
            try
            {
                CreateRequestHandlerResponse response =
                    _requestHandlerAdapter.CreateRequestHandler(requestHandlerRequest);
                return CreatedAtAction(nameof(CreateRequestHandler),
                    new { id = response.Id }, response);
            }
            catch (ObjectErrorAdapterException exceptionCaught)
            {
                return BadRequest(exceptionCaught.Message);
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}