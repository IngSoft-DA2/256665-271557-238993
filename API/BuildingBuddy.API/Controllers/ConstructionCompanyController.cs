using BuildingBuddy.API.Filters;
using Domain.Enums;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.ConstructionCompanyRequests;
using WebModel.Responses.ConstructionCompanyResponses;


namespace BuildingBuddy.API.Controllers
{
    [ExceptionFilter]
    [AuthenticationFilter(SystemUserRoleEnum.ConstructionCompanyAdmin)]
    [Route("api/v2")]
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
        [Route("construction-companies")]

        public IActionResult GetAllConstructionCompanies()
        {
            return Ok(_constructionCompanyAdapter.GetAllConstructionCompanies());
        }

        #endregion

        #region Get Construction Company By Id

        [HttpGet]
        
        [Route("construction-companies/{constructionCompanyId:Guid}")]
        public IActionResult GetConstructionCompanyById([FromRoute] Guid constructionCompanyId)
        {
            return Ok(_constructionCompanyAdapter.GetConstructionCompanyById(constructionCompanyId));
        }

        #endregion
        
        #region Get Construction Company By User Creator Id

        [HttpGet]
        [Route("user-id/{userId:Guid}/construction-companies")]
        public IActionResult GetConstructionCompanyByUserCreatorId([FromRoute] Guid userId)
        {
            return Ok(_constructionCompanyAdapter.GetConstructionCompanyByUserCreatorId(userId));
        }

        #endregion

        #region Create Construction Company

        [HttpPost]
        [Route("construction-companies")]
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
        [Route("construction-companies/{id:Guid}")]
        public IActionResult UpdateConstructionCompany([FromRoute] Guid id,[FromBody] UpdateConstructionCompanyRequest updateConstructionCompanyRequest)
        {
            _constructionCompanyAdapter.UpdateConstructionCompany(id,updateConstructionCompanyRequest);
            return NoContent();
        }

        #endregion
    }
}