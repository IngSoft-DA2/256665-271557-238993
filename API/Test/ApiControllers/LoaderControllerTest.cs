using BuildingBuddy.API.Controllers;
using IAdapter;
using ILoaders;
using IServiceLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.BuildingRequests;
using WebModel.Requests.LoaderRequests;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.LoaderReponses;

namespace Test.ApiControllers;

[TestClass]
public class LoaderControllerTest
{
    #region Initialzing aspects

    private Mock<ILoaderAdapter> _loaderAdapter;
    private Mock<ISessionService> _sessionService;
    private Mock<IBuildingAdapter> _buildingAdapter;
    private LoaderController _loaderController;

    [TestInitialize]
    public void Initialize()
    {
        _loaderAdapter = new Mock<ILoaderAdapter>(MockBehavior.Strict);
        _sessionService = new Mock<ISessionService>(MockBehavior.Strict);
        _buildingAdapter = new Mock<IBuildingAdapter>(MockBehavior.Strict);

        _loaderController =
            new LoaderController(_loaderAdapter.Object, _sessionService.Object, _buildingAdapter.Object);
    }

    #endregion

    #region Create All Buildings From Load

    [TestMethod]
    public void CreateAllBuildingsFromLoad_ReturnsOkResponse()
    {
        ImportBuildingFromFileRequest importBuildingFromFileRequest = new ImportBuildingFromFileRequest()
        {
            FilePath = "building/file/path.xml"
        };
        List<CreateBuildingFromLoadResponse> createBuildingRequestList = new List<CreateBuildingFromLoadResponse>()
        {
            new CreateBuildingFromLoadResponse()
            {
                Details = "building name",
                idOfBuildingCreated = new Guid()
            }
        };
        
        _loaderController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext()
        };

        _loaderController.HttpContext.Request.Headers["Authorization"] = new Guid().ToString();
        
        _loaderAdapter.Setup(adapter => adapter.CreateAllBuildingsFromLoad(importBuildingFromFileRequest, It.IsAny<Guid>()))
            .Returns(createBuildingRequestList);
        
        IActionResult controllerResponse = _loaderController.CreateAllBuildingsFromLoad(importBuildingFromFileRequest);
        
        OkObjectResult okObjectResult = controllerResponse as OkObjectResult;
        Assert.IsNotNull(okObjectResult);
        
        Assert.AreEqual(createBuildingRequestList, okObjectResult.Value);

        Assert.IsNotNull(controllerResponse);
        Assert.IsInstanceOfType(controllerResponse, typeof(OkObjectResult));

        _loaderAdapter.VerifyAll();
    }

    #endregion

    #region Get All Loaders

    [TestMethod]
    public void GetAllLoaders_ReturnsOkResponse()
    {
        List<string> loaderList = new List<string>();

        _loaderAdapter.Setup(adapter => adapter.GetAllLoaders()).Returns(loaderList);

        IActionResult controllerResponse = _loaderController.GetAllLoaders();

        Assert.IsNotNull(controllerResponse);
        Assert.IsInstanceOfType(controllerResponse, typeof(OkObjectResult));

        _loaderAdapter.VerifyAll();
    }
    
    #endregion
}