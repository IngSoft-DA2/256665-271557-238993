using Adapter.CustomExceptions;
using BuildingBuddy.API.Filters;
using Domain.Enums;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.ConstructionCompanyRequests;
using WebModel.Responses.ConstructionCompanyResponses;


namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
    [Route("api/v2/construction-companies")]
    [ApiController]
    public class ConstructionCompanyController : ControllerBase
    {
        #region Constructor and atributes

        private readonly IConstructionCompanyAdapter _constructionCompanyAdapter;

        public ConstructionCompanyController(IConstructionCompanyAdapter constructionCompanyAdapter)
        {
            _constructionCompanyAdapter = constructionCompanyAdapter;
        }

        #endregion

        #region Get all Construction Companies

        [HttpGet]
        [AuthenticationFilter(SystemUserRoleEnum.ConstructionCompanyAdmin)]
        public IActionResult GetAllConstructionCompanies()
        {
            return Ok(_constructionCompanyAdapter.GetAllConstructionCompanies());
        }

        #endregion

        #region Get Construction Company By User Creator Id

        [HttpGet]
        [Route("{constructionCompanyId:Guid}")]
        public IActionResult GetConstructionCompanyById([FromRoute] Guid constructionCompanyId)
        {
            return Ok(_constructionCompanyAdapter.GetConstructionCompanyById(constructionCompanyId));
        }

        #endregion

        #region Create Construction Company

        [HttpPost]
        [AuthenticationFilter(SystemUserRoleEnum.ConstructionCompanyAdmin)]
        public IActionResult CreateConstructionCompany(
            [FromBody] CreateConstructionCompanyRequest createConstructionCompanyRequest)
        {
            CreateConstructionCompanyResponse response =
                _constructionCompanyAdapter.CreateConstructionCompany(createConstructionCompanyRequest);
            return CreatedAtAction(nameof(CreateConstructionCompany), new { id = response.Id }, response);
        }

        #endregion
        
        #region Update Construction Company

        [HttpPut]
        [AuthenticationFilter(SystemUserRoleEnum.ConstructionCompanyAdmin)]
        [Route("{id:Guid}")]
        public IActionResult UpdateConstructionCompany([FromRoute] Guid id,[FromBody] UpdateConstructionCompanyRequest updateConstructionCompanyRequest)
        {
            _constructionCompanyAdapter.UpdateConstructionCompany(id,updateConstructionCompanyRequest);
            return NoContent();
        }

        #endregion
    }
}