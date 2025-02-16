using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repositories.CustomExceptions;

namespace DataAccess.Repositories;

public class ConstructionCompanyRepository : IConstructionCompanyRepository
{
    private readonly DbContext _dbContext;

    public ConstructionCompanyRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<ConstructionCompany> GetAllConstructionCompanies()
    {
        try
        {
            IEnumerable<ConstructionCompany> constructionCompanies = _dbContext.Set<ConstructionCompany>().ToList();
            return constructionCompanies;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public ConstructionCompany GetConstructionCompanyById(Guid idOfConstructionCompany)
    {
        try
        {
            return _dbContext.Set<ConstructionCompany>().Find(idOfConstructionCompany);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
    }

    public void CreateConstructionCompany(ConstructionCompany constructionCompanyToAdd)
    {
        try
        {
            _dbContext.Set<ConstructionCompany>().Add(constructionCompanyToAdd);
            _dbContext.SaveChanges();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownRepositoryException(exceptionCaught.Message);
        }
      
    }
}