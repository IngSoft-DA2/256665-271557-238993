using System.Collections;
using Domain;
using Domain.Enums;
using IRepository;
using IServiceLogic;
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
    private Mock<ISessionService> _sessionService;
    private InvitationService _invitationService;
    private Invitation _invitationExample;

    [TestInitialize]
    public void Initialize()
    {
        _invitationRepository = new Mock<IInvitationRepository>(MockBehavior.Strict);
        _sessionService = new Mock<ISessionService>(MockBehavior.Strict);
        _invitationService = new InvitationService(_invitationRepository.Object,_sessionService.Object);
        _invitationExample = new Invitation
        {
            Id = Guid.NewGuid(),
            Firstname = "firstnameExample",
            Lastname = "lastnameExample",
            Email = "example@gmail.com",
            ExpirationDate = DateTime.MaxValue,
            Status = StatusEnum.Pending,
            Role = SystemUserRoleEnum.Manager
        };
    }

    #endregion

    #region Get all invitations

    [TestMethod]
    //Happy path
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
    public void GetAllInvitationsByEmail_InvitationsAreReturned()
    {
        IEnumerable<Invitation> expectedRepositoryResponse = new List<Invitation>
        {
            new Invitation()
            {
                Id = Guid.NewGuid(),
                Firstname = "firstnameExample",
                Lastname = "lastnameExample",
                Email = "invitation@gmail.com",
                ExpirationDate = DateTime.MaxValue,
                Status = StatusEnum.Pending
            }
        };

        _invitationRepository.Setup(invitationRepository =>
                invitationRepository.GetAllInvitationsByEmail(It.IsAny<string>()))
            .Returns(expectedRepositoryResponse);

        IEnumerable<Invitation> invitationsObtained =
            _invitationService.GetAllInvitationsByEmail(expectedRepositoryResponse.First().Email);

        Assert.IsTrue(expectedRepositoryResponse.SequenceEqual(invitationsObtained));
    }

    #region Get all invitations, repository validation

    [TestMethod]
    public void GetAllInvitations_UnknownServiceExceptionIsThrown()
    {
        _invitationRepository.Setup(invitationRepository => invitationRepository.GetAllInvitations())
            .Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() => _invitationService.GetAllInvitations());
    }

    [TestMethod]
    public void GetAllInvitationsByEmail_UnknownServiceExceptionIsThrown()
    {
        _invitationRepository.Setup(invitationRepository =>
                invitationRepository.GetAllInvitationsByEmail(It.IsAny<string>()))
            .Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _invitationService.GetAllInvitationsByEmail(It.IsAny<string>()));
    }

    #endregion

    #endregion

    #region Get invitation By Id

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

    //Happy path
    [TestMethod]
    public void CreateInvitation_InvitationIsValidated()
    {
        Invitation invitationToCreate = new Invitation
        {
            Id = Guid.NewGuid(),
            Firstname = "Michael",
            Lastname = "Ken",
            Email = "Michael@gmail.com",
            ExpirationDate = DateTime.MaxValue,
            Status = StatusEnum.Pending,
            Role = SystemUserRoleEnum.Manager
        };

        _invitationRepository.Setup(invitationRepository =>
            invitationRepository.CreateInvitation(It.IsAny<Invitation>()));
        _invitationRepository.Setup(invitationRepository =>
            invitationRepository.GetAllInvitations()).Returns(new List<Invitation> { invitationToCreate });

        _invitationService.CreateInvitation(_invitationExample);
        _invitationRepository.VerifyAll();
    }

    #region Create Invitation, Domain Validations

    [TestMethod]
    public void CreateInvitationWithEmptyFirstname_ThrowsObjectErrorServiceException()
    {
        Invitation invitationWithEmptyName = new Invitation();
        invitationWithEmptyName.Firstname = "";

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.CreateInvitation(invitationWithEmptyName));
    }

    [TestMethod]
    public void CreateInvitationThatHasFirstnameWithSpecialChars_ShouldThrowException()
    {
        _invitationExample.Firstname = "Michael@";
        _invitationExample.Role = SystemUserRoleEnum.ConstructionCompanyAdmin;
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.CreateInvitation(_invitationExample));
    }

    [TestMethod]
    public void CreateInvitationThatHasFirstnameWithBlanks_ShouldThrowException()
    {
        _invitationExample.Firstname = "Mich  ael";
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.CreateInvitation(_invitationExample));
    }

    [TestMethod]
    public void CreateInvitationWithNumericFirstname_ThrowsObjectErrorServiceException()
    {
        _invitationExample.Firstname = "123";
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.CreateInvitation(_invitationExample));
    }

    [TestMethod]
    public void
        CreateInvitationWithEmptyLastname_ThrowsObjectErrorServiceException_WhenSystemUserIsAConstructionCompanyAdmin()
    {
        _invitationExample.Lastname = "";
        _invitationExample.Role = SystemUserRoleEnum.ConstructionCompanyAdmin;
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.CreateInvitation(_invitationExample));
    }

    [TestMethod]
    public void
        CreateInvitationWithLastnameThatHasBlanks_ShouldThrowException_WhenSystemUserIsAConstructionCompanyAdmin()
    {
        _invitationExample.Lastname = "Ken t";
        _invitationExample.Role = SystemUserRoleEnum.ConstructionCompanyAdmin;
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.CreateInvitation(_invitationExample));
    }

    [TestMethod]
    public void
        CreateInvitationWithLastnameThatHasSpecialChars_ShouldThrowException_WhenSystemUserIsAConstructionCompanyAdmin()
    {
        _invitationExample.Lastname = "Michael@";
        _invitationExample.Role = SystemUserRoleEnum.ConstructionCompanyAdmin;
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.CreateInvitation(_invitationExample));
    }

    [TestMethod]
    public void
        CreateInvitationWithNumericLastname_ThrowsObjectErrorServiceException_WhenSystemUserIsAConstructionCompanyAdmin()
    {
        _invitationExample.Lastname = "123";
        _invitationExample.Role = SystemUserRoleEnum.ConstructionCompanyAdmin;
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.CreateInvitation(_invitationExample));
    }

    [TestMethod]
    public void CreateInvitationWithEmptyEmail_ThrowsObjectErrorServiceException()
    {
        _invitationExample.Email = "";
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.CreateInvitation(_invitationExample));
    }

    [TestMethod]
    public void CreateInvitationWithIncorrectFormat_ThrowsObjectErrorServiceException()
    {
        _invitationExample.Email = "a@example";
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.CreateInvitation(_invitationExample));
    }

    [TestMethod]
    public void CreateInvitationThatHasAnExpirationDateThatIsBeforeToday_ThrowsObjectErrorServiceException()
    {
        DateTime expiredExpirationDate = DateTime.MinValue;
        _invitationExample.ExpirationDate = expiredExpirationDate;

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.CreateInvitation(_invitationExample));
    }

    [TestMethod]
    public void WhenCreatingAnInvitation_StatusShouldBePending()
    {
        Invitation invitationToCheckStatus = new Invitation();
        Assert.AreEqual(StatusEnum.Pending, (invitationToCheckStatus).Status);
    }

    [TestMethod]
    public void CreateInvitationWithWrongRole_ThrowsObjectErrorServiceException()
    {
        _invitationExample.Role = SystemUserRoleEnum.Admin;
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.CreateInvitation(_invitationExample));
    }

    #endregion

    #region Create Invitation, Repository Validations

    [TestMethod]
    public void CreateInvitationThatTheEmailHasPendingValidInvitation_ThrowsObjectRepeatedServiceException()
    {
        IEnumerable<Invitation> invitationsFromDb = new List<Invitation>
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
            .Returns(invitationsFromDb);

        Assert.ThrowsException<ObjectRepeatedServiceException>(() =>
            _invitationService.CreateInvitation(_invitationExample));

        _invitationRepository.VerifyAll();
    }
    
    [TestMethod]
    public void CreateInvitationForAnAuthenticatedUser_ThrowsObjectErrorServiceException()
    {

        Invitation invitationAccepted = new Invitation
        {
            Id = Guid.NewGuid(),
            Firstname = "firstnameExample",
            Lastname = "lastnameExample",
            Email = "example@gmail.com",
            ExpirationDate = DateTime.MaxValue,
            Status = StatusEnum.Accepted,
            Role = SystemUserRoleEnum.Manager
        };
        
        _sessionService.Setup(sessionService => sessionService.IsUserAuthenticated(It.IsAny<string>()))
            .Returns(true);

        _invitationRepository.Setup(invitationRepository => invitationRepository.GetAllInvitations())
            .Returns(new List<Invitation?> { invitationAccepted });

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.CreateInvitation(_invitationExample));
        
        _sessionService.VerifyAll();
    }

    [TestMethod]
    public void CreateInvitation_UnknownServiceExceptionIsThrown()
    {
        _invitationRepository.Setup(invitationRepository =>
                invitationRepository.CreateInvitation(It.IsAny<Invitation>()))
            .Throws(new UnknownRepositoryException("Internal Error"));

        Assert.ThrowsException<UnknownServiceException>(() =>
            _invitationService.CreateInvitation(_invitationExample));
    }

    #endregion

    #endregion

    #region Update Invitation By Id

    //Happy path
    [TestMethod]
    public void UpdateInvitationById_InvitationIsUpdated()
    {
        _invitationRepository.Setup(invitationRepository =>
            invitationRepository.UpdateInvitation(It.IsAny<Invitation>()));


        _invitationRepository.Setup(invitationRepository => invitationRepository.GetInvitationById(It.IsAny<Guid>()))
            .Returns(_invitationExample);

        Invitation invitationWithUpdates = new Invitation
        {
            Id = _invitationExample.Id,
            Status = StatusEnum.Rejected,
            ExpirationDate = DateTime.MaxValue,
            Role = SystemUserRoleEnum.Manager
        };
        Guid idOfInvitationToUpdate = _invitationExample.Id;

        _invitationService.UpdateInvitation(idOfInvitationToUpdate, invitationWithUpdates);

        _invitationRepository.VerifyAll();
    }

    [TestMethod]
    //Happy path
    public void PendingInvitation_ExpirationDateCanBeUpdated()
    {
        _invitationExample.ExpirationDate = DateTime.Now.AddDays(1);

        Invitation invitationWithUpdates = new Invitation
        {
            Status = StatusEnum.Pending,
            ExpirationDate = DateTime.MaxValue,
            Role = SystemUserRoleEnum.ConstructionCompanyAdmin
        };

        _invitationRepository.Setup(invitationRepository => invitationRepository.GetInvitationById(It.IsAny<Guid>()))
            .Returns(_invitationExample);
        _invitationRepository.Setup(_invitationRepository =>
            _invitationRepository.UpdateInvitation(It.IsAny<Invitation>()));

        _invitationService.UpdateInvitation(_invitationExample.Id, invitationWithUpdates);

        _invitationRepository.VerifyAll();
    }

    #region Update Invitation By Id, Repository Validations

    [TestMethod]
    public void UpdateInvitationById_ThrowsObjectNotFoundServiceException()
    {
        _invitationRepository.Setup(invitationRepository => invitationRepository.GetInvitationById(It.IsAny<Guid>()))
            .Returns(() => null);

        Assert.ThrowsException<ObjectNotFoundServiceException>(() =>
            _invitationService.UpdateInvitation(Guid.NewGuid(), new Invitation()));

        _invitationRepository.VerifyAll();
    }

    [TestMethod]
    public void UpdateInvitationById_ThrowsUnknownServiceException()
    {
        _invitationRepository.Setup(invitationRepository => invitationRepository.GetInvitationById(It.IsAny<Guid>()))
            .Returns(_invitationExample);

        Invitation invitationUpdated = new Invitation
        {
            Status = StatusEnum.Accepted,
            ExpirationDate = DateTime.MaxValue,
            Role = SystemUserRoleEnum.ConstructionCompanyAdmin
        };

        _invitationRepository
            .Setup(invitationRepository => invitationRepository.UpdateInvitation(It.IsAny<Invitation>()))
            .Throws(new UnknownRepositoryException("Internal Error"));

        Assert.ThrowsException<UnknownServiceException>(() =>
            _invitationService.UpdateInvitation(Guid.NewGuid(), invitationUpdated));

        _invitationRepository.VerifyAll();
    }

    [TestMethod]
    public void UpdateInvitationStatus_WhenStatusIsNotPending_ThrowsObjectErrorServiceException()
    {
        _invitationExample.Status = StatusEnum.Rejected;

        Invitation invitationWithUpdates = new Invitation
        {
            Status = StatusEnum.Accepted,
            ExpirationDate = DateTime.MaxValue
        };

        _invitationRepository.Setup(invitationRepository => invitationRepository.GetInvitationById(It.IsAny<Guid>()))
            .Returns(_invitationExample);

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.UpdateInvitation(_invitationExample.Id, invitationWithUpdates));

        _invitationRepository.VerifyAll();
    }

    [TestMethod]
    public void UpdateStatus_CannotBeDoneIfExpirationDateIsExpired()
    {
        _invitationExample.ExpirationDate = DateTime.MinValue;

        Invitation invitationWithUpdates = new Invitation
        {
            Status = StatusEnum.Accepted,
            ExpirationDate = DateTime.MinValue
        };

        _invitationRepository.Setup(invitationRepository => invitationRepository.GetInvitationById(It.IsAny<Guid>()))
            .Returns(_invitationExample);

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.UpdateInvitation(_invitationExample.Id, invitationWithUpdates));

        _invitationRepository.VerifyAll();
    }

    [TestMethod]
    public void WhenStatusIsPending_InvitationThatIsNotNearToExpire_CannotUpdateExpirationDate()
    {
        _invitationExample.ExpirationDate = DateTime.Now.AddDays(10);

        Invitation invitationWithUpdates = new Invitation
        {
            Status = StatusEnum.Pending,
            ExpirationDate = DateTime.MaxValue
        };

        _invitationRepository.Setup(invitationRepository => invitationRepository.GetInvitationById(It.IsAny<Guid>()))
            .Returns(_invitationExample);

        Assert.ThrowsException<ObjectErrorServiceException>(() => _invitationService.UpdateInvitation
            (_invitationExample.Id, invitationWithUpdates));

        _invitationRepository.VerifyAll();
    }

    [TestMethod]
    public void UpdateExpirationDate_FromAnInvitationThatIsNotPending_ThrowsObjectErrorServiceException()
    {
        _invitationExample.Status = StatusEnum.Pending;

        Invitation invitationWithUpdates = new Invitation
        {
            Status = StatusEnum.Accepted,
            ExpirationDate = DateTime.MinValue
        };

        _invitationRepository.Setup(invitationRepository => invitationRepository.GetInvitationById(It.IsAny<Guid>()))
            .Returns(_invitationExample);

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.UpdateInvitation(_invitationExample.Id, invitationWithUpdates));

        _invitationRepository.VerifyAll();
    }

    [TestMethod]
    public void UpdateInvitationButWithoutChanges_ThrowsObjectRepeatedException()
    {
        _invitationExample.ExpirationDate = DateTime.MinValue;

        _invitationRepository.Setup(invitationRepository =>
            invitationRepository.GetInvitationById(It.IsAny<Guid>())).Returns(_invitationExample);


        Assert.ThrowsException<ObjectRepeatedServiceException>(() =>
            _invitationService.UpdateInvitation(It.IsAny<Guid>(), _invitationExample));
    }

    #endregion

    #region Update Invitation By Id, Domain Validations

    [TestMethod]
    public void UpdateInvitationWithExpirationDateExpired_ThrowsObjectErrorServiceException()
    {
        _invitationExample.ExpirationDate = new DateTime(2023, 1, 1);

        Invitation invitationWithUpdates = new Invitation
        {
            Status = StatusEnum.Pending,
            ExpirationDate = DateTime.MinValue
        };

        _invitationRepository.Setup(invitationRepository => invitationRepository.GetInvitationById(It.IsAny<Guid>()))
            .Returns(_invitationExample);

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.UpdateInvitation(_invitationExample.Id, invitationWithUpdates));

        _invitationRepository.VerifyAll();
    }

    #endregion

    #endregion

    #region Delete Invitation By Id

    //Happy path
    [TestMethod]
    public void DeleteInvitationById_InvitationIsDeleted()
    {
        _invitationRepository.Setup(invitationRepository => invitationRepository.GetInvitationById(It.IsAny<Guid>()))
            .Returns(_invitationExample);
        _invitationRepository.Setup(invitationRepository =>
            invitationRepository.DeleteInvitation(It.IsAny<Invitation>()));

        _invitationService.DeleteInvitation(_invitationExample.Id);

        _invitationRepository.VerifyAll();
    }

    #region Delete Invitation By Id, Repository Validations

    [TestMethod]
    public void DeleteInvitationThatIsAccepted_ThrowsObjectErrorServiceException()
    {
        _invitationExample.Status = StatusEnum.Accepted;

        _invitationRepository.Setup(invitationRepository => invitationRepository.GetInvitationById(It.IsAny<Guid>()))
            .Returns(_invitationExample);

        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _invitationService.DeleteInvitation(_invitationExample.Id));

        _invitationRepository.VerifyAll();
    }

    [TestMethod]
    public void DeleteInvitationThatIsNotInDb_ThrowsNotFoundException()
    {
        _invitationRepository.Setup(invitationRepository => invitationRepository.GetInvitationById(It.IsAny<Guid>()))
            .Returns(() => null);

        Assert.ThrowsException<ObjectNotFoundServiceException>(
            () => _invitationService.DeleteInvitation(Guid.NewGuid()));
    }

    [TestMethod]
    public void DeleteInvitation_ThrowsUnknownServiceException()
    {
        _invitationRepository.Setup(invitationRepository => invitationRepository.GetInvitationById(It.IsAny<Guid>()))
            .Returns(_invitationExample);
        _invitationRepository
            .Setup(invitationRepository => invitationRepository.DeleteInvitation(It.IsAny<Invitation>()))
            .Throws(new UnknownRepositoryException("Internal Error"));

        Assert.ThrowsException<UnknownServiceException>(
            () => _invitationService.DeleteInvitation(_invitationExample.Id));
    }

    #endregion

    #endregion
}