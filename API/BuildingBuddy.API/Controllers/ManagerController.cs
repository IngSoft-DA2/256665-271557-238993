using BuildingBuddy.API.Filters;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.ManagerRequests;
using WebModel.Responses.ManagerResponses;

namespace BuildingBuddy.API.Controllers
{
    [CustomExceptionFilter]
    [Route("api/v1/managers")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        #region Constructor and attributes
        
        private readonly IManagerAdapter _managerAdapter;

        public ManagerController(IManagerAdapter managerAdapter)
        {
            _managerAdapter = managerAdapter;
        }
        
        #endregion
        
        #region Get All Managers

        [HttpGet]
        public IActionResult GetAllManagers()
        {
           
            return Ok(_managerAdapter.GetAllManagers());
            
        }
        
        #endregion

        #region Delete Manager
        
        [HttpDelete]
        [Route("{managerId:Guid}")]
        public IActionResult DeleteManagerById([FromRoute] Guid managerId)
        {
           
                _managerAdapter.DeleteManagerById(managerId);
                return NoContent();
            
        }
        
        #endregion
        
        #region Create Manager

        [HttpPost]
        public IActionResult CreateManager(CreateManagerRequest createRequest, [FromQuery] Guid idOfInvitationAccepted)
        {
          
                CreateManagerResponse adapterReponse = _managerAdapter.CreateManager(createRequest, idOfInvitationAccepted);

                return CreatedAtAction(nameof(CreateManager), new { id = adapterReponse.Id }, adapterReponse);
        }
        
        #endregion
    }
}