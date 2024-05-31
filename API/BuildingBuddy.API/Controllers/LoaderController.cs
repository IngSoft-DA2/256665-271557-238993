using BuildingBuddy.API.Filters;
using Domain.Enums;
using IAdapter;
using ILoaders;
using IServiceLogic;
using Microsoft.AspNetCore.Mvc;
using WebModel.Requests.LoaderRequests;
using WebModel.Responses.BuildingResponses;

namespace BuildingBuddy.API.Controllers;

[ExceptionFilter]
[Route("api/v1/loaders")]
[ApiController]
public class LoaderController : ControllerBase
{
    private readonly IBuildingAdapter _buildingAdapter;
    private readonly ILoaderAdapter _loaderAdapter;
    private readonly ISessionService _sessionService;
    
    public LoaderController(ILoaderAdapter loaderAdapter, ISessionService sessionService, IBuildingAdapter buildingAdapter)
    {
        _loaderAdapter = loaderAdapter;
        _sessionService = sessionService;
        _buildingAdapter = buildingAdapter;
    }
    
    [HttpPost]
    [AuthenticationFilter(SystemUserRoleEnum.ConstructionCompanyAdmin)]
    public IActionResult CreateAllBuildingsFromLoad([FromBody] CreateLoaderRequest createLoaderRequest)
    {
        List<CreateBuildingResponse> response = _loaderAdapter.CreateAllBuildingsFromLoad(createLoaderRequest);
        return Ok(response);
    }

    [HttpGet]
    public IActionResult GetAllLoaders()
    {
        List<string> response = _loaderAdapter.GetAllLoaders();
        return Ok(response);
    }





}