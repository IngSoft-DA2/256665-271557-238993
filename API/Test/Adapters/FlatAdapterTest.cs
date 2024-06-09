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
    
    #region Create Flat

    [TestMethod]
    public void CreateFlat_ReturnsGetFlatResponse()
    {
        CreateFlatRequest dummyCreateRequest = new CreateFlatRequest();
        dummyCreateRequest.OwnerAssignedId = Guid.NewGuid();

        _flatService.Setup(service => service.CreateFlat(It.IsAny<Flat>()));
        _ownerService.Setup(ownerService => ownerService.GetOwnerById(dummyCreateRequest.OwnerAssignedId)).Returns(It.IsAny<Owner>());

        _flatAdapter.CreateFlat(dummyCreateRequest);
        _flatService.VerifyAll();
        _ownerService.VerifyAll();
    }

    [TestMethod]
    public void CreateFlat_ThrowsObjectNotFoundAdapterException_WhenOwnerServiceFails()
    {
        CreateFlatRequest dummyCreateRequest = new CreateFlatRequest();
        dummyCreateRequest.OwnerAssignedId = Guid.NewGuid();
        
        _ownerService.Setup(ownerService => ownerService.GetOwnerById(dummyCreateRequest.OwnerAssignedId))
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
        _ownerService.Setup(ownerSetup => ownerSetup.GetOwnerById(dummyCreateRequest.OwnerAssignedId)).Returns(It.IsAny<Owner>());

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
        _ownerService.Setup(ownerSetup => ownerSetup.GetOwnerById(dummyCreateRequest.OwnerAssignedId)).Returns(It.IsAny<Owner>());

        Assert.ThrowsException<Exception>(() => _flatAdapter.CreateFlat(dummyCreateRequest));
        _flatService.VerifyAll();
        _ownerService.VerifyAll();
    }

    #endregion
}