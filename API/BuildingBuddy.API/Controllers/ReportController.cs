using BuildingBuddy.API.Filters;
using Domain.Enums;
using IAdapter;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
    [Route("api/v2")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        #region Constructor and atributes

        private readonly IReportAdapter _reportAdapter;

        public ReportController(IReportAdapter reportAdapter)
        {
            _reportAdapter = reportAdapter;
        }

        #endregion

        #region GetMaintenanceRequestsByBuilding

        [AuthenticationFilter(SystemUserRoleEnum.Manager)]
        [Route("buildings/maintenance-requests/reports")]
        [HttpGet]
        public IActionResult GetMaintenanceRequestsByBuilding([FromQuery] Guid managerId, [FromQuery] Guid buildingId)
        {
            return Ok(_reportAdapter.GetMaintenanceReportByBuilding(managerId, buildingId));
        }

        #endregion

        #region GetMaintenanceRequestsByRequestHandler

        [AuthenticationFilter(SystemUserRoleEnum.Manager)]
        [Route("request-handler/maintenance-requests/reports")]
        [HttpGet]
        public IActionResult GetMaintenanceRequestsByRequestHandler([FromQuery] Guid requestHandlerId,
            [FromQuery] Guid buildingId, [FromQuery] Guid managerId)
        {
            return Ok(_reportAdapter.GetMaintenanceReportByRequestHandler(requestHandlerId, buildingId, managerId));
        }

        #endregion

        #region GetMaintenanceRequestsByCategory

        [HttpGet]
        [AuthenticationFilter(SystemUserRoleEnum.Admin)]
        [Route("categories/maintenance-requests/reports")]
        public IActionResult GetMaintenanceRequestsByCategory([FromQuery] Guid buildingId, [FromQuery] Guid categoryId)
        {
            return Ok(_reportAdapter.GetMaintenanceReportByCategory(buildingId, categoryId));
        }

        #endregion

        #region GetFlatRequestsByBuildingReport

        [HttpGet]
        [AuthenticationFilter(SystemUserRoleEnum.Manager)]
        [Route("flats/maintenance-requests/reports")]
        public IActionResult GetFlatRequestsByBuildingReport([FromQuery] Guid buildingId)
        {
            try
            {
                return Ok(_reportAdapter.GetFlatRequestsByBuildingReport(buildingId));
            }
            catch (Exception exceptionCaught)
            {
                return BadRequest(exceptionCaught.Message);
            }
        }

        #endregion
    }
}