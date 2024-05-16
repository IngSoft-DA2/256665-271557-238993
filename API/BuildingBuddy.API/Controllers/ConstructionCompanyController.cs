using Adapter.CustomExceptions;
using BuildingBuddy.API.Filters;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.ConstructionCompanyRequests;
using WebModel.Responses.ConstructionCompanyResponses;


namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/construction-companies")]
    [ApiController]
    [CustomExceptionFilter]
    public class ConstructionCompanyController : ControllerBase
    {
        #region Constructor and atributes

        private readonly IConstructionCompanyAdapter _constructionCompanyAdapter;

        public ConstructionCompanyController(IConstructionCompanyAdapter constructionCompanyAdapter)
        {
            _constructionCompanyAdapter = constructionCompanyAdapter;
        }

        #endregion

        #region GetConstructionCompanies

        [HttpGet]
        public IActionResult GetAllConstructionCompanies()
        {
            
            return Ok(_constructionCompanyAdapter.GetAllConstructionCompanies());
            
        }

        #endregion
        
        #region CreateConstructionCompany
        
        [HttpPost]
        public IActionResult CreateConstructionCompany([FromBody] CreateConstructionCompanyRequest createConstructionCompanyRequest)
        {
            CreateConstructionCompanyResponse response = _constructionCompanyAdapter.CreateConstructionCompany(createConstructionCompanyRequest);
            return CreatedAtAction(nameof(CreateConstructionCompany), new { id = response.Id }, response);
           
        }
        
        #endregion
    }
}
