using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.RequestHandlerRequests;
using WebModel.Responses.RequestHandlerResponses;


namespace Test.ApiControllers;

[TestClass]
public class RequestHandlerControllerTest
{
    [TestMethod]
    public void CreateRequestHandlerRequest_CreatedAtActionIsReturned()
    {
        CreateRequestHandlerResponse expectedValue = new CreateRequestHandlerResponse();

        CreatedAtActionResult expectedControllerResponse =
            new CreatedAtActionResult("CreateRequestHandler", "CreateRequestHandler"
                , expectedValue.Id, expectedValue);

        Mock<IRequestHandlerAdapter> requestHandlerAdapter = new Mock<IRequestHandlerAdapter>();
        requestHandlerAdapter.Setup(x => x.CreateRequestHandler(It.IsAny<CreateRequestHandlerRequest>()))
            .Returns(expectedValue);

        RequestHandlerController requestHandlerController = new RequestHandlerController(requestHandlerAdapter.Object);

        IActionResult controllerResponse =
            requestHandlerController.CreateRequestHandler(It.IsAny<CreateRequestHandlerRequest>());

        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        Assert.IsNotNull(controllerResponseCasted);

        CreateRequestHandlerResponse? value =
            controllerResponseCasted.Value as CreateRequestHandlerResponse;
        Assert.IsNotNull(value);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedValue.Id, value.Id);
    }
}