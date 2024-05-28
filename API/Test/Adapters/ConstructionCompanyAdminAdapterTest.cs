using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.AdministratorRequests;
using WebModel.Requests.ConstructionCompanyAdminRequests;
using WebModel.Responses.AdministratorResponses;
using WebModel.Responses.ConstructionCompanyAdminResponses;

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

    [TestMethod]
    public void CreateConstructionCompanyAdmin_ReturnsCreateConstructionCompanyAdminResponse()
    {
        CreateConstructionCompanyAdminRequest dummyCreateRequest = new CreateConstructionCompanyAdminRequest();

        _constructionCompanyAdminService.Setup(service =>
            service.CreateConstructionCompanyAdmin(It.IsAny<ConstructionCompanyAdmin>()));

        CreateConstructionCompanyAdminResponse response =
            _constructionCompanyAdminAdapter.CreateConstructionCompanyAdmin(dummyCreateRequest);

        Assert.IsNotNull(response);

        _constructionCompanyAdminService.Verify(createConstructionCompanyAdminService =>
                createConstructionCompanyAdminService.CreateConstructionCompanyAdmin(
                    It.IsAny<ConstructionCompanyAdmin>()), Times.Once());
    }
}