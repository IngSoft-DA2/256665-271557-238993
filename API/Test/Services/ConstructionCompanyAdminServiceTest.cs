using Domain;
using IDataAccess;
using Moq;
using ServiceLogic;


namespace Test.Services;

[TestClass]
public class ConstructionCompanyAdminServiceTest
{
    #region Initialize

    private Mock<IConstructionCompanyAdminRepository> _constructionCompanyAdminRepository;
    private ConstructionCompanyAdminService _constructionCompanyAdminService;

    [TestInitialize]
    public void Initilize()
    {
        _constructionCompanyAdminRepository = new Mock<IConstructionCompanyAdminRepository>(MockBehavior.Strict);
        _constructionCompanyAdminService =
            new ConstructionCompanyAdminService(_constructionCompanyAdminRepository.Object);
    }

    #endregion

    #region Create Construction Company Admin

    [TestMethod]
    public void CreateConstructionCompanyAdmin_CreateConstructionCompanyAdminIsCreated()
    {
        
        ConstructionCompanyAdmin constructionCompanyAdminDummy = new ConstructionCompanyAdmin();
        _constructionCompanyAdminRepository.Setup(constructionCompanyAdminRepository =>
            constructionCompanyAdminRepository.CreateConstructionCompanyAdmin(It.IsAny<ConstructionCompanyAdmin>()));
        
        _constructionCompanyAdminService.CreateConstructionCompanyAdmin(constructionCompanyAdminDummy);
        
        _constructionCompanyAdminRepository.Verify(constructionCompanyAdminRepository =>
            constructionCompanyAdminRepository.CreateConstructionCompanyAdmin(It.IsAny<ConstructionCompanyAdmin>()), Times.Once);
    }

    #endregion
}