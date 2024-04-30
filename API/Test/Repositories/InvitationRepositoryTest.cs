using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories.CustomExceptions;

namespace Test.Repositories;

[TestClass]

public class InvitationRepositoryTest
{
    private DbContext _dbContext;
    private InvitationRepository _invitationRepository;
    private Invitation _invitationInDb;
    private Invitation _invitationInDb2;

    [TestInitialize]
    public void TestInitialize()
    {
        _dbContext = CreateDbContext("InvitationRepositoryTest");
        _dbContext.Set<Invitation>();
        _invitationRepository = new InvitationRepository(_dbContext);
        
        _invitationInDb = new Invitation
        {
            Id = Guid.NewGuid(),
            Email = "person@gmail.com",
            ExpirationDate = DateTime.Now.AddDays(1),
            Firstname = "Someone",
            Lastname = "Else",
            Status = StatusEnum.Accepted
        };
        
        _invitationInDb2 = new Invitation
        {
            Id = Guid.NewGuid(),
            Email = "person2@gmail.com",
            ExpirationDate = DateTime.Now.AddDays(1),
            Firstname = "Someone2",
            Lastname = "Else2",
            Status = StatusEnum.Rejected
        };

        _dbContext.Set<Invitation>().Add(_invitationInDb);
        _dbContext.Set<Invitation>().Add(_invitationInDb2);
        _dbContext.SaveChanges();
    }
    
    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
    }

    [TestMethod]
    public void GetAllInvitations_InvitationsAreReturn()
    {
        IEnumerable<Invitation> expectedInvitations = new List<Invitation> {_invitationInDb, _invitationInDb2};
        
        IEnumerable<Invitation> invitationsRepositoryResponse= _invitationRepository.GetAllInvitations();
        
        Assert.IsTrue(expectedInvitations.SequenceEqual(invitationsRepositoryResponse));
    }
    
    [TestMethod]
    public void GetAllInvitations_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(dbContext => dbContext.Set<Invitation>()).Throws(new Exception());
        
        _invitationRepository = new InvitationRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _invitationRepository.GetAllInvitations());
        _mockDbContext.VerifyAll();
    }

    [TestMethod]
    public void GetInvitationById_InvitationIsReturn()
    {
        Invitation invitationResponse = _invitationRepository.GetInvitationById(_invitationInDb.Id);
        
        Assert.AreEqual(_invitationInDb, invitationResponse);
    }
    
    [TestMethod]
    public void GetInvitationById_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(dbContext => dbContext.Set<Invitation>()).Throws(new Exception());
        
        _invitationRepository = new InvitationRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _invitationRepository.GetInvitationById(_invitationInDb.Id));
        _mockDbContext.VerifyAll();
    }

    [TestMethod]
    public void CreateInvitation_InvitationIsCreated()
    {
        Invitation invitationToAdd = new Invitation
        {
            Id = Guid.NewGuid(),
            Email = "New@invita.com",
            ExpirationDate = DateTime.Now.AddDays(1),
            Firstname = "New",
            Lastname = "Invita",
            Status = StatusEnum.Pending
        };
        
        _invitationRepository.CreateInvitation(invitationToAdd);
        
        Invitation invitationResponse = _invitationRepository.GetInvitationById(invitationToAdd.Id);
        
        Assert.AreEqual(invitationToAdd, invitationResponse);   
    }
    
    [TestMethod]
    public void CreateInvitation_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(dbContext => dbContext.Set<Invitation>()).Throws(new Exception());
        
        _invitationRepository = new InvitationRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _invitationRepository.CreateInvitation(new Invitation()));
        _mockDbContext.VerifyAll();
    }
    
    [TestMethod]
    public void UpdateInvitation_InvitationIsUpdated()
    {
        _invitationInDb.Status = StatusEnum.Rejected;
        
        _invitationRepository.UpdateInvitation(_invitationInDb);
        
        Invitation invitationResponse = _invitationRepository.GetInvitationById(_invitationInDb.Id);
        
        Assert.AreEqual(_invitationInDb, invitationResponse);
    }
    
    [TestMethod]
    public void UpdateInvitation_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(dbContext => dbContext.Set<Invitation>()).Throws(new Exception());
        
        _invitationRepository = new InvitationRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() => _invitationRepository.UpdateInvitation(new Invitation()));
        _mockDbContext.VerifyAll();
    }
    
    [TestMethod]
    public void DeleteInvitation_InvitationIsDeleted()
    {
        _invitationRepository.DeleteInvitation(_invitationInDb);
        
        Assert.IsNull(_invitationRepository.GetInvitationById(_invitationInDb.Id));
    }



}