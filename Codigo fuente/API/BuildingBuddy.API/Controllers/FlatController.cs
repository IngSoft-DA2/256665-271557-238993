using Adapter.CustomExceptions;
using BuildingBuddy.API.Filters;
using Domain.Enums;
using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.FlatRequests;

namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
    [AuthenticationFilter(SystemUserRoleEnum.ConstructionCompanyAdmin)]
    [Route("api/v2/flats")]
    [ApiController]
    public class FlatController : ControllerBase
    {
        #region Constructor and atributes

        private readonly IFlatAdapter _flatAdapter;

        public FlatController(IFlatAdapter flatAdapter)
        {
            _flatAdapter = flatAdapter;
        }

        #endregion
       
        #region CreateFlat

        [HttpPost]
        public IActionResult CreateFlat([FromBody] CreateFlatRequest flatToCreate)
        {
            
            _flatAdapter.CreateFlat(flatToCreate);  
            return Accepted(new {message = "Flat accepted, and it will be processed on the building creation stage"});
            
        }

        #endregion
    }
}