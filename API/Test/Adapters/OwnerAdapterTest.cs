using System.Collections;
using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.FlatRequests;
using WebModel.Requests.OwnerRequests;
using WebModel.Responses.ManagerResponses;
using WebModel.Responses.OwnerResponses;

namespace Test.Adapters;

[TestClass]
public class OwnerAdapterTest
{
    #region Initialize

    private Mock<IOwnerService> _ownerService;
    private OwnerAdapter _ownerAdapter;

    [TestInitialize]
    public void Initialize()
    {
        _ownerService = new Mock<IOwnerService>(MockBehavior.Strict);
        _ownerAdapter = new OwnerAdapter(_ownerService.Object);
    }

    #endregion

    #region Get All Owners

    [TestMethod]
    public void GetAllOwners_ReturnsGetAllOwnersResponse()
    {
        IEnumerable<GetOwnerResponse> expectedAdapterResponse = new List<GetOwnerResponse>()
        {
            new GetOwnerResponse
            {
                Id = Guid.NewGuid(),
                Firstname = "OwnerName",
                Lastname = "OwnerLastname",
                Email = "owner@gmail.com",
            }
        };
        IEnumerable<Owner> expectedServiceResponse = new List<Owner>()
        {
            new Owner
            {
                Id = expectedAdapterResponse.First().Id,
                Firstname = expectedAdapterResponse.First().Firstname,
                Lastname = expectedAdapterResponse.First().Lastname,
                Email = expectedAdapterResponse.First().Email,
                Flats = new List<Flat>()
            }
        };

        _ownerService.Setup(service => service.GetAllOwners()).Returns(expectedServiceResponse);

        IEnumerable<GetOwnerResponse> adapterResponse = _ownerAdapter.GetAllOwners();

        Assert.AreEqual(expectedAdapterResponse.Count(), adapterResponse.Count());
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }

    [TestMethod]
    public void GetAllOwners_ThrowsException()
    {
        _ownerService.Setup(service => service.GetAllOwners()).Throws(new Exception());
        Assert.ThrowsException<Exception>(() => _ownerAdapter.GetAllOwners());
    }

    #endregion

    #region Get owner by Id

    [TestMethod]
    public void GetOwnerById_ReturnsGetOwnerResponse()
    {
        Guid ownerId = Guid.NewGuid();

        GetOwnerResponse expectedAdapterResponse = new GetOwnerResponse
        {
            Id = ownerId,
            Firstname = "OwnerFirstname",
            Lastname = "OwnerLastname",
            Email = "owner@gmail.com"
        };

        Owner expectedServiceResponse = new Owner
        {
            Id = ownerId,
            Firstname = expectedAdapterResponse.Firstname,
            Lastname = expectedAdapterResponse.Lastname,
            Email = expectedAdapterResponse.Email,
            Flats = new List<Flat>()
        };


        _ownerService.Setup(service => service.GetOwnerById(It.IsAny<Guid>())).Returns(expectedServiceResponse);

        GetOwnerResponse adapterResponse = _ownerAdapter.GetOwnerById(ownerId);
        _ownerService.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.Equals(adapterResponse));
    }

    [TestMethod]
    public void GetOwnerById_ThrowsObjectNotFoundAdapterException()
    {
        _ownerService.Setup(service => service.GetOwnerById(It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundServiceException());
        Assert.ThrowsException<ObjectNotFoundAdapterException>(() => _ownerAdapter.GetOwnerById(It.IsAny<Guid>()));
    }

    [TestMethod]
    public void GetOwnerById_ThrowsException()
    {
        _ownerService.Setup(service => service.GetOwnerById(It.IsAny<Guid>())).Throws(new Exception());
        Assert.ThrowsException<Exception>(() => _ownerAdapter.GetOwnerById(It.IsAny<Guid>()));
    }

    #endregion

    #region Create owner

    [TestMethod]
    public void CreateOwner_ReturnsCreateOwnerResponse()
    {
        CreateOwnerRequest dummyRequest = new CreateOwnerRequest
        {
            Firstname = "Firstname",
            Lastname = "Lastname",
            Email = "owner@gmail.com"
        };
        _ownerService.Setup(service => service.CreateOwner(It.IsAny<Owner>()));

        CreateOwnerResponse adapterResponse = _ownerAdapter.CreateOwner(dummyRequest);
        _ownerService.VerifyAll();
        Assert.IsNotNull(adapterResponse.Id);
    }

    [TestMethod]
    public void CreateOwner_ThrowsObjectErrorAdapterException_WhenServiceFails()
    {
        CreateOwnerRequest dummyRequest = new CreateOwnerRequest();
        _ownerService.Setup(service => service.CreateOwner(It.IsAny<Owner>()))
            .Throws(new ObjectErrorServiceException("Specific error detected at service"));

        Assert.ThrowsException<ObjectErrorAdapterException>(() =>
            _ownerAdapter.CreateOwner(dummyRequest));
        _ownerService.VerifyAll();
    }

    [TestMethod]
    public void CreateOwner_ThrowsException()
    {
        CreateOwnerRequest dummyRequest = new CreateOwnerRequest();
        _ownerService.Setup(service => service.CreateOwner(It.IsAny<Owner>()))
            .Throws(new Exception("Internal Server Error"));

        Assert.ThrowsException<Exception>(() => _ownerAdapter.CreateOwner(dummyRequest));
        _ownerService.VerifyAll();
    }

    #endregion

    #region Update owner

    [TestMethod]
    public void UpdateOwner_ReturnsOwnerUpdateResponse()
    {
        UpdateOwnerRequest dummyUpdate = new UpdateOwnerRequest
        {
            Firstname = "Firstname",
            Lastname = "Lastname",
            Email = "owner@gmail.com"
        };
        _ownerService.Setup(service => service.UpdateOwnerById(It.IsAny<Owner>()));

        _ownerAdapter.UpdateOwnerById(It.IsAny<Guid>(), dummyUpdate);
        _ownerService.Verify(service => service.UpdateOwnerById(It.IsAny<Owner>()), Times.Once);
    }

    [TestMethod]
    public void UpdateOwner_ThrowsObjectErrorAdapterException()
    {
        UpdateOwnerRequest dummyUpdate = new UpdateOwnerRequest();
        _ownerService.Setup(service => service.UpdateOwnerById(It.IsAny<Owner>()))
            .Throws(new ObjectErrorServiceException("Specific error detected at service"));

        Assert.ThrowsException<ObjectErrorAdapterException>((() =>
            _ownerAdapter.UpdateOwnerById(It.IsAny<Guid>(), dummyUpdate)));
        _ownerService.VerifyAll();
    }

    [TestMethod]
    public void UpdateOwner_ThrowsObjectNotFoundAdapterException()
    {
        UpdateOwnerRequest dummyUpdate = new UpdateOwnerRequest();
        _ownerService.Setup(service => service.UpdateOwnerById(It.IsAny<Owner>()))
            .Throws(new ObjectNotFoundServiceException());

        Assert.ThrowsException<ObjectNotFoundAdapterException>((() =>
            _ownerAdapter.UpdateOwnerById(It.IsAny<Guid>(), dummyUpdate)));
        _ownerService.VerifyAll();
    }

    [TestMethod]
    public void UpdateOwner_ThrowsRepeatedObjectAdapterException()
    {
        UpdateOwnerRequest dummyUpdate = new UpdateOwnerRequest();
        _ownerService.Setup(service => service.UpdateOwnerById(It.IsAny<Owner>()))
            .Throws(new ObjectRepeatedServiceException());

        Assert.ThrowsException<ObjectRepeatedAdapterException>((() =>
            _ownerAdapter.UpdateOwnerById(It.IsAny<Guid>(), dummyUpdate)));
        _ownerService.VerifyAll();
    }

    [TestMethod]
    public void UpdateOwner_ThrowsException()
    {
        UpdateOwnerRequest dummyUpdate = new UpdateOwnerRequest();
        _ownerService.Setup(service => service.UpdateOwnerById(It.IsAny<Owner>()))
            .Throws(new Exception("Internal Server Error"));

        Assert.ThrowsException<Exception>((() =>
            _ownerAdapter.UpdateOwnerById(It.IsAny<Guid>(), dummyUpdate)));
        _ownerService.VerifyAll();
    }

    #endregion
}