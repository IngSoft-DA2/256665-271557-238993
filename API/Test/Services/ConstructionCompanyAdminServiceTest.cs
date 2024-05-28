using Domain;
using Domain.Enums;
using IDataAccess;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;


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

    //Happy path
    [TestMethod]
    public void CreateConstructionCompanyAdmin_CreateConstructionCompanyAdminIsCreated()
    {
        ConstructionCompanyAdmin constructionCompanyAdminDummy = new ConstructionCompanyAdmin
        {
            Id = Guid.NewGuid(),
            Firstname = "ConstructionCompanyAdminFirstname",
            Lastname = "ConstructionCompanyAdminLastname",
            Email = "ConstructionCompanyAdminEmail@gmail.com",
            Password = "ConstructionCompanyAdminPassword",
            ConstructionCompany = null,
            Role = SystemUserRoleEnum.ConstructionCompanyAdmin
        };
        
        _constructionCompanyAdminRepository.Setup(constructionCompanyAdminRepository =>
            constructionCompanyAdminRepository.CreateConstructionCompanyAdmin(It.IsAny<ConstructionCompanyAdmin>()));

        _constructionCompanyAdminService.CreateConstructionCompanyAdmin(constructionCompanyAdminDummy);

        _constructionCompanyAdminRepository.Verify(constructionCompanyAdminRepository =>
                constructionCompanyAdminRepository.CreateConstructionCompanyAdmin(It.IsAny<ConstructionCompanyAdmin>()),
            Times.Once);
    }

    #region Create Construction Company Admin , Domain Validations

    [TestMethod]
    public void CreateConstructionCompanyAdminWithEmptyFirstname_ThrowsObjectErrorServiceException()
    {
        ConstructionCompanyAdmin constructionCompanyAdminWithError = new ConstructionCompanyAdmin()
        {
            Id = Guid.NewGuid(),
            Firstname = ""
        };
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _constructionCompanyAdminService.CreateConstructionCompanyAdmin(constructionCompanyAdminWithError));
        
    }

    #endregion

    #endregion
}