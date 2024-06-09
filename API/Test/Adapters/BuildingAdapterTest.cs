using System.Collections;
using Adapter;
using Adapter.CustomExceptions;
using Domain;
using Domain.Enums;
using IServiceLogic;
using Microsoft.VisualBasic.CompilerServices;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.BuildingRequests;
using WebModel.Requests.FlatRequests;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.ConstructionCompanyResponses;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.ManagerResponses;
using WebModel.Responses.OwnerResponses;

namespace Test.Adapters;

[TestClass]
public class BuildingAdapterTest
{
    #region Initialize

    private Mock<IBuildingService> _buildingService;
    private Mock<IConstructionCompanyService> _constructionCompanyService;
    private Mock<IOwnerService> _ownerService;
    private Mock<IManagerService> _managerService;
    private BuildingAdapter _buildingAdapter;

    [TestInitialize]
    public void Initialize()
    {
        _buildingService = new Mock<IBuildingService>(MockBehavior.Strict);
        _constructionCompanyService = new Mock<IConstructionCompanyService>(MockBehavior.Strict);
        _ownerService = new Mock<IOwnerService>(MockBehavior.Strict);
        _managerService = new Mock<IManagerService>(MockBehavior.Strict);


        _buildingAdapter = new BuildingAdapter(_buildingService.Object, _constructionCompanyService.Object,
            _ownerService.Object, _managerService.Object);
    }

    #endregion

    #region Get all buildings

    [TestMethod]
    public void GetAllBuilding_ReturnsGetBuildingResponses()
    {
        Guid managerId = Guid.NewGuid();
        IEnumerable<Building> expectedServiceResponse = new List<Building>
        {
            new Building
            {
                Id = Guid.NewGuid(),
                ManagerId = managerId,
                Manager = new Manager
                {
                    Id = managerId,
                    Firstname = "ManagerFirstname",
                    Email = "ManagerEmaiL@gmail.com",
                    Password = "Password123",
                    Buildings = new List<Building>(),
                    Requests = new List<MaintenanceRequest>(),
                    Role = SystemUserRoleEnum.Manager
                },
                Name = "Building 1",
                Address = "Address 1",
                Location = new Location
                {
                    Latitude = 1.23,
                    Longitude = 4.56
                },
                ConstructionCompany = new ConstructionCompany
                {
                    Id = Guid.NewGuid(),
                    Name = "constructionCompany",
                    UserCreatorId = Guid.NewGuid(),
                    Buildings = new List<Building>()
                },
                CommonExpenses = 1000,
                Flats = new List<Flat>
                {
                    new Flat
                    {
                        Id = Guid.NewGuid(),
                        Floor = 1,
                        RoomNumber = "102",
                        OwnerAssigned = new Owner
                        {
                            Id = Guid.NewGuid(),
                            Firstname = "OwnerFirstname",
                            Lastname = "OwnerLastname",
                            Email = "owner@gmail.com",
                            Flats = new List<Flat>()
                        },
                        TotalRooms = 4,
                        TotalBaths = 2,
                        HasTerrace = true
                    }
                }
            }
        };
        IEnumerable<GetBuildingResponse> expectedAdapterResponse = new List<GetBuildingResponse>
        {
            new GetBuildingResponse
            {
                Id = expectedServiceResponse.First().Id,
                Manager = new GetManagerResponse
                {
                    Id = managerId,
                    Name = expectedServiceResponse.First().Manager.Firstname,
                    Email = expectedServiceResponse.First().Manager.Email,
                    BuildingsId = expectedServiceResponse.First().Manager.Buildings
                        .Select(building => building.Id).ToList(),
                    MaintenanceRequestsId = expectedServiceResponse.First().Manager.Requests
                        .Select(request => request.Id).ToList()
                },
                Name = expectedServiceResponse.First().Name,
                Address = expectedServiceResponse.First().Address,
                Location = new LocationResponse
                {
                    Latitude = expectedServiceResponse.First().Location.Latitude,
                    Longitude = expectedServiceResponse.First().Location.Longitude
                },
                ConstructionCompany = new GetConstructionCompanyResponse
                {
                    Id = expectedServiceResponse.First().ConstructionCompany.Id,
                    Name = expectedServiceResponse.First().ConstructionCompany.Name,
                    UserCreatorId = expectedServiceResponse.First().ConstructionCompany.UserCreatorId,
                    BuildingsId = expectedServiceResponse.First().ConstructionCompany.Buildings
                        .Select(building => building.Id).ToList()
                },
                CommonExpenses = 1000,
                Flats = new List<GetFlatResponse>
                {
                    new GetFlatResponse
                    {
                        Id = expectedServiceResponse.First().Flats.First().Id,
                        Floor = expectedServiceResponse.First().Flats.First().Floor,
                        RoomNumber = expectedServiceResponse.First().Flats.First().RoomNumber,
                        OwnerAssigned = new GetOwnerResponse()
                        {
                            Id = expectedServiceResponse.First().Flats.First().OwnerAssigned.Id,
                            Firstname = expectedServiceResponse.First().Flats.First().OwnerAssigned.Firstname,
                            Lastname = expectedServiceResponse.First().Flats.First().OwnerAssigned.Lastname,
                            Email = expectedServiceResponse.First().Flats.First().OwnerAssigned.Email
                        },
                        TotalRooms = expectedServiceResponse.First().Flats.First().TotalRooms,
                        TotalBaths = expectedServiceResponse.First().Flats.First().TotalBaths,
                        HasTerrace = expectedServiceResponse.First().Flats.First().HasTerrace
                    }
                }
            }
        };

        _buildingService.Setup(service => service.GetAllBuildings(It.IsAny<Guid>())).Returns(expectedServiceResponse);

        IEnumerable<GetBuildingResponse> adapterResponse = _buildingAdapter.GetAllBuildings(It.IsAny<Guid>());
        _buildingService.VerifyAll();

        Assert.AreEqual(expectedAdapterResponse.Count(), adapterResponse.Count());
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }

    [TestMethod]
    public void GetAllBuildings_ThrowsUnknownAdapterException()
    {
        _buildingService.Setup(service => service.GetAllBuildings(It.IsAny<Guid>())).Throws<Exception>();

        Assert.ThrowsException<UnknownAdapterException>(() => _buildingAdapter.GetAllBuildings(It.IsAny<Guid>()));
        _buildingService.VerifyAll();
    }

    #endregion

    #region Get building by id

    [TestMethod]
    public void GetBuildingById_ReturnsBuildingResponse()
    {
        Guid managerId = Guid.NewGuid();
        Building expectedServiceResponse = new Building
        {
            Id = Guid.NewGuid(),
            ManagerId = managerId,
            Name = "Building 1",
            Address = "Address 1",
            Location = new Location
            {
                Latitude = 1.23,
                Longitude = 4.56
            },
            ConstructionCompany = new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "constructionCompany",
                UserCreatorId = Guid.NewGuid(),
                Buildings = new List<Building>()
            },
            Manager = new Manager
            {
                Id = managerId,
                Firstname = "ManagerFirstname",
                Email = "manager@gmail.com",
                Password = "Password123",
                Buildings = new List<Building>(),
                Requests = new List<MaintenanceRequest>()
            },

            CommonExpenses = 1000,
            Flats = new List<Flat>
            {
                new Flat
                {
                    Id = Guid.NewGuid(),
                    Floor = 1,
                    RoomNumber = "102",
                    OwnerAssigned = new Owner
                    {
                        Id = Guid.NewGuid(),
                        Firstname = "OwnerFirstname",
                        Lastname = "OwnerLastname",
                        Email = "owner@gmail.com",
                    },
                    TotalRooms = 4,
                    TotalBaths = 2,
                    HasTerrace = true
                }
            }
        };

        GetBuildingResponse expectedAdapterResponse = new GetBuildingResponse
        {
            Id = expectedServiceResponse.Id,
            Manager = new GetManagerResponse
            {
                Id = managerId,
                Name = expectedServiceResponse.Manager.Firstname,
                Email = expectedServiceResponse.Manager.Email,
                BuildingsId = expectedServiceResponse.Manager.Buildings.Select(building => building.Id).ToList(),
                MaintenanceRequestsId = expectedServiceResponse.Manager.Requests.Select(request => request.Id).ToList()
            },
            Name = expectedServiceResponse.Name,
            Address = expectedServiceResponse.Address,
            Location = new LocationResponse
            {
                Latitude = expectedServiceResponse.Location.Latitude,
                Longitude = expectedServiceResponse.Location.Longitude
            },
            ConstructionCompany = new GetConstructionCompanyResponse
            {
                Id = expectedServiceResponse.ConstructionCompany.Id,
                Name = expectedServiceResponse.ConstructionCompany.Name,
                UserCreatorId = expectedServiceResponse.ConstructionCompany.UserCreatorId,
                BuildingsId = expectedServiceResponse.ConstructionCompany.Buildings.Select(building => building.Id)
                    .ToList()
            },
            CommonExpenses = 1000,
            Flats = new List<GetFlatResponse>
            {
                new GetFlatResponse
                {
                    Id = expectedServiceResponse.Flats.First().Id,
                    Floor = expectedServiceResponse.Flats.First().Floor,
                    RoomNumber = expectedServiceResponse.Flats.First().RoomNumber,
                    OwnerAssigned = new GetOwnerResponse()
                    {
                        Id = expectedServiceResponse.Flats.First().OwnerAssigned.Id,
                        Firstname = expectedServiceResponse.Flats.First().OwnerAssigned.Firstname,
                        Lastname = expectedServiceResponse.Flats.First().OwnerAssigned.Lastname,
                        Email = expectedServiceResponse.Flats.First().OwnerAssigned.Email
                    },
                    TotalRooms = expectedServiceResponse.Flats.First().TotalRooms,
                    TotalBaths = expectedServiceResponse.Flats.First().TotalBaths,
                    HasTerrace = expectedServiceResponse.Flats.First().HasTerrace
                }
            }
        };

        _buildingService.Setup(service => service.GetBuildingById(It.IsAny<Guid>())).Returns(expectedServiceResponse);

        GetBuildingResponse adapterResponse = _buildingAdapter.GetBuildingById(Guid.NewGuid());
        _buildingService.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.Equals(adapterResponse));
    }

    [TestMethod]
    public void GetBuildingById_ThrowsNotFoundAdapterException()
    {
        _buildingService.Setup(service => service.GetBuildingById(It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundServiceException());

        Assert.ThrowsException<ObjectNotFoundAdapterException>(() => _buildingAdapter.GetBuildingById(Guid.NewGuid()));
        _buildingService.VerifyAll();
    }

    [TestMethod]
    public void GetBuildingById_ThrowsUnknownAdapterException()
    {
        _buildingService.Setup(service => service.GetBuildingById(It.IsAny<Guid>())).Throws<Exception>();

        Assert.ThrowsException<UnknownAdapterException>(() => _buildingAdapter.GetBuildingById(Guid.NewGuid()));
        _buildingService.VerifyAll();
    }

    #endregion

    #region Create building

    [TestMethod]
    public void CreateBuilding_ReturnsCreateBuildingResponse()
    {
        _buildingService.Setup(buildingService => buildingService.CreateBuilding(It.IsAny<Building>()));

        _constructionCompanyService
            .Setup(constructionCompanyService =>
                constructionCompanyService.GetConstructionCompanyById(It.IsAny<Guid>()))
            .Returns(new ConstructionCompany());
        _ownerService.Setup(ownerService => ownerService.GetOwnerById(It.IsAny<Guid>()))
            .Returns(new Owner());
        
        _managerService.Setup(ownerService => ownerService.GetManagerById(It.IsAny<Guid>()))
            .Returns(new Manager());

        CreateBuildingRequest dummyCreateRequest = new CreateBuildingRequest();
        LocationRequest dummyLocationRequest = new LocationRequest();
        IEnumerable<CreateFlatRequest> dummyFlats = new List<CreateFlatRequest>
        {
            new CreateFlatRequest
            {
                Floor = 1,
                RoomNumber = "101",
                OwnerAssignedId = Guid.NewGuid(),
                TotalRooms = 4,
                TotalBaths = 2,
                HasTerrace = true
            }
        };
        dummyCreateRequest.Location = dummyLocationRequest;
        dummyCreateRequest.Flats = dummyFlats;

        CreateBuildingResponse buildingResponse = _buildingAdapter.CreateBuilding(dummyCreateRequest);

        _constructionCompanyService.VerifyAll();
        _ownerService.VerifyAll();
        _buildingService.VerifyAll();
        _managerService.VerifyAll();

        Assert.IsNotNull(buildingResponse);
        Assert.IsInstanceOfType<Guid>(buildingResponse.Id);
    }

    [TestMethod]
    public void CreateBuilding_ThrowsNotFoundAdapterException()
    {
        _buildingService.Setup(buildingService => buildingService.CreateBuilding(It.IsAny<Building>()))
            .Throws(new ObjectNotFoundServiceException());

        _constructionCompanyService
            .Setup(constructionCompanyService =>
                constructionCompanyService.GetConstructionCompanyById(It.IsAny<Guid>()))
            .Returns(new ConstructionCompany());
        _managerService.Setup(ownerService => ownerService.GetManagerById(It.IsAny<Guid>()))
            .Returns(new Manager());

        CreateBuildingRequest dummyCreateRequest = new CreateBuildingRequest();
        LocationRequest dummyLocationRequest = new LocationRequest();

        dummyCreateRequest.Location = dummyLocationRequest;

        Assert.ThrowsException<ObjectNotFoundAdapterException>(
            () => _buildingAdapter.CreateBuilding(dummyCreateRequest));

        _constructionCompanyService.VerifyAll();
        _buildingService.VerifyAll();
        _managerService.VerifyAll();
    }

    [TestMethod]
    public void CreateBuilding_ThrowsObjectErrorException()
    {
        _buildingService.Setup(buildingService => buildingService.CreateBuilding(It.IsAny<Building>()))
            .Throws(new ObjectErrorServiceException("Specific Error"));

        _constructionCompanyService
            .Setup(constructionCompanyService =>
                constructionCompanyService.GetConstructionCompanyById(It.IsAny<Guid>()))
            .Returns(new ConstructionCompany());
        _managerService.Setup(ownerService => ownerService.GetManagerById(It.IsAny<Guid>()))
            .Returns(new Manager());

        CreateBuildingRequest dummyCreateRequest = new CreateBuildingRequest();
        LocationRequest dummyLocationRequest = new LocationRequest();

        dummyCreateRequest.Location = dummyLocationRequest;

        Assert.ThrowsException<ObjectErrorAdapterException>(() => _buildingAdapter.CreateBuilding(dummyCreateRequest));

        _constructionCompanyService.VerifyAll();
        _buildingService.VerifyAll();
        _managerService.VerifyAll();
    }

    [TestMethod]
    public void CreateBuilding_ThrowsRepeatedObjectException()
    {
        _buildingService.Setup(buildingService => buildingService.CreateBuilding(It.IsAny<Building>()))
            .Throws(new ObjectRepeatedServiceException());

        _constructionCompanyService
            .Setup(constructionCompanyService =>
                constructionCompanyService.GetConstructionCompanyById(It.IsAny<Guid>()))
            .Returns(new ConstructionCompany());
        
        _managerService.Setup(ownerService => ownerService.GetManagerById(It.IsAny<Guid>()))
            .Returns(new Manager());

        CreateBuildingRequest dummyCreateRequest = new CreateBuildingRequest();
        LocationRequest dummyLocationRequest = new LocationRequest();

        dummyCreateRequest.Location = dummyLocationRequest;

        Assert.ThrowsException<ObjectRepeatedAdapterException>(
            () => _buildingAdapter.CreateBuilding(dummyCreateRequest));

        _constructionCompanyService.VerifyAll();
        _buildingService.VerifyAll();
        _managerService.VerifyAll();
    }

    [TestMethod]
    public void CreateBuilding_ThrowsUnknownAdapterException()
    {
        _buildingService.Setup(buildingService => buildingService.CreateBuilding(It.IsAny<Building>()))
            .Throws(new Exception());

        _constructionCompanyService
            .Setup(constructionCompanyService =>
                constructionCompanyService.GetConstructionCompanyById(It.IsAny<Guid>()))
            .Returns(new ConstructionCompany());
        
        _managerService.Setup(ownerService => ownerService.GetManagerById(It.IsAny<Guid>()))
            .Returns(new Manager());

        CreateBuildingRequest dummyCreateRequest = new CreateBuildingRequest();
        LocationRequest dummyLocationRequest = new LocationRequest();

        dummyCreateRequest.Location = dummyLocationRequest;

        Assert.ThrowsException<UnknownAdapterException>(() => _buildingAdapter.CreateBuilding(dummyCreateRequest));

        _constructionCompanyService.VerifyAll();
        _buildingService.VerifyAll();
        _managerService.VerifyAll();
        
    }

    #endregion

    #region Update building

    [TestMethod]
    public void UpdateBuildingById_UpdatesSuccessfully()
    {
        Manager dummyManager = new Manager();
        UpdateBuildingRequest dummyUpdateRequest = new UpdateBuildingRequest();

        _buildingService.Setup(service => service.UpdateBuilding(It.IsAny<Building>()));
        _managerService.Setup(managerService => managerService.GetManagerById(It.IsAny<Guid>())).Returns(dummyManager);

        _buildingAdapter.UpdateBuildingById(Guid.NewGuid(), dummyUpdateRequest);
        _managerService.VerifyAll();
        _buildingService.Verify(service => service.UpdateBuilding(It.IsAny<Building>()), Times.Once);
    }

    [TestMethod]
    public void UpdateBuildingById_ThrowsNotFoundAdapterException()
    {
        Manager dummyManager = new Manager();
        UpdateBuildingRequest dummyUpdateRequest = new UpdateBuildingRequest();

        _buildingService.Setup(service => service.UpdateBuilding(It.IsAny<Building>()))
            .Throws(new ObjectNotFoundServiceException());

        _managerService.Setup(managerService => managerService.GetManagerById(It.IsAny<Guid>())).Returns(dummyManager);

        Assert.ThrowsException<ObjectNotFoundAdapterException>(() =>
            _buildingAdapter.UpdateBuildingById(Guid.NewGuid(), dummyUpdateRequest));

        _buildingService.VerifyAll();
        _managerService.VerifyAll();
    }

    [TestMethod]
    public void UpdateBuildingId_ThrowsObjectErrorException()
    {
        Manager dummyManager = new Manager();
        UpdateBuildingRequest dummyUpdateRequest = new UpdateBuildingRequest();

        _buildingService.Setup(service => service.UpdateBuilding(It.IsAny<Building>()))
            .Throws(new ObjectErrorServiceException("Specific Error"));

        _managerService.Setup(managerService => managerService.GetManagerById(It.IsAny<Guid>())).Returns(dummyManager);

        Assert.ThrowsException<ObjectErrorAdapterException>(() =>
            _buildingAdapter.UpdateBuildingById(Guid.NewGuid(), dummyUpdateRequest));

        _buildingService.VerifyAll();
        _managerService.VerifyAll();
    }

    [TestMethod]
    public void UpdateBuildingId_ThrowsUnknownAdapterException()
    {
        Manager dummyManager = new Manager();
        UpdateBuildingRequest dummyUpdateRequest = new UpdateBuildingRequest();

        _buildingService.Setup(service => service.UpdateBuilding(It.IsAny<Building>()))
            .Throws(new Exception());

        _managerService.Setup(managerService => managerService.GetManagerById(It.IsAny<Guid>())).Returns(dummyManager);

        Assert.ThrowsException<UnknownAdapterException>(() =>
            _buildingAdapter.UpdateBuildingById(Guid.NewGuid(), dummyUpdateRequest));

        _buildingService.VerifyAll();
        _managerService.VerifyAll();
    }

    #endregion

    #region Delete building

    [TestMethod]
    public void DeleteBuildingById_DeletesSuccessfully()
    {
        _buildingService.Setup(service => service.DeleteBuilding(It.IsAny<Guid>()));

        _buildingAdapter.DeleteBuildingById(Guid.NewGuid());
        _buildingService.Verify(_buildingService => _buildingService.DeleteBuilding(It.IsAny<Guid>()), Times.Once);
    }

    [TestMethod]
    public void DeleteBuildingById_ThrowsNotFoundAdapterException()
    {
        _buildingService.Setup(service => service.DeleteBuilding(It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundServiceException());

        Assert.ThrowsException<ObjectNotFoundAdapterException>(
            () => _buildingAdapter.DeleteBuildingById(Guid.NewGuid()));
        _buildingService.VerifyAll();
    }

    [TestMethod]
    public void DeleteBuildingById_ThrowsObjectErrorAdapterException()
    {
        _buildingService.Setup(service => service.DeleteBuilding(It.IsAny<Guid>()))
            .Throws(new ObjectErrorServiceException("Specific Error"));

        Assert.ThrowsException<ObjectErrorAdapterException>(() => _buildingAdapter.DeleteBuildingById(Guid.NewGuid()));
        _buildingService.VerifyAll();
    }

    [TestMethod]
    public void DeleteBuildingById_ThrowsUnknownAdapterException()
    {
        _buildingService.Setup(service => service.DeleteBuilding(It.IsAny<Guid>()))
            .Throws(new Exception());

        Assert.ThrowsException<UnknownAdapterException>(() => _buildingAdapter.DeleteBuildingById(Guid.NewGuid()));
        _buildingService.VerifyAll();
    }

    #endregion
}