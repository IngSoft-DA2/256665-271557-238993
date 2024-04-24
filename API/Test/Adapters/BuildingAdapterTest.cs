using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.BuildingRequests;
using WebModel.Responses.BuildingResponses;
using WebModel.Responses.ConstructionCompanyResponses;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.OwnerResponses;

namespace Test.Adapters;

[TestClass]
public class BuildingAdapterTest
{
    #region Initialize

    private Mock<IBuildingService> _buildingService;
    private Mock<IConstructionCompanyService> _constructionCompanyService;
    private Mock<IOwnerService> _ownerService;
    private BuildingAdapter _buildingAdapter;

    [TestInitialize]
    public void Initialize()
    {
        _buildingService = new Mock<IBuildingService>(MockBehavior.Strict);
        _constructionCompanyService = new Mock<IConstructionCompanyService>(MockBehavior.Strict);
        _ownerService = new Mock<IOwnerService>(MockBehavior.Strict);
        _buildingAdapter = new BuildingAdapter(_buildingService.Object, _constructionCompanyService.Object,
            _ownerService.Object);
    }

    #endregion

    #region Get all buildings

    [TestMethod]
    public void GetAllBuilding_ReturnsGetBuildingResponses()
    {
        IEnumerable<Building> expectedServiceResponse = new List<Building>
        {
            new Building
            {
                Id = Guid.NewGuid(),
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
                    Name = "constructionCompany"
                },
                CommonExpenses = 1000,
                Flats = new List<Flat>
                {
                    new Flat
                    {
                        Id = Guid.NewGuid(),
                        Floor = 1,
                        RoomNumber = 102,
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

        _buildingService.Setup(service => service.GetAllBuildings()).Returns(expectedServiceResponse);

        IEnumerable<GetBuildingResponse> adapterResponse = _buildingAdapter.GetAllBuildings();
        _buildingService.VerifyAll();

        Assert.AreEqual(expectedAdapterResponse.Count(), adapterResponse.Count());
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }

    [TestMethod]
    public void GetAllBuildings_ThrowsUnknownAdapterException()
    {
        _buildingService.Setup(service => service.GetAllBuildings()).Throws<Exception>();

        Assert.ThrowsException<UnknownAdapterException>(() => _buildingAdapter.GetAllBuildings());
        _buildingService.VerifyAll();
    }

    #endregion

    #region Get building by id

    [TestMethod]
    public void GetBuildingById_ReturnsBuildingResponse()
    {
        Building expectedServiceResponse = new Building
        {
            Id = Guid.NewGuid(),
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
                Name = "constructionCompany"
            },
            CommonExpenses = 1000,
            Flats = new List<Flat>
            {
                new Flat
                {
                    Id = Guid.NewGuid(),
                    Floor = 1,
                    RoomNumber = 102,
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

    [TestMethod]
    public void CreateBuilding_ReturnsCreateBuildingResponse()
    {
        _buildingService.Setup(buildingService => buildingService.CreateBuilding(It.IsAny<Building>()));

        _constructionCompanyService
            .Setup(constructionCompanyService =>
                constructionCompanyService.GetConstructionCompanyById(It.IsAny<Guid>()))
            .Returns(new ConstructionCompany());
        
        CreateBuildingRequest dummyCreateRequest = new CreateBuildingRequest();
        LocationRequest dummyLocationRequest = new LocationRequest();
        
        dummyCreateRequest.Location = dummyLocationRequest;
        
        
        CreateBuildingResponse buildingResponse = _buildingAdapter.CreateBuilding(dummyCreateRequest);
        
        _constructionCompanyService.VerifyAll();
        _ownerService.VerifyAll();
        _buildingService.VerifyAll();

        Assert.IsNotNull(buildingResponse);
        Assert.IsInstanceOfType<Guid>(buildingResponse.Id);
    }
}