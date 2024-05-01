using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.OwnerRequests;
using WebModel.Responses.OwnerResponses;

namespace Test.ApiControllers;

[TestClass]
public class OwnerControllerTest
{
    #region Initialize

    private Mock<IOwnerAdapter> _ownerAdapter;
    private OwnerController _ownerController;

    [TestInitialize]
    public void Initialize()
    {
        _ownerAdapter = new Mock<IOwnerAdapter>(MockBehavior.Strict);
        _ownerController = new OwnerController(_ownerAdapter.Object);
    }

    #endregion

    #region GetOwners

    [TestMethod]
    public void GetOwners_OkIsReturned()
    {
        IEnumerable<GetOwnerResponse> expectedOwners = new List<GetOwnerResponse>()
        {
            new GetOwnerResponse()
            {
                Id = Guid.NewGuid(),
                Firstname = "myOwner",
                Lastname = "myLastName",
                Email = "email@email.com",
            }
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedOwners);

        _ownerAdapter.Setup(adapter => adapter.GetAllOwners()).Returns(expectedOwners);

        IActionResult controllerResponse = _ownerController.GetAllOwners();

        _ownerAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        List<GetOwnerResponse>? controllerResponseValueCasted =
            controllerResponseCasted.Value as List<GetOwnerResponse>;
        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(controllerResponseValueCasted.SequenceEqual(expectedOwners));
    }

    #endregion

    #region Get Owner By Id

    [TestMethod]
    public void GetOwnerResponseById_OkIsReturned()
    {
        GetOwnerResponse expectedOwner = new GetOwnerResponse()
        {
            Id = Guid.NewGuid(),
            Firstname = "ownerFirstname",
            Lastname = "ownerLastname",
            Email = "owner@gmail.com"
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedOwner);

        _ownerAdapter.Setup(adapter => adapter.GetOwnerById(It.IsAny<Guid>())).Returns(expectedOwner);

        IActionResult controllerResponse = _ownerController.GetOwnerById(It.IsAny<Guid>());
        _ownerAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        GetOwnerResponse? controllerResponseValueCasted = controllerResponseCasted.Value as GetOwnerResponse;
        Assert.IsNotNull(controllerResponseValueCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedOwner.Equals(controllerResponseValueCasted));
    }

    
    #endregion

    #region CreateOwner

    [TestMethod]
    public void CreateOwner_CreatedAtActionIsReturned()
    {
        CreateOwnerResponse expectedOwner = new CreateOwnerResponse()
        {
            Id = Guid.NewGuid()
        };

        CreatedAtActionResult expectedControllerResponse =
            new CreatedAtActionResult("CreateOwner", "CreateOwner"
                , expectedOwner.Id, expectedOwner);

        _ownerAdapter.Setup(adapter => adapter.CreateOwner(It.IsAny<CreateOwnerRequest>())).Returns(expectedOwner);

        IActionResult controllerResponse = _ownerController.CreateOwner(It.IsAny<CreateOwnerRequest>());

        _ownerAdapter.VerifyAll();

        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;

        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }

   
    #endregion

    #region UpdateOwner

    [TestMethod]
    public void UpdateOwner_NoContentIsReturned()
    {
        NoContentResult expectedControllerResponse = new NoContentResult();
        _ownerAdapter.Setup(adapter => adapter.UpdateOwnerById(It.IsAny<Guid>(), It.IsAny<UpdateOwnerRequest>()));

        IActionResult controllerResponse =
            _ownerController.UpdateOwnerById(It.IsAny<Guid>(), It.IsAny<UpdateOwnerRequest>());

        _ownerAdapter.Verify(
            adapter => adapter.UpdateOwnerById(It.IsAny<Guid>(), It.IsAny<UpdateOwnerRequest>()), Times.Once());

        NoContentResult? controllerResponseCasted = controllerResponse as NoContentResult;

        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }

    #endregion
}