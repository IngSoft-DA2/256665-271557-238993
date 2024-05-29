using Adapter;
using Adapter.CustomExceptions;
using Domain;
using Humanizer;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.ConstructionCompanyAdminRequests;
using WebModel.Responses.ConstructionCompanyAdminResponses;

namespace Test.Adapters;

[TestClass]
public class ConstructionCompanyAdminAdapterTest
{
    #region Initialize

    private Mock<IConstructionCompanyAdminService> _constructionCompanyAdminService;
    private ConstructionCompanyAdminAdapter _constructionCompanyAdminAdapter;


    [TestInitialize]
    public void TestInitialize()
    {
        _constructionCompanyAdminService = new Mock<IConstructionCompanyAdminService>(MockBehavior.Strict);
        _constructionCompanyAdminAdapter =
            new ConstructionCompanyAdminAdapter(_constructionCompanyAdminService.Object);
    }

    #endregion

    #region Create Construction Company Admin

    [TestMethod]
    public void CreateConstructionCompanyAdmin_ReturnsCreateConstructionCompanyAdminResponse()
    {
        CreateConstructionCompanyAdminRequest createRequest = new CreateConstructionCompanyAdminRequest();

        _constructionCompanyAdminService.Setup(service =>
            service.CreateConstructionCompanyAdminByInvitation(It.IsAny<ConstructionCompanyAdmin>(), It.IsAny<Guid>()));

        CreateConstructionCompanyAdminResponse response =
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdminByInvitation(createRequest, It.IsAny<Guid>());

        Assert.IsNotNull(response);

        _constructionCompanyAdminService.Verify(createConstructionCompanyAdminService =>
            createConstructionCompanyAdminService.CreateConstructionCompanyAdminByInvitation(
                It.IsAny<ConstructionCompanyAdmin>(), It.IsAny<Guid>()), Times.Once());
        
    }

    [TestMethod]
    public void CreateConstructionCompanyAdmin_ThrowsObjectErrorAdapterException()
    {
        CreateConstructionCompanyAdminRequest constructionCompanyDummy = new CreateConstructionCompanyAdminRequest();
        Guid invitationIdDummy = Guid.NewGuid();

        _constructionCompanyAdminService.Setup(constructionCompanyAdminService =>
                constructionCompanyAdminService.CreateConstructionCompanyAdminByInvitation(It.IsAny<ConstructionCompanyAdmin>(),
                    It.IsAny<Guid>()))
            .Throws(new ObjectErrorServiceException("Specific Error"));

        Assert.ThrowsException<ObjectErrorAdapterException>(() =>
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdminByInvitation(constructionCompanyDummy,
                invitationIdDummy));
        _constructionCompanyAdminService.VerifyAll();
    }

    [TestMethod]
    public void CreateConstructionCompanyAdmin_ThrowsObjectRepeatedAdapterException()
    {
        CreateConstructionCompanyAdminRequest createDummyRequest = new CreateConstructionCompanyAdminRequest();
        Guid invitationIdDummy = Guid.NewGuid();


        _constructionCompanyAdminService.Setup(constructionCompanyAdminService =>
                constructionCompanyAdminService.CreateConstructionCompanyAdminByInvitation(It.IsAny<ConstructionCompanyAdmin>(),
                    It.IsAny<Guid>()))
            .Throws(new ObjectRepeatedServiceException());

        Assert.ThrowsException<ObjectRepeatedAdapterException>(() =>
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdminByInvitation(createDummyRequest, invitationIdDummy));
        _constructionCompanyAdminService.VerifyAll();
    }

    [TestMethod]
    public void CreateConstructionCompanyAdmin_ThrowsUnknownAdapterException()
    {
        CreateConstructionCompanyAdminRequest createDummyRequest = new CreateConstructionCompanyAdminRequest();
        Guid invitationIdDummy = Guid.NewGuid();

        _constructionCompanyAdminService.Setup(constructionCompanyAdminService =>
                constructionCompanyAdminService.CreateConstructionCompanyAdminByInvitation(It.IsAny<ConstructionCompanyAdmin>(),
                    It.IsAny<Guid>()))
            .Throws(new Exception("Unknown Error"));

        Assert.ThrowsException<UnknownAdapterException>(() =>
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdminByInvitation(createDummyRequest, invitationIdDummy));
        _constructionCompanyAdminService.VerifyAll();
    }

    [TestMethod]
    public void CreateConstructionCompanyAdmin_ThrowsObjectNotFoundAdapterException()
    {
        CreateConstructionCompanyAdminRequest createDummyRequest = new CreateConstructionCompanyAdminRequest();
        Guid invitationIdDummy = Guid.NewGuid();

        _constructionCompanyAdminService.Setup(constructionCompanyAdminService =>
                constructionCompanyAdminService.CreateConstructionCompanyAdminByInvitation(It.IsAny<ConstructionCompanyAdmin>(), It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundServiceException());

        Assert.ThrowsException<ObjectNotFoundAdapterException>(() =>
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdminByInvitation(createDummyRequest, invitationIdDummy));
        _constructionCompanyAdminService.VerifyAll();
    }

    #endregion
}