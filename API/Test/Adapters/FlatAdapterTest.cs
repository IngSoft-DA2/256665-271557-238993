using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.FlatRequests;
using WebModel.Requests.OwnerRequests;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.OwnerResponses;

namespace Test.Adapters;

[TestClass]
public class FlatAdapterTest
{
    private Mock<IFlatService> _flatService;
    private Mock<IOwnerService> _ownerService;
    private FlatAdapter _flatAdapter;

    #region Initialize

    [TestInitialize]
    public void Initialize()
    {
        _flatService = new Mock<IFlatService>(MockBehavior.Strict);
        _ownerService = new Mock<IOwnerService>(MockBehavior.Strict);
        _flatAdapter = new FlatAdapter(_ownerService.Object, _flatService.Object);
    }

    #endregion

    #region Get All Flats

    [TestMethod]
    public void GetAllFlats_ShouldConvertFlatsReceived_IntoGetFlatResponses()
    {
        IEnumerable<Flat> expectedServiceResponse = new List<Flat>
        {
            new Flat
            {
                Floor = 1,
                RoomNumber = 102,
                OwnerAssigned = new Owner
                {
                    Id = Guid.NewGuid(),
                    Firstname = "Michael",
                    Lastname = "Kent",
                    Email = "owner@gmail.com",
                },
                TotalRooms = 4,
                TotalBaths = 2,
                HasTerrace = true
            }
        };

        IEnumerable<GetFlatResponse> expectedAdapterResponse = expectedServiceResponse.Select(flatResponse =>
            new GetFlatResponse
            {
                Id = flatResponse.Id,
                Floor = flatResponse.Floor,
                RoomNumber = flatResponse.RoomNumber,
                GetOwnerAssigned = new GetOwnerAssignedResponse()
                {
                    Id = flatResponse.OwnerAssigned.Id,
                    Firstname = flatResponse.OwnerAssigned.Firstname,
                    Lastname = flatResponse.OwnerAssigned.Lastname,
                    Email = flatResponse.OwnerAssigned.Email
                },
                TotalRooms = flatResponse.TotalRooms,
                TotalBaths = flatResponse.TotalBaths,
                HasTerrace = flatResponse.HasTerrace
            });

        _flatService.Setup(service => service.GetAllFlats(It.IsAny<Guid>())).Returns(expectedServiceResponse);

        IEnumerable<GetFlatResponse> adapterResponse = _flatAdapter.GetAllFlats(It.IsAny<Guid>());
        _flatService.VerifyAll();

        Assert.AreEqual(expectedAdapterResponse.Count(), adapterResponse.Count());
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }

    [TestMethod]
    public void GetAllFlats_ShouldThrowNotFoundException()
    {
        _flatService.Setup(service => service.GetAllFlats(It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundServiceException());

        Assert.ThrowsException<ObjectNotFoundAdapterException>(() => _flatAdapter.GetAllFlats(It.IsAny<Guid>()));
        _flatService.VerifyAll();
    }

    [TestMethod]
    public void GetAllFlats_ShouldThrowException()
    {
        _flatService.Setup(service => service.GetAllFlats(It.IsAny<Guid>())).Throws(new Exception("Unknown Error"));

        Assert.ThrowsException<Exception>(() => _flatAdapter.GetAllFlats(It.IsAny<Guid>()));
        _flatService.VerifyAll();
    }

    #endregion

    #region Get flat by Id

    [TestMethod]
    public void GetFlatById_ReturnsGetFlatResponse()
    {
        Flat expectedServiceResponse = new Flat
        {
            Floor = 1,
            RoomNumber = 102,
            OwnerAssigned = new Owner
            {
                Id = Guid.NewGuid(),
                Firstname = "Michael",
                Lastname = "Kent",
                Email = "owner@gmail.com",
            },
            TotalRooms = 4,
            TotalBaths = 2,
            HasTerrace = true
        };

        GetFlatResponse expectedAdapterResponse = new GetFlatResponse
        {
            Id = expectedServiceResponse.Id,
            Floor = expectedServiceResponse.Floor,
            RoomNumber = expectedServiceResponse.RoomNumber,
            GetOwnerAssigned = new GetOwnerAssignedResponse()
            {
                Id = expectedServiceResponse.OwnerAssigned.Id,
                Firstname = expectedServiceResponse.OwnerAssigned.Firstname,
                Lastname = expectedServiceResponse.OwnerAssigned.Lastname,
                Email = expectedServiceResponse.OwnerAssigned.Email
            },
            TotalRooms = expectedServiceResponse.TotalRooms,
            TotalBaths = expectedServiceResponse.TotalBaths,
            HasTerrace = expectedServiceResponse.HasTerrace
        };

        _flatService.Setup(service => service.GetFlatById(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Returns(expectedServiceResponse);

        GetFlatResponse adapterResponse = _flatAdapter.GetFlatById(It.IsAny<Guid>(), It.IsAny<Guid>());
        _flatService.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.Equals(adapterResponse));
    }

    [TestMethod]
    public void GetFlatById_ThrowsObjectNotFoundAdapterException()
    {
        _flatService.Setup(service => service.GetFlatById(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundServiceException());

        Assert.ThrowsException<ObjectNotFoundAdapterException>(() => _flatAdapter.GetFlatById(It.IsAny<Guid>(),
            It.IsAny<Guid>()));
        _flatService.VerifyAll();
    }

    [TestMethod]
    public void GetFlatById_ThrowsException()
    {
        _flatService.Setup(service => service.GetFlatById(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Throws(new Exception());

        Assert.ThrowsException<Exception>(() => _flatAdapter.GetFlatById(It.IsAny<Guid>(),
            It.IsAny<Guid>()));
        _flatService.VerifyAll();
    }

    #endregion

    #region Create Flat

    [TestMethod]
    public void CreateFlat_ReturnsGetFlatResponse()
    {
        CreateFlatRequest dummyCreateRequest = new CreateFlatRequest();
        dummyCreateRequest.OwnerAssignedId = Guid.NewGuid();

        _flatService.Setup(service => service.CreateFlat(It.IsAny<Flat>()));
        _ownerService.Setup(ownerService => ownerService.GetOwnerById(dummyCreateRequest.OwnerAssignedId.Value)).Returns(It.IsAny<Owner>());

        _flatAdapter.CreateFlat(dummyCreateRequest);
        _flatService.VerifyAll();
        _ownerService.VerifyAll();
    }

    [TestMethod]
    public void CreateFlat_ThrowsObjectNotFoundAdapterException_WhenOwnerServiceFails()
    {
        CreateFlatRequest dummyCreateRequest = new CreateFlatRequest();
        dummyCreateRequest.OwnerAssignedId = Guid.NewGuid();
        
        _ownerService.Setup(ownerService => ownerService.GetOwnerById(dummyCreateRequest.OwnerAssignedId.Value))
            .Throws(new ObjectNotFoundServiceException());

        Assert.ThrowsException<ObjectNotFoundAdapterException>(() => _flatAdapter.CreateFlat(dummyCreateRequest));
        _ownerService.VerifyAll();
    }

    [TestMethod]
    public void CreateFlat_ThrowsObjectErrorAdapterException_WhenServiceFails()
    {
        CreateFlatRequest dummyCreateRequest = new CreateFlatRequest();
        dummyCreateRequest.OwnerAssignedId = Guid.NewGuid();
        
        _flatService.Setup(service => service.CreateFlat(It.IsAny<Flat>()))
            .Throws(new ObjectErrorServiceException("Specific Flat Error"));
        _ownerService.Setup(ownerSetup => ownerSetup.GetOwnerById(dummyCreateRequest.OwnerAssignedId.Value)).Returns(It.IsAny<Owner>());

        Assert.ThrowsException<ObjectErrorAdapterException>(() => _flatAdapter.CreateFlat(dummyCreateRequest));
        _flatService.VerifyAll();
        _ownerService.VerifyAll();
    }

    [TestMethod]
    public void CreateFlat_ThrowsException_WhenServiceFails()
    {
        CreateFlatRequest dummyCreateRequest = new CreateFlatRequest();
        dummyCreateRequest.OwnerAssignedId = Guid.NewGuid();    
        
        _flatService.Setup(service => service.CreateFlat(It.IsAny<Flat>()))
            .Throws(new Exception("Unknown Error"));
        _ownerService.Setup(ownerSetup => ownerSetup.GetOwnerById(dummyCreateRequest.OwnerAssignedId.Value)).Returns(It.IsAny<Owner>());

        Assert.ThrowsException<Exception>(() => _flatAdapter.CreateFlat(dummyCreateRequest));
        _flatService.VerifyAll();
        _ownerService.VerifyAll();
    }

    #endregion
}