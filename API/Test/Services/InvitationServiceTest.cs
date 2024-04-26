using Domain;
using Domain.Enums;
using IRepository;
using Moq;
using ServiceLogic;

namespace Test.Services;

[TestClass]
public class InvitationServiceTest
{
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

        Mock<IInvitationRepository> invitationRepositoryMock = new Mock<IInvitationRepository>(MockBehavior.Strict);
        invitationRepositoryMock.Setup(invitationRepository => invitationRepository.GetAllInvitations())
            .Returns(expectedRepositoryResponse);
        
        InvitationService invitationService = new InvitationService(invitationRepositoryMock.Object);
        
        IEnumerable<Invitation> actualResponse = invitationService.GetAllInvitations();
        invitationRepositoryMock.VerifyAll();
        
        Assert.AreEqual(expectedRepositoryResponse.Count(), actualResponse.Count());
        Assert.IsTrue(expectedRepositoryResponse.SequenceEqual(actualResponse));
        
    }
    
    
    
    
    
    
    
    
    
    
    
}