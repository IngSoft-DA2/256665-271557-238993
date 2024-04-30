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
        try
        {
            return _dbContext.Set<Invitation>().ToList();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public Invitation GetInvitationById(Guid invitationId)
    {
        try
        {
            Invitation invitationFound = _dbContext.Set<Invitation>().Find(invitationId);
            return invitationFound;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public void CreateInvitation(Invitation invitationToAdd)
    {
        _dbContext.Set<Invitation>().Add(invitationToAdd);
        _dbContext.SaveChanges();
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