using BuildingBuddy.API.Filters;
using Domain.Enums;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.ManagerRequests;
using WebModel.Responses.ManagerResponses;

namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
    [Route("api/v2/managers")]
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
        [AuthenticationFilter(SystemUserRoleEnum.Admin,SystemUserRoleEnum.ConstructionCompanyAdmin)]
        public IActionResult GetAllManagers()
        {
            return Ok(_managerAdapter.GetAllManagers());
        }

        #endregion

        #region Delete Manager
        
        [HttpDelete]
        [Route("{managerId:Guid}")]
        [AuthenticationFilter(SystemUserRoleEnum.Admin)]
        public IActionResult DeleteManagerById([FromRoute] Guid managerId)
        {
            _managerAdapter.DeleteManagerById(managerId);
            return NoContent();
        }

        #endregion

        #region Create Manager

        [HttpPost]
        public IActionResult CreateManager([FromBody] CreateManagerRequest createRequest,
            [FromQuery] Guid idOfInvitationAccepted)
        {
            CreateManagerResponse adapterResponse =
                _managerAdapter.CreateManager(createRequest, idOfInvitationAccepted);

            return CreatedAtAction(nameof(CreateManager), new { id = adapterResponse.Id }, adapterResponse);
        }

        #endregion

        #region Get Manager By Id

        [HttpGet]
        [Route("{managerId:Guid}")]
        [AuthenticationFilter(SystemUserRoleEnum.Admin,SystemUserRoleEnum.ConstructionCompanyAdmin, SystemUserRoleEnum.Manager)]
        public IActionResult GetManagerById([FromRoute] Guid managerId)
        {
            return Ok(_managerAdapter.GetManagerById(managerId));
        }

        #endregion
    }
}