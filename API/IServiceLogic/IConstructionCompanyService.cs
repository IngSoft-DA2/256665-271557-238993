using Domain;

namespace IServiceLogic;

public interface IConstructionCompanyService
{
    public IEnumerable<ConstructionCompany> GetAllConstructionCompanies();
}