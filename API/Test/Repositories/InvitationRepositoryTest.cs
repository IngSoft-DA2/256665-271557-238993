using DataAccess.DbContexts;
using DataAccess.Repositories;
using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Test.Repositories;

[TestClass]

public class InvitationRepositoryTest
{
    private DbContext _dbContext;
    private InvitationRepository _invitationRepository;

    [TestInitialize]
    public void TestInitialize()
    {
        _dbContext = CreateDbContext("InvitationRepositoryTest");
        _dbContext.Set<Invitation>();
        _invitationRepository = new InvitationRepository(_dbContext);
    }
    
    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
        return new ApplicationDbContext(options);
    }

    [TestMethod]
    public void GetAllInvitations_InvitationsAreReturn()
    {
        Invitation invitationInDb = new Invitation
        {
            Id = Guid.NewGuid(),
            Email = "person@gmail.com",
            ExpirationDate = DateTime.Now.AddDays(1),
            Firstname = "Someone",
            Lastname = "Else",
            Status = StatusEnum.Accepted
        };
        
        Invitation invitationInDb2 = new Invitation
        {
            Id = Guid.NewGuid(),
            Email = "person2@gmail.com",
            ExpirationDate = DateTime.Now.AddDays(1),
            Firstname = "Someone2",
            Lastname = "Else2",
            Status = StatusEnum.Rejected
        };
        
        IEnumerable<Invitation> expectedInvitations = new List<Invitation> {invitationInDb, invitationInDb2};

        _dbContext.Set<Invitation>().Add(invitationInDb);
        _dbContext.Set<Invitation>().Add(invitationInDb2);
        _dbContext.SaveChanges();
        
        IEnumerable<Invitation> invitationsRepositoryResponse= _invitationRepository.GetAllInvitations();
        
        Assert.IsTrue(expectedInvitations.SequenceEqual(invitationsRepositoryResponse));
        
        

    }


}