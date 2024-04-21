using Adapter.CustomExceptions;
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
    private Mock<IRequestHandlerAdapter> _requestHandlerAdapter;
    private RequestHandlerController requestHandlerController;


    [TestInitialize]
    public void Initialize()
    {
        _requestHandlerAdapter = new Mock<IRequestHandlerAdapter>();
        requestHandlerController = new RequestHandlerController(_requestHandlerAdapter.Object);
    }

    [TestMethod]
    public void CreateRequestHandlerRequest_CreatedAtActionIsReturned()
    {
        CreateRequestHandlerResponse expectedValue = new CreateRequestHandlerResponse();

        CreatedAtActionResult expectedControllerResponse =
            new CreatedAtActionResult("CreateRequestHandler", "CreateRequestHandler"
                , expectedValue.Id, expectedValue);

        _requestHandlerAdapter.Setup(x => x.CreateRequestHandler(It.IsAny<CreateRequestHandlerRequest>()))
            .Returns(expectedValue);

        IActionResult controllerResponse =
            requestHandlerController.CreateRequestHandler(It.IsAny<CreateRequestHandlerRequest>());
        _requestHandlerAdapter.VerifyAll();

        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        Assert.IsNotNull(controllerResponseCasted);

        CreateRequestHandlerResponse? value =
            controllerResponseCasted.Value as CreateRequestHandlerResponse;
        Assert.IsNotNull(value);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedValue.Id, value.Id);
    }

    [TestMethod]
    public void CreateRequestHandlerRequest_BadRequestIsReturned()
    {
        BadRequestObjectResult expectedControllerResponse = new BadRequestObjectResult("Specific Error");
        
        _requestHandlerAdapter.Setup(adapter => adapter.CreateRequestHandler(It.IsAny<CreateRequestHandlerRequest>()))
            .Throws(new ObjectErrorException("Specific Error"));

        IActionResult controllerResponse =
            requestHandlerController.CreateRequestHandler(It.IsAny<CreateRequestHandlerRequest>());
        _requestHandlerAdapter.VerifyAll();

        BadRequestObjectResult? controllerResponseCasted = controllerResponse as BadRequestObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }
}