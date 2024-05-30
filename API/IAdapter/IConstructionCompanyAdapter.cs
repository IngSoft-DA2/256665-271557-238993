using WebModel.Requests.ConstructionCompanyRequests;
using WebModel.Responses.ConstructionCompanyResponses;

namespace IAdapter;

public interface IConstructionCompanyAdapter
{
    public IEnumerable<GetConstructionCompanyResponse> GetAllConstructionCompanies();
    public CreateConstructionCompanyResponse CreateConstructionCompany(CreateConstructionCompanyRequest request);
}