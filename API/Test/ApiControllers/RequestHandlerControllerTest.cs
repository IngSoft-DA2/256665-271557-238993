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
    #region Initialize

    private Mock<IRequestHandlerAdapter> _requestHandlerAdapter;
    private RequestHandlerController _requestHandlerController;

    [TestInitialize]
    public void Initialize()
    {
        _requestHandlerAdapter = new Mock<IRequestHandlerAdapter>();
        _requestHandlerController = new RequestHandlerController(_requestHandlerAdapter.Object);
    }

    #endregion

    #region Create Request Handler Request

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
            _requestHandlerController.CreateRequestHandler(It.IsAny<CreateRequestHandlerRequest>());
        _requestHandlerAdapter.VerifyAll();

        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        Assert.IsNotNull(controllerResponseCasted);

        CreateRequestHandlerResponse? value =
            controllerResponseCasted.Value as CreateRequestHandlerResponse;
        Assert.IsNotNull(value);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedValue.Id, value.Id);
    }
    
    #endregion
    
    #region Get All Request Handlers
    
    [TestMethod]
    public void GetAllRequestHandlers_OkIsReturned()
    {
        List<GetRequestHandlerResponse> expectedValue = new List<GetRequestHandlerResponse>();

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedValue);

        _requestHandlerAdapter.Setup(x => x.GetAllRequestHandlers()).Returns(expectedValue);

        IActionResult controllerResponse = _requestHandlerController.GetAllRequestHandlers();
        _requestHandlerAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetRequestHandlerResponse>? value = controllerResponseCasted.Value as List<GetRequestHandlerResponse>;
        Assert.IsNotNull(value);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedValue, value);
    }
    
    #endregion
}