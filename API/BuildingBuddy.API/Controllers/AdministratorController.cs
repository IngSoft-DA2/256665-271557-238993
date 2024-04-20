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
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorAdapter _administratorAdapter;

        public AdministratorController(IAdministratorAdapter administratorAdapter)
        {
            _administratorAdapter = administratorAdapter;
        }


        public IActionResult CreateAdministrator(CreateAdministratorRequest request)
        {
            try
            {
                CreateAdministratorResponse response = _administratorAdapter.CreateAdministrator(request);
                return CreatedAtAction(nameof(CreateAdministrator), new { id = response.Id }, response);
            }
            catch (ObjectErrorException exceptionCaught)
            {
                return BadRequest(exceptionCaught.Message);
            }
       
            
        }
    }
}