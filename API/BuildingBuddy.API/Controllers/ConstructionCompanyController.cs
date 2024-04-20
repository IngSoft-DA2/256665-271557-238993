using IAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v1/constructionCompany")]
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

        #region GetConstructionCompanies

        [HttpGet]
        public IActionResult GetConstructionCompanies()
        {
            try
            {
                return Ok(_constructionCompanyAdapter.GetConstructionCompanies());
            }
            catch (Exception exceptionCaught)
            {
                Console.WriteLine(exceptionCaught.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        #endregion
    }
}
