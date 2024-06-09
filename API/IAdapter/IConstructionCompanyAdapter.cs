using WebModel.Requests.ConstructionCompanyRequests;
using WebModel.Responses.ConstructionCompanyResponses;

namespace IAdapter;

public interface IConstructionCompanyAdapter
{
    public IEnumerable<GetConstructionCompanyResponse> GetAllConstructionCompanies();
    public CreateConstructionCompanyResponse CreateConstructionCompany(CreateConstructionCompanyRequest request);
    public void UpdateConstructionCompany (Guid id,UpdateConstructionCompanyRequest request);
    public GetConstructionCompanyResponse GetConstructionCompanyById(Guid constructionCompanyId);
    public GetConstructionCompanyResponse GetConstructionCompanyByUserCreatorId(Guid userId);
}