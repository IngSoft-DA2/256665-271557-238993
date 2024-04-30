using Domain;
using IRepository;
using Microsoft.EntityFrameworkCore;

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
        IEnumerable<ConstructionCompany> constructionCompanies = _dbContext.Set<ConstructionCompany>().ToList();
        return constructionCompanies;
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