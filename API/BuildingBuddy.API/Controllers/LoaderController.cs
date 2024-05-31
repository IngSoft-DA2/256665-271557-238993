using BuildingBuddy.API.Filters;
using Domain.Enums;
using IAdapter;
using ILoaders;
using IServiceLogic;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.LoaderRequests;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.LoaderReponses;

namespace BuildingBuddy.API.Controllers;

[ExceptionFilter]
[Route("api/v1/loaders")]
[ApiController]
public class LoaderController : ControllerBase
{
    #region Constructor and attributes
    
    private readonly IBuildingAdapter _buildingAdapter;
    private readonly ILoaderAdapter _loaderAdapter;
    private readonly ISessionService _sessionService;
    
    public LoaderController(ILoaderAdapter loaderAdapter, ISessionService sessionService, IBuildingAdapter buildingAdapter)
    {
        _loaderAdapter = loaderAdapter;
        _sessionService = sessionService;
        _buildingAdapter = buildingAdapter;
    }
    
    #endregion
    
    #region Create All Buildings From Load
    
    [HttpPost]
    [AuthenticationFilter(SystemUserRoleEnum.ConstructionCompanyAdmin)]
    public IActionResult CreateAllBuildingsFromLoad([FromBody] CreateLoaderRequest createLoaderRequest)
    {
        List<CreateBuildingFromLoadResponse> response = _loaderAdapter.CreateAllBuildingsFromLoad(createLoaderRequest);
        return Ok(response);
    }
    
    #endregion

    #region Get All Loaders
    
    [HttpGet]
    public IActionResult GetAllLoaders()
    {
        List<string> response = _loaderAdapter.GetAllLoaders();
        return Ok(response);
    }
    
    #endregion
}