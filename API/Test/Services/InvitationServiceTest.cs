using Domain;
using Domain.Enums;
using IRepository;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class InvitationServiceTest
{
    #region Initialize

    private Mock<IInvitationRepository> _invitationRepository;
    private InvitationService _invitationService;

    [TestInitialize]
    public void Initialize()
    {
        _invitationRepository = new Mock<IInvitationRepository>(MockBehavior.Strict);
        _invitationService = new InvitationService(_invitationRepository.Object);
    }

    #endregion

    #region Get all invitations

    [TestMethod]
    public void GetAllInvitations_InvitationsAreReturned()
    {
        IEnumerable<Invitation> expectedRepositoryResponse = new List<Invitation>
        {
            new Invitation()
            {
                Id = Guid.NewGuid(),
                Firstname = "firstnameExample",
                Lastname = "lastnameExample",
                Email = "example@gmail.com",
                ExpirationDate = DateTime.MaxValue,
                Status = StatusEnum.Pending,
            },
            new Invitation()
            {
                Id = Guid.NewGuid(),
                Firstname = "firstname2Example",
                Lastname = "lastname2Example",
                Email = "example2@gmail.com",
                ExpirationDate = DateTime.MaxValue,
                Status = StatusEnum.Pending,
            }
        };


        _invitationRepository.Setup(invitationRepository => invitationRepository.GetAllInvitations())
            .Returns(expectedRepositoryResponse);


        IEnumerable<Invitation> actualResponse = _invitationService.GetAllInvitations();
        _invitationRepository.VerifyAll();

        Assert.AreEqual(expectedRepositoryResponse.Count(), actualResponse.Count());
        Assert.IsTrue(expectedRepositoryResponse.SequenceEqual(actualResponse));
    }

    [TestMethod]
    public void GetAllInvitations_UnknownServiceExceptionIsThrown()
    {
        _invitationRepository.Setup(invitationRepository => invitationRepository.GetAllInvitations())
            .Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() => _invitationService.GetAllInvitations());
    }

    #endregion

    [TestMethod]
    public void GetInvitationById_InvitationIsReturned()
    {
        Invitation expectedResponse = new Invitation()
        {
            Id = Guid.NewGuid(),
            Firstname = "firstnameExample",
            Lastname = "lastnameExample",
            Email = "example@gmail.com",
            Status = StatusEnum.Pending
        };

        _invitationRepository.Setup(invitationRepository => invitationRepository.GetInvitationById(It.IsAny<Guid>()))
            .Returns(expectedResponse);


        Invitation serviceResponse = _invitationService.GetInvitationById(Guid.NewGuid());
        _invitationRepository.VerifyAll();

        Assert.IsTrue(expectedResponse.Equals(serviceResponse));
    }
    
    [TestMethod]
    public void GetInvitationById_InvitationNotFound()
    {
        _invitationRepository.Setup(invitationRepository => invitationRepository.GetInvitationById(It.IsAny<Guid>()))
            .Returns(() => null);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() => _invitationService.GetInvitationById(Guid.NewGuid()));
        _invitationRepository.VerifyAll();
        
    }











}