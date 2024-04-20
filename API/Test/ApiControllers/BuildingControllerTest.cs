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
    [TestMethod]
    public void GetBuildings_OkIsReturned()
    {
        IEnumerable<GetBuildingResponse> expectedBuildings = new List<GetBuildingResponse>
        {
            new GetBuildingResponse()
            {
                Id = Guid.NewGuid(),
                Name = "Building 1",
                Direction = "North Avenue",
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

        Mock<IBuildingAdapter> buildingAdapter = new Mock<IBuildingAdapter>(MockBehavior.Strict);
        buildingAdapter.Setup(adapter => adapter.GetBuildings(It.IsAny<Guid>())).Returns(expectedBuildings);

        BuildingController buildingController = new BuildingController(buildingAdapter.Object);

        IActionResult controllerResponse = buildingController.GetBuildings(It.IsAny<Guid>());
        buildingAdapter.VerifyAll();

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

        Mock<IBuildingAdapter> buildingAdapter = new Mock<IBuildingAdapter>(MockBehavior.Strict);
        buildingAdapter.Setup(adapter => adapter.GetBuildings(It.IsAny<Guid>())).Throws(new ObjectNotFoundException());

        BuildingController buildingController = new BuildingController(buildingAdapter.Object);

        IActionResult controllerResponse = buildingController.GetBuildings(It.IsAny<Guid>());
        
        NotFoundObjectResult? controllerResponseCasted = controllerResponse as NotFoundObjectResult;
        Assert.IsNotNull(controllerResponseCasted);
        
        Assert.AreEqual(expectedControllerResponse.StatusCode,controllerResponseCasted.StatusCode);
        Assert.AreEqual(expectedControllerResponse.Value,controllerResponseCasted.Value);
        
    }
}