using Domain;
using Domain.Enums;
using IAdapter;
using IServiceLogic;
using WebModel.Requests.ConstructionCompanyAdminRequests;
using WebModel.Responses.ConstructionCompanyAdminResponses;

namespace Adapter;

public class ConstructionCompanyAdminAdapter : IConstructionCompanyAdminAdapter
{
    private readonly IConstructionCompanyAdminService _constructionCompanyAdminService;

    public ConstructionCompanyAdminAdapter(IConstructionCompanyAdminService constructionCompanyAdminService)
    {
        _constructionCompanyAdminService = constructionCompanyAdminService;
    }
    
    public CreateConstructionCompanyAdminResponse CreateConstructionCompanyAdmin(CreateConstructionCompanyAdminRequest createRequest)
    {
        ConstructionCompanyAdmin constructionCompanyAdminToCreate = new ConstructionCompanyAdmin
        {
            Id = Guid.NewGuid(),
            Firstname = createRequest.Firstname,
            Lastname = createRequest.Lastname,
            Email = createRequest.Email,
            Password = createRequest.Password,
            ConstructionCompany = null,
            Role = SystemUserRoleEnum.ConstructionCompanyAdmin
        };
        
        _constructionCompanyAdminService.CreateConstructionCompanyAdmin(constructionCompanyAdminToCreate);
        
        CreateConstructionCompanyAdminResponse constructionCompanyAdminResponse = new CreateConstructionCompanyAdminResponse
        {
            Id = constructionCompanyAdminToCreate.Id
        };
        
        return constructionCompanyAdminResponse;
        
    }
}