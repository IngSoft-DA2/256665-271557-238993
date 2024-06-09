using System.Data.Common;
using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using IAdapter;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebModel.Requests.BuildingRequests;
using WebModel.Requests.ConstructionCompanyRequests;
using WebModel.Requests.FlatRequests;
using WebModel.Requests.OwnerRequests;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.ConstructionCompanyResponses;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.ManagerResponses;
using WebModel.Responses.OwnerResponses;

namespace Test.ApiControllers;

[TestClass]
public class BuildingControllerTest
{
    #region Initialization

    private Mock<IBuildingAdapter> _buildingAdapter;
    private BuildingController _buildingController;

    [TestInitialize]
    public void Initialize()
    {
        _buildingAdapter = new Mock<IBuildingAdapter>(MockBehavior.Strict);
        _buildingController = new BuildingController(_buildingAdapter.Object);
    }

    #endregion

    #region Get All Buildings

    [TestMethod]
    public void GetAllBuildings_OkIsReturned()
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
                ConstructionCompany = new GetConstructionCompanyResponse
                {
                    Id = Guid.NewGuid(),
                    Name = "ConstructionCompany"
                },
                CommonExpenses = 300,
                Flats = new[]
                {
                    new GetFlatResponse()
                    {
                        Floor = 1,
                        RoomNumber = "102",
                        OwnerAssigned = new GetOwnerResponse()
                        {
                            Id = Guid.NewGuid(),
                            Firstname = "Owner Name",
                            Lastname = "Owner Lastname",
                            Email = "owner@gmail.com"
                        },
                        TotalRooms = 4,
                        TotalBaths = 2,
                        HasTerrace = true
                    }
                }
            }
        };
        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedBuildings);

        _buildingAdapter.Setup(adapter => adapter.GetAllBuildings(It.IsAny<Guid>())).Returns(expectedBuildings);

        IActionResult controllerResponse = _buildingController.GetAllBuildings(It.IsAny<Guid>());
        _buildingAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        IEnumerable<GetBuildingResponse> controllerResponseValue =
            controllerResponseCasted.Value as IEnumerable<GetBuildingResponse>;

        Assert.IsNotNull(controllerResponseValue);
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedBuildings.Equals(controllerResponseValue));
    }
    
    #endregion

    #region Get Building By Id

    [TestMethod]
    public void GetBuildingById_OkIsReturned()
    {
        GetBuildingResponse expectedBuildingValue = new GetBuildingResponse()
        {
            Name = "Building 1",
            Manager = new GetManagerResponse
            {
                Id = Guid.NewGuid(),
                Name = "ManagerName",
                Email = "email@gmail.com",
                BuildingsId = new List<Guid> {Guid.NewGuid()},
                MaintenanceRequestsId = new List<Guid> {Guid.NewGuid()}
            },
            Address = "North Avenue",
            Location = new LocationResponse
            {
                Latitude = 1.023,
                Longitude = 1.12
            },
            ConstructionCompany = new GetConstructionCompanyResponse
            {
                Id = Guid.NewGuid(),
                Name = "Construction company 1",
                UserCreatorId = Guid.NewGuid(),
                BuildingsId = new List<Guid> {Guid.NewGuid()}
            },
            CommonExpenses = 1000,
            Flats = new[]
            {
                new GetFlatResponse()
                {
                    Floor = 1,
                    RoomNumber = "102",
                    OwnerAssigned = new GetOwnerResponse()
                    {
                        Id = Guid.NewGuid(),
                        Firstname = "Owner name",
                        Lastname = "Owner lastname",
                        Email = "owner@gmail.com"
                    },
                    TotalRooms = 2,
                    TotalBaths = 1,
                    HasTerrace = true
                }
            }
        };

        OkObjectResult expectedControllerResponse = new OkObjectResult(expectedBuildingValue);

        _buildingAdapter.Setup(adapter => adapter.GetBuildingById(It.IsAny<Guid>())).Returns(
            expectedBuildingValue);

        IActionResult controllerResponse = _buildingController.GetBuildingById(It.IsAny<Guid>());
        _buildingAdapter.VerifyAll();

        OkObjectResult? controllerResponseCasted = controllerResponse as OkObjectResult;
        Assert.IsNotNull(controllerResponseCasted);

        GetBuildingResponse? controllerValue = controllerResponseCasted.Value as GetBuildingResponse;
        Assert.IsNotNull(controllerValue);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.IsTrue(expectedBuildingValue.Equals(controllerValue));
    }
    
    #endregion

    #region Create Building

    [TestMethod]
    public void CreateBuildingRequest_OkIsReturned()
    {
        CreateBuildingRequest createBuildingRequest = new CreateBuildingRequest()
        {
            ManagerId = Guid.NewGuid(),
            Name = "Building 1",
            Address = "North Avenue",
            Location = new LocationRequest()
            {
                Latitude = 1.2345,
                Longitude = 1.2345
            },
            ConstructionCompanyId = Guid.NewGuid(),
            CommonExpenses = 300,
            Flats = new[]
            {
                new CreateFlatRequest
                {
                    Floor = 1,
                    RoomNumber = "102",
                    OwnerAssignedId = Guid.NewGuid(),
                    TotalRooms = 4,
                    TotalBaths = 2,
                    HasTerrace = true
                }
            }
        };

        CreateBuildingResponse response = new CreateBuildingResponse
        {
            Id = Guid.NewGuid()
        };

        CreatedAtActionResult expectedControllerResponse = new CreatedAtActionResult("CreateBuilding", "CreateBuilding",
            response.Id, response);

        _buildingAdapter.Setup(adapter => adapter.CreateBuilding(It.IsAny<CreateBuildingRequest>())).Returns(response);


        IActionResult controllerResponse = _buildingController.CreateBuilding(createBuildingRequest);
        _buildingAdapter.VerifyAll();


        CreatedAtActionResult? controllerResponseCasted = controllerResponse as CreatedAtActionResult;
        Assert.IsNotNull(controllerResponseCasted);

        CreateBuildingResponse? controllerValue = controllerResponseCasted.Value as CreateBuildingResponse;
        Assert.IsNotNull(controllerValue);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
        Assert.AreEqual(response.Id, controllerValue.Id);
    }

    #endregion

    #region Update Building By Id

    [TestMethod]
    public void UpdateBuildingById_OkIsReturned()
    {
        NoContentResult expectedControllerResponse = new NoContentResult();

        _buildingAdapter.Setup(adapter =>
            adapter.UpdateBuildingById(It.IsAny<Guid>(), It.IsAny<UpdateBuildingRequest>()));

        UpdateBuildingRequest updateBuildingRequest = new UpdateBuildingRequest
        {
            ManagerId = Guid.NewGuid(),
            CommonExpenses = 1000
        };
        
        IActionResult controllerResponse =
            _buildingController.UpdateBuildingById(It.IsAny<Guid>(), updateBuildingRequest);

        _buildingAdapter.Verify(
            adapter => adapter.UpdateBuildingById(It.IsAny<Guid>(), It.IsAny<UpdateBuildingRequest>()), Times.Once());

        NoContentResult? controllerResponseCasted = controllerResponse as NoContentResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }

    #endregion

    #region Delete Building By Id

    [TestMethod]
    public void DeleteBuildingRequest_NoContentIsReturned()
    {
        NoContentResult expectedControllerResponse = new NoContentResult();

        _buildingAdapter.Setup(adapter => adapter.DeleteBuildingById(It.IsAny<Guid>()));

        IActionResult controllerResponse = _buildingController.DeleteBuildingById(It.IsAny<Guid>());
        _buildingAdapter.Verify(adapter => adapter.DeleteBuildingById(It.IsAny<Guid>()), Times.Once());

        NoContentResult? controllerResponseCasted = controllerResponse as NoContentResult;
        Assert.IsNotNull(controllerResponseCasted);

        Assert.AreEqual(expectedControllerResponse.StatusCode, controllerResponseCasted.StatusCode);
    }
    
    #endregion
}