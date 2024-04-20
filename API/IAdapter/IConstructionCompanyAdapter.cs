using WebModel.Requests.ConstructionCompanyRequests;
using WebModel.Responses.ConstructionCompanyResponses;

namespace IAdapter;

public interface IConstructionCompanyAdapter
{
    public IEnumerable<GetConstructionCompanyResponse> GetConstructionCompanies();
    public CreateConstructionCompanyResponse CreateConstructionCompany(CreateConstructionCompanyRequest name);
}