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
        throw new NotImplementedException();
    }

    public void CreateConstructionCompany(ConstructionCompany constructionCompanyToAdd)
    {
        throw new NotImplementedException();
    }
}