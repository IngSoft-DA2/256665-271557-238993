using BuildingBuddy.API.Filters;
using Domain.Enums;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.RequestHandlerRequests;
using WebModel.Responses.RequestHandlerResponses;

namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
    [Route("api/v2/request-handlers")]
    [ApiController]
    public class RequestHandlerController : ControllerBase
    {
        #region Constructor and Dependency Injector

        private readonly IRequestHandlerAdapter _requestHandlerAdapter;

        public RequestHandlerController(IRequestHandlerAdapter requestHandlerAdapter)
        {
            _requestHandlerAdapter = requestHandlerAdapter;
        }

        #endregion

        #region Create Request Handler

        [HttpPost]
        [AuthenticationFilter(SystemUserRoleEnum.Manager)]
        public IActionResult CreateRequestHandler([FromBody] CreateRequestHandlerRequest requestHandlerRequest)
        {
            CreateRequestHandlerResponse response =
                _requestHandlerAdapter.CreateRequestHandler(requestHandlerRequest);
            return CreatedAtAction(nameof(CreateRequestHandler),
                new { id = response.Id }, response);
        }

        #endregion

        #region Get all Request Handlers

        [HttpGet]
        [AuthenticationFilter(SystemUserRoleEnum.RequestHandler, SystemUserRoleEnum.Manager)]
        public IActionResult GetAllRequestHandlers()
        {
            return Ok(_requestHandlerAdapter.GetAllRequestHandlers());
        }

        #endregion
    }
}