using BuildingBuddy.API.Controllers;
using IAdapter;
using ILoaders;
using IServiceLogic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.BuildingRequests;
using WebModel.Requests.LoaderRequests;
using WebModel.Responses.BuildingResponses;

namespace Test.ApiControllers;

[TestClass]
public class LoaderControllerTest
{
    [TestMethod]
    public void CreateAllBuildingsFromLoad_ReturnsOkResponse()
    {
        Mock<ILoaderAdapter> loaderAdapter = new Mock<ILoaderAdapter>(MockBehavior.Strict);
        Mock<ISessionService> sessionService = new Mock<ISessionService>(MockBehavior.Strict);
        Mock<IBuildingAdapter> buildingAdapter = new Mock<IBuildingAdapter>(MockBehavior.Strict);

        CreateLoaderRequest createLoaderRequest = new CreateLoaderRequest();
        List<CreateBuildingResponse> createBuildingRequestList = new List<CreateBuildingResponse>();
        
        loaderAdapter.Setup(adapter => adapter.CreateAllBuildingsFromLoad(createLoaderRequest)).Returns(createBuildingRequestList);
        LoaderController loaderController = new LoaderController(loaderAdapter.Object, sessionService.Object, buildingAdapter.Object);
        
        IActionResult controllerResponse = loaderController.CreateAllBuildingsFromLoad(createLoaderRequest);
        
        Assert.IsNotNull(controllerResponse);
        Assert.IsInstanceOfType(controllerResponse, typeof(OkObjectResult));
        
        loaderAdapter.VerifyAll();
    }

}