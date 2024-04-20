using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.OwnerResponses;

namespace Test.ApiControllers;

[TestClass]
public class BuildingControllerTest
{
    private Mock<IBuildingAdapter> _buildingAdapter;
    private BuildingController _buildingController;

    #region Initialization

    [TestInitialize]
    public void Initialize()
    {
        _buildingAdapter = new Mock<IBuildingAdapter>(MockBehavior.Strict);
        _buildingController = new BuildingController(_buildingAdapter.Object);
    }

    #endregion

    #region Get Buildings

    [TestMethod]
    public void GetBuildings_OkIsReturned()
    {
        IEnumerable<GetBuildingResponse> expectedBuildings = new List<GetBuildingResponse>
        {
            new GetBuildingResponse()
            {
                Id = Guid.NewGuid(),
                Name = "Building 1",
                Address = "North Avenue",
                Location = new LocationResponse()
                {
                    Latitude = 1.2345,
                    Longitude = 1.2345,
                },
                ConstructionCompany = "Company 1",
                CommonExpenses = 300,
                Flats = new GetFlatResponse()
                {
                    Floor = 1,
                    RoomNumber = 102,
                    Owner = new OwnerResponse
                    {
                        Id = Guid.NewGuid(),
                        Name = "Owner Name",
                        Lastname = "Owner Lastname",
                        Email = "owner@gmail.com"
                    },
                    TotalRooms = 4,
                    TotalBaths = 2,
                    HasTerrace = true
                }
            }
        };
        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedBuildings);

        _buildingAdapter.Setup(adapter => adapter.GetBuildings(It.IsAny<Guid>())).Returns(expectedBuildings);

        IActionResult controllerResponse = _buildingController.GetBuildings(It.IsAny<Guid>());
        _buildingAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        IEnumerable<GetBuildingResponse> controllerResponseValue =
            controllerResponseCasted.Value as IEnumerable<GetBuildingResponse>;

        Assert.IsNotNull(controllerResponseValue);
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedBuildings.Equals(controllerResponseValue));
    }

    [TestMethod]
    public void GetBuildings_NotFoundIsReturned()

    {
        NotFoundObjectResult expectedControllerResponse = new NotFoundObjectResult("User id was not found in database");

        _buildingAdapter.Setup(adapter => adapter.GetBuildings(It.IsAny<Guid>())).Throws(new ObjectNotFoundException());

        IActionResult controllerResponse = _buildingController.GetBuildings(It.IsAny<Guid>());
        _buildingAdapter.VerifyAll();

        NotFoundObjectResult? controllerResponseCasted = controllerResponse as NotFoundObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    [TestMethod]
    public void GetBuildings_500StatusCodeIsReturned()
    {
        ObjectResult expectedControllerResponse = new ObjectResult("Internal Server Error");
        expectedControllerResponse.StatusCode = 500;

        _buildingAdapter.Setup(adapter => adapter.GetBuildings(It.IsAny<Guid>()))
            .Throws(new Exception("Unknown error"));

        IActionResult controllerResponse = _buildingController.GetBuildings(It.IsAny<Guid>());
        _buildingAdapter.VerifyAll();

        ObjectResult? controllerResponseCasted = controllerResponse as ObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value, controllerResponseCasted.Value);
    }

    #endregion

    [TestMethod]
    public void GetBuildingById_OkIsReturned()
    {
        GetBuildingResponse expectedBuildingValue = new GetBuildingResponse()
        {
            Name = "Building 1",
            Address = "North Avenue",
            Location = new LocationResponse
            {
                Latitude = 1.023,
                Longitude = 1.12
            },
            ConstructionCompany = "Construction Company 1",
            CommonExpenses = 1000,
            Flats = new GetFlatResponse()
            {
                Floor = 1,
                RoomNumber = 102,
                Owner = new OwnerResponse()
                {
                    Name = "Owner name",
                    Lastname = "Owner lastname",
                    Email = "owner@gmail.com"
                },
                TotalRooms = 2,
                TotalBaths = 1,
                HasTerrace = true
            }
        };
        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedBuildingValue);

        _buildingAdapter.Setup(adapter => adapter.GetBuildingById(It.IsAny<Guid>())).Returns(expectedBuildingValue);

        IActionResult controllerResponse = _buildingController.GetBuildingById(It.IsAny<Guid>());
        _buildingAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);
        
        GetBuildingResponse? controllerValue = controllerResponseCasted.Value as GetBuildingResponse;
        Assert.IsNotNull(controllerValue);
        
        Assert.AreEqual(expectedControllerResponse.StatusCode,controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedBuildingValue.Equals(controllerValue));
    }
}




