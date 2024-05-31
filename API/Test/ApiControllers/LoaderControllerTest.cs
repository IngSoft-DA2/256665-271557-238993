using BuildingBuddy.API.Controllers;
using IAdapter;
using ILoaders;
using IServiceLogic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.BuildingRequests;
using WebModel.Requests.LoaderRequests;

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

        LoaderRequest loaderRequest = new LoaderRequest();
        IEnumerable<ILoader> createBuildingRequestList = new List<ILoader>();
        
        loaderAdapter.Setup(adapter => adapter.GetLoaderInterfaces(loaderRequest)).Returns(createBuildingRequestList.ToList());
        LoaderController loaderController = new LoaderController(loaderAdapter.Object, sessionService.Object, buildingAdapter.Object);
        
        IActionResult controllerResponse = loaderController.CreateAllBuildingsFromLoad(loaderRequest);
        
        Assert.IsNotNull(controllerResponse);
        Assert.IsInstanceOfType(controllerResponse, typeof(OkObjectResult));
        
        loaderAdapter.VerifyAll();
    }

}