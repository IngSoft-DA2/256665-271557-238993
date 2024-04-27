using Domain;
using Domain.Enums;
using IRepository;
using Moq;
using Repositories.CustomExceptions;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class InvitationServiceTest
{
    #region Initialize

    private Mock<IInvitationRepository> _invitationRepository;
    private InvitationService _invitationService;
    private Invitation _invitationExample;

    [TestInitialize]
    public void Initialize()
    {
        _invitationRepository = new Mock<IInvitationRepository>(MockBehavior.Strict);
        _invitationService = new InvitationService(_invitationRepository.Object);
        _invitationExample = new Invitation
        {
            Id = Guid.NewGuid(),
            Firstname = "firstnameExample",
            Lastname = "lastnameExample",
            Email = "example@gmail.com",
            ExpirationDate = DateTime.MaxValue,
            Status = StatusEnum.Pending
        };
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

    #region Get invitation by Id

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

        Assert.ThrowsException<ObjectNotFoundServiceException>(() =>
            _invitationService.GetInvitationById(Guid.NewGuid()));
        _invitationRepository.VerifyAll();
    }

    [TestMethod]
    public void GetInvitationById_UnknownServiceExceptionIsThrown()
    {
        _invitationRepository.Setup(invitationRepository => invitationRepository.GetInvitationById(It.IsAny<Guid>()))
            .Throws(new UnknownRepositoryException("Internal Server Error"));

        Assert.ThrowsException<UnknownServiceException>(() => _invitationService.GetInvitationById(Guid.NewGuid()));
        _invitationRepository.VerifyAll();
    }

    #endregion

    #region Create Invitation

    [TestMethod]
    public void CreateInvitation_InvitationIsValidated()
    {

        _invitationRepository.Setup(invitationRepository =>
            invitationRepository.CreateInvitation(It.IsAny<Invitation>()));

        _invitationService.CreateInvitation(_invitationExample);

        _invitationRepository.Verify(_invitationRepository =>
            _invitationRepository.CreateInvitation(It.IsAny<Invitation>()), Times.Once);
    }


    [TestMethod]
    public void CreateInvitationWithEmptyFirstname_ThrowsObjectErrorServiceException()
    {
        Invitation invitationWithEmptyName = new Invitation();
        invitationWithEmptyName.Firstname = "";
        
        Assert.ThrowsException<ObjectErrorServiceException>(() => _invitationService.CreateInvitation(invitationWithEmptyName));
    }
    
    [TestMethod]
    public void GivenFirstnameWithSpecialChars_ShouldThrowException()
    {
        _invitationExample.Firstname = "Michael@";
        Assert.ThrowsException<ObjectErrorServiceException>(() => _invitationService.CreateInvitation(_invitationExample));
    }
    
    [TestMethod]
    public void CreateInvitationWithNumericFirstname_ThrowsObjectErrorServiceException()
    {
        _invitationExample.Firstname = "123";
        Assert.ThrowsException<ObjectErrorServiceException>(() => _invitationService.CreateInvitation(_invitationExample));
    }

    [TestMethod]
    public void CreateInvitationWithEmptyLastname_ThrowsObjectErrorServiceException()
    {
        _invitationExample.Lastname = "";
        Assert.ThrowsException<ObjectErrorServiceException>(() => _invitationService.CreateInvitation(_invitationExample));
    }
    
    [TestMethod]
    public void CreateInvitationWithNumericLastname_ThrowsObjectErrorServiceException()
    {
        _invitationExample.Lastname = "123";
        Assert.ThrowsException<ObjectErrorServiceException>(() => _invitationService.CreateInvitation(_invitationExample));
    }
    
    [TestMethod]
    public void CreateInvitationWithEmptyEmail_ThrowsObjectErrorServiceException()
    {
        _invitationExample.Email = "";
        Assert.ThrowsException<ObjectErrorServiceException>(() => _invitationService.CreateInvitation(_invitationExample));
    }
    
    [TestMethod]
    public void CreateInvitationWithIncorrectFormat_ThrowsObjectErrorServiceException()
    {
        _invitationExample.Email = "a@example";
        Assert.ThrowsException<ObjectErrorServiceException>(() => _invitationService.CreateInvitation(_invitationExample));
    }
    
    
    
    
    

    #endregion
}