using WebModel.Requests.ConstructionCompanyRequests;
using WebModel.Responses.ConstructionCompanyResponses;

namespace IAdapter;

public interface IConstructionCompanyAdapter
{
    public IEnumerable<ConstructionCompanyResponse> GetConstructionCompanies();
    public CreateConstructionCompanyResponse CreateConstructionCompany(CreateConstructionCompanyRequest name);
}