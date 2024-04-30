using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class InvitationRepository : IInvitationRepository
{
    private readonly DbContext _dbContext;

    public InvitationRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Invitation> GetAllInvitations()
    {
        return _dbContext.Set<Invitation>().ToList();
    }

    public Invitation GetInvitationById(Guid invitationId)
    {
        throw new NotImplementedException();
    }

    public void CreateInvitation(Invitation invitationToAdd)
    {
        throw new NotImplementedException();
    }

    public void UpdateInvitation(Invitation invitationUpdated)
    {
        throw new NotImplementedException();
    }

    public void DeleteInvitation(Invitation invitationToDelete)
    {
        throw new NotImplementedException();
    }
}