using System.Collections;
using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.FlatResponses;
using WebModel.Responses.OwnerResponses;

namespace Test.Adapters;

[TestClass]
public class FlatAdapterTest
{
    private Mock<IFlatService> _flatService;
    private FlatAdapter _flatAdapter;

    #region Initialize

    [TestInitialize]
    public void Initialize()
    {
        _flatService = new Mock<IFlatService>(MockBehavior.Strict);
        _flatAdapter = new FlatAdapter(_flatService.Object);
    }

    #endregion

    #region GetAllFlats

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
        
        GetFlatResponse adapterResponse = _flatAdapter.GetFlatById(It.IsAny<Guid>(),It.IsAny<Guid>());
        _flatService.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.Equals(adapterResponse));
    }
}