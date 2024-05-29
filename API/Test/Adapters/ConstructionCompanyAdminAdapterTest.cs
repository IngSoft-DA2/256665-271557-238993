﻿using Adapter;
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

    #region Create Construction Company Admin By Invitation

    [TestMethod]
    public void CreateConstructionCompanyAdminByInvitation_ReturnsCreateConstructionCompanyAdminResponse()
    {
        CreateConstructionCompanyAdminRequest createRequest = new CreateConstructionCompanyAdminRequest();

        _constructionCompanyAdminService.Setup(service =>
            service.CreateConstructionCompanyAdmin(It.IsAny<ConstructionCompanyAdmin>(), It.IsAny<Guid>(),null));

        CreateConstructionCompanyAdminResponse response =
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(createRequest,
                It.IsAny<Guid>());

        Assert.IsNotNull(response);

        _constructionCompanyAdminService.Verify(createConstructionCompanyAdminService =>
            createConstructionCompanyAdminService.CreateConstructionCompanyAdmin(
                It.IsAny<ConstructionCompanyAdmin>(), It.IsAny<Guid>()), Times.Once());
    }

    [TestMethod]
    public void CreateConstructionCompanyAdminByInvitation_ThrowsObjectErrorAdapterException()
    {
        CreateConstructionCompanyAdminRequest constructionCompanyDummy = new CreateConstructionCompanyAdminRequest();
        Guid invitationIdDummy = Guid.NewGuid();

        _constructionCompanyAdminService.Setup(constructionCompanyAdminService =>
                constructionCompanyAdminService.CreateConstructionCompanyAdmin(
                    It.IsAny<ConstructionCompanyAdmin>(),
                    It.IsAny<Guid>()))
            .Throws(new ObjectErrorServiceException("Specific Error"));

        Assert.ThrowsException<ObjectErrorAdapterException>(() =>
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdminconstructionCompanyDummy,
                invitationIdDummy));
        _constructionCompanyAdminService.VerifyAll();
    }

    [TestMethod]
    public void CreateConstructionCompanyAdminByInvitation_ThrowsObjectRepeatedAdapterException()
    {
        CreateConstructionCompanyAdminRequest createDummyRequest = new CreateConstructionCompanyAdminRequest();
        Guid invitationIdDummy = Guid.NewGuid();


        _constructionCompanyAdminService.Setup(constructionCompanyAdminService =>
                constructionCompanyAdminService.CreateConstructionCompanyAdmin(
                    It.IsAny<ConstructionCompanyAdmin>(),
                    It.IsAny<Guid>()))
            .Throws(new ObjectRepeatedServiceException());

        Assert.ThrowsException<ObjectRepeatedAdapterException>(() =>
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(createDummyRequest,
                invitationIdDummy));
        _constructionCompanyAdminService.VerifyAll();
    }

    [TestMethod]
    public void CreateConstructionCompanyAdminByInvitation_ThrowsUnknownAdapterException()
    {
        CreateConstructionCompanyAdminRequest createDummyRequest = new CreateConstructionCompanyAdminRequest();
        Guid invitationIdDummy = Guid.NewGuid();

        _constructionCompanyAdminService.Setup(constructionCompanyAdminService =>
                constructionCompanyAdminService.CreateConstructionCompanyAdmin(
                    It.IsAny<ConstructionCompanyAdmin>(),
                    It.IsAny<Guid>()))
            .Throws(new Exception("Unknown Error"));

        Assert.ThrowsException<UnknownAdapterException>(() =>
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(createDummyRequest,
                invitationIdDummy));
        _constructionCompanyAdminService.VerifyAll();
    }

    [TestMethod]
    public void CreateConstructionCompanyAdminByInvitation_ThrowsObjectNotFoundAdapterException()
    {
        CreateConstructionCompanyAdminRequest createDummyRequest = new CreateConstructionCompanyAdminRequest();
        Guid invitationIdDummy = Guid.NewGuid();

        _constructionCompanyAdminService.Setup(constructionCompanyAdminService =>
                constructionCompanyAdminService.CreateConstructionCompanyAdmin(
                    It.IsAny<ConstructionCompanyAdmin>(), It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundServiceException());

        Assert.ThrowsException<ObjectNotFoundAdapterException>(() =>
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(createDummyRequest,
                invitationIdDummy));
        _constructionCompanyAdminService.VerifyAll();
    }

    #endregion

    #region Create Construction Company Admin By Another Admin

    
    [TestMethod]
    
    public void CreateConstructionCompanyAdminByAnotherAdmin_ReturnsCreateConstructionCompanyAdminResponse()
    {
        CreateConstructionCompanyAdminRequest createRequest = new CreateConstructionCompanyAdminRequest();

        _constructionCompanyAdminService.Setup(service =>
            service.CreateConstructionCompanyAdmin(It.IsAny<ConstructionCompanyAdmin>()));

        CreateConstructionCompanyAdminResponse response =
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(createRequest);

        Assert.IsNotNull(response);

        _constructionCompanyAdminService.Verify(createConstructionCompanyAdminService =>
            createConstructionCompanyAdminService.CreateConstructionCompanyAdmin(
                It.IsAny<ConstructionCompanyAdmin>()), Times.Once());
    }
    

    #endregion
    
    
}