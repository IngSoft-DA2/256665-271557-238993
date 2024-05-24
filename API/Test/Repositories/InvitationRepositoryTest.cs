using System.Collections;
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
    #region Initializing Aspects

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

    #endregion

    #region Get All Invitations

    [TestMethod]
    public void GetAllInvitations_InvitationsAreReturn()
    {
        IEnumerable<Invitation> expectedInvitations = new List<Invitation> { _invitationInDb, _invitationInDb2 };

        IEnumerable<Invitation> invitationsRepositoryResponse = _invitationRepository.GetAllInvitations();

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

    #endregion

    #region Get All Invitations By Email

    [TestMethod]
    public void GetAllInvitationsByEmail_InvitationsAreReturn()
    {
        IEnumerable<Invitation> invitationsWithThatEmail = new List<Invitation>
        {
            new Invitation
            {
                Id = Guid.NewGuid(),
                Firstname = "Invitation",
                Lastname = "Invitation",
                Email = "invitation@gmail.com",
                ExpirationDate = DateTime.MaxValue,
                Status = StatusEnum.Pending
            },
        };

        _dbContext.Set<Invitation>().Add(invitationsWithThatEmail.ElementAt(0));
        _dbContext.SaveChanges();

        IEnumerable<Invitation> invitationsObtained =
            _invitationRepository.GetAllInvitationsByEmail(invitationsWithThatEmail.ElementAt(0).Email);

        Assert.IsTrue(invitationsWithThatEmail.SequenceEqual(invitationsObtained));
    }

    #endregion

    #region Get Invitation By Id

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

    #endregion

    #region Create Invitation

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
    #endregion

    #region Update Invitation

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

    #endregion

    #region Delete Invitation

    [TestMethod]
    public void DeleteInvitation_InvitationIsDeleted()
    {
        _invitationRepository.DeleteInvitation(_invitationInDb);

        Assert.IsNull(_invitationRepository.GetInvitationById(_invitationInDb.Id));
    }

    [TestMethod]
    public void DeleteInvitation_ThrowsUnknownException()
    {
        var _mockDbContext = new Mock<DbContext>(MockBehavior.Strict);
        _mockDbContext.Setup(dbContext => dbContext.Set<Invitation>()).Throws(new Exception());

        _invitationRepository = new InvitationRepository(_mockDbContext.Object);
        Assert.ThrowsException<UnknownRepositoryException>(() =>
            _invitationRepository.DeleteInvitation(new Invitation()));
        _mockDbContext.VerifyAll();
    }

    #endregion

    [TestCleanup]
    public void TestCleanup()
    {
        _dbContext.Database.EnsureDeleted();
    }
}