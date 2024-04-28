using Domain;

namespace IRepository;

public interface IConstructionCompanyRepository
{
    public IEnumerable<ConstructionCompany> GetAllConstructionCompanies();
}