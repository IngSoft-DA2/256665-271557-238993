using WebModel.Responses.ConstructionCompanyResponses;

namespace IAdapter;

public interface IConstructionCompanyAdapter
{
    public IEnumerable<ConstructionCompanyResponse> GetConstructionCompanies();
}