using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

namespace DataAccess.Repositories;

public class InvitationRepository : IInvitationRepository
{
    #region Constructor and attributes
    
    private readonly DbContext _dbContext;

    public InvitationRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion
    
    #region Get All Invitations
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
    
    #endregion
    
    #region Get Invitation By Id

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
    
    #endregion
    
    #region Create Invitation

    public void CreateInvitation(Invitation invitationToAdd)
    {
        try
        {
            _dbContext.Set<Invitation>().Add(invitationToAdd);
            _dbContext.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }
    
    #endregion
    
    #region Update Invitation

    public void UpdateInvitation(Invitation invitationUpdated)
    {
        try
        {
            Invitation invitationInDb = _dbContext.Set<Invitation>().Find(invitationUpdated.Id);
            if(invitationInDb != null)
            {
                _dbContext.Set<Invitation>().Entry(invitationInDb).CurrentValues.SetValues(invitationUpdated);
                _dbContext.SaveChanges();
            }
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }
    
    #endregion
    
    #region Delete Invitation

    public void DeleteInvitation(Invitation invitationToDelete)
    {
        try
        {
            _dbContext.Set<Invitation>().Remove(invitationToDelete);
            _dbContext.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public IEnumerable<Invitation> GetAllInvitationsByEmail(string email)
    {
        return _dbContext.Set<Invitation>().Where(invitation => invitation.Email == email).ToList();
    }

    #endregion
}