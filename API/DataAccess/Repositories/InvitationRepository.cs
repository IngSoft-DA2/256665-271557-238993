using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

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
        try{
            return _dbContext.Set<Invitation>().ToList();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
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