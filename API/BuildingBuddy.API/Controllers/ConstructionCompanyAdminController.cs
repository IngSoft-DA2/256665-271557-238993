using IAdapter;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.ConstructionCompanyAdminRequests;
using WebModel.Responses.ConstructionCompanyAdminResponses;
using WebModel.Responses.ConstructionCompanyResponses;

namespace BuildingBuddy.API.Controllers
{
    [Route("api/v2/ConstructionCompanyAdmins")]
    [ApiController]
    public class ConstructionCompanyAdminController : ControllerBase
    {
        private IConstructionCompanyAdminAdapter _constructionCompanyAdminAdapter;

        public ConstructionCompanyAdminController(IConstructionCompanyAdminAdapter constructionCompanyAdminAdapter)
        {
            _constructionCompanyAdminAdapter = constructionCompanyAdminAdapter;
        }

        [HttpPost]
        public IActionResult CreateConstructionCompanyAdmin(
            [FromBody] CreateConstructionCompanyAdminRequest createRequest)
        {
            CreateConstructionCompanyAdminResponse response =
                _constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(createRequest);
            
            return CreatedAtAction(nameof(CreateConstructionCompanyAdmin), new { id = response.Id }, response);
        }
    }
}