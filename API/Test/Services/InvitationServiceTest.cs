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
                Email = "example@gmail.com",
                Status = StatusEnum.Pending,
                ExpirationDate = DateTime.MaxValue
            },
            new Invitation()
            {
                Id = Guid.NewGuid(),
                Email = "example2@gmail.com",
                Status = StatusEnum.Pending,
                ExpirationDate = DateTime.MaxValue
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
}