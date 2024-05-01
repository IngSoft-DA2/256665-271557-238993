using Adapter.CustomExceptions;
using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.AdministratorRequests;
using WebModel.Requests.BuildingRequests;
using WebModel.Responses.AdministratorResponses;
using WebModel.Responses.BuildingResponses;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/administrators")]
    [ApiController]
    [CustomExceptionFilter]
    public class AdministratorController : ControllerBase
    {
        #region Constructor and attributes
        
        private readonly IAdministratorAdapter _administratorAdapter;

        public AdministratorController(IAdministratorAdapter administratorAdapter)
        {
            _administratorAdapter = administratorAdapter;
        }
        
        #endregion
        
        #region Create Administrator
        
        [AuthorizationFilter(RoleNeeded = "Admin")]
        [HttpPost]
        public IActionResult CreateAdministrator([FromBody] CreateAdministratorRequest request)
        {
            CreateAdministratorResponse response = _administratorAdapter.CreateAdministrator(request);
            return CreatedAtAction(nameof(CreateAdministrator), new { id = response.Id }, response);
            
        }
        
        #endregion
    }
}