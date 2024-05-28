
using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.AdministratorRequests;
using WebModel.Responses.AdministratorResponses;

namespace Test.Adapters;

[TestClass]
public class ConstructionCompanyAdminAdapterTest
{
    #region Initialize
    
    private Mock<IConstructionCompanyAdminService> _constructionCompanyAdminService;
    private ConstructionCompanyAdminAdapter _constructionCompanyAdminAdapter;
    
    
    [TestInitialize]
    public void TestInitialize()
    {
        _constructionCompanyAdminService = new Mock<IConstructionCompanyAdminService>(MockBehavior.Strict);
        _constructionCompanyAdminAdapter = new ConstructionCompanyAdminAdapter(_constructionCompanyAdminService.Object);
    }
    
    #endregion
  
}