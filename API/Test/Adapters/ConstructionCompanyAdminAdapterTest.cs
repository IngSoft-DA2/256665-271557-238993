using Adapter;
using Adapter.CustomExceptions;
using Domain;
using Domain.Enums;
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

    #region Create Construction Company Admin By Invitation

    [TestMethod]
    public void CreateConstructionCompanyAdminByInvitation_ReturnsCreateConstructionCompanyAdminResponse()
    {
        CreateConstructionCompanyAdminRequest createRequest = new CreateConstructionCompanyAdminRequest
        {
            Firstname = "Firstname",
            Lastname = "Lastname",
            Email = "email@gmail.com",
            Password = "Password",
            InvitationId = Guid.NewGuid()
        };

        _constructionCompanyAdminService.Setup(service =>
            service.CreateConstructionCompanyAdminByInvitation(It.IsAny<ConstructionCompanyAdmin>(), It.IsAny<Guid>()));

        CreateConstructionCompanyAdminResponse response =
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(createRequest, null);

        Assert.IsNotNull(response);

        _constructionCompanyAdminService.Verify(createConstructionCompanyAdminService =>
            createConstructionCompanyAdminService.CreateConstructionCompanyAdminByInvitation(
                It.IsAny<ConstructionCompanyAdmin>(), It.IsAny<Guid>()), Times.Once());
    }

    [TestMethod]
    public void CreateConstructionCompanyAdminByInvitation_ThrowsObjectErrorAdapterException()
    {
        CreateConstructionCompanyAdminRequest createRequest = new CreateConstructionCompanyAdminRequest
        {
            Firstname = "Firstname",
            Lastname = "",
            Email = "email@gmail.com",
            Password = "Password",
            InvitationId = Guid.NewGuid()
        };

        _constructionCompanyAdminService.Setup(constructionCompanyAdminService =>
                constructionCompanyAdminService.CreateConstructionCompanyAdminByInvitation
                    (It.IsAny<ConstructionCompanyAdmin>(), It.IsAny<Guid>()))
            .Throws(new ObjectErrorServiceException("Specific Error"));

        Assert.ThrowsException<ObjectErrorAdapterException>(() =>
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(createRequest, null));
        _constructionCompanyAdminService.VerifyAll();
    }

    [TestMethod]
    public void CreateConstructionCompanyAdminByInvitation_ThrowsObjectRepeatedAdapterException()
    {
        CreateConstructionCompanyAdminRequest createRequest = new CreateConstructionCompanyAdminRequest
        {
            Firstname = "Firstname",
            Lastname = "Lastname",
            Email = "email@gmail.com",
            Password = "Password",
            InvitationId = Guid.NewGuid()
        };

        _constructionCompanyAdminService.Setup(constructionCompanyAdminService =>
                constructionCompanyAdminService.CreateConstructionCompanyAdminByInvitation(
                    It.IsAny<ConstructionCompanyAdmin>(), It.IsAny<Guid>()))
            .Throws(new ObjectRepeatedServiceException());

        Assert.ThrowsException<ObjectRepeatedAdapterException>(() =>
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(createRequest,
                null));
        _constructionCompanyAdminService.VerifyAll();
    }

    [TestMethod]
    public void CreateConstructionCompanyAdminByInvitation_ThrowsUnknownAdapterException()
    {
        CreateConstructionCompanyAdminRequest createRequest = new CreateConstructionCompanyAdminRequest
        {
            Firstname = "Firstname",
            Lastname = "Lastname",
            Email = "email@gmail.com",
            Password = "Password",
            InvitationId = Guid.NewGuid()
        };

        _constructionCompanyAdminService.Setup(constructionCompanyAdminService =>
                constructionCompanyAdminService.CreateConstructionCompanyAdminByInvitation(
                    It.IsAny<ConstructionCompanyAdmin>(), It.IsAny<Guid>()))
            .Throws(new Exception("Unknown Error"));

        Assert.ThrowsException<UnknownAdapterException>(() =>
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(createRequest,
                null));
        _constructionCompanyAdminService.VerifyAll();
    }

    [TestMethod]
    public void CreateConstructionCompanyAdminByInvitation_ThrowsObjectNotFoundAdapterException()
    {
        CreateConstructionCompanyAdminRequest createRequest = new CreateConstructionCompanyAdminRequest
        {
            Firstname = "Firstname",
            Lastname = "Lastname",
            Email = "email@gmail.com",
            Password = "Password",
            InvitationId = Guid.NewGuid()
        };

        _constructionCompanyAdminService.Setup(constructionCompanyAdminService =>
                constructionCompanyAdminService.CreateConstructionCompanyAdminByInvitation(
                    It.IsAny<ConstructionCompanyAdmin>(), It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundServiceException());

        Assert.ThrowsException<ObjectNotFoundAdapterException>(() =>
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(createRequest, null));
        _constructionCompanyAdminService.VerifyAll();
    }

    #endregion

    #region Create Construction Company Admin By Another Admin

    //Happy path
    [TestMethod]
    public void CreateConstructionCompanyAdminByAnotherAdmin_ReturnsCreateConstructionCompanyAdminResponse()
    {
        CreateConstructionCompanyAdminRequest createRequest = new CreateConstructionCompanyAdminRequest();

        _constructionCompanyAdminService.Setup(service =>
            service.CreateConstructionCompanyAdminForAdmins(It.IsAny<ConstructionCompanyAdmin>()));

        CreateConstructionCompanyAdminResponse response =
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(createRequest,
                SystemUserRoleEnum.ConstructionCompanyAdmin);

        Assert.IsNotNull(response);

        _constructionCompanyAdminService.Verify(createConstructionCompanyAdminService =>
            createConstructionCompanyAdminService.CreateConstructionCompanyAdminForAdmins(
                It.IsAny<ConstructionCompanyAdmin>()), Times.Once());
    }

    #endregion
}