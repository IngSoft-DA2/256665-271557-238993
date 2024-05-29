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
    private  ConstructionCompanyAdmin _constructionCompanyAdminExample;
    [TestInitialize]
    public void Initilize()
    {
        _constructionCompanyAdminRepository = new Mock<IConstructionCompanyAdminRepository>(MockBehavior.Strict);
        _constructionCompanyAdminService =
            new ConstructionCompanyAdminService(_constructionCompanyAdminRepository.Object);
        
        
        _constructionCompanyAdminExample = new ConstructionCompanyAdmin
        {
            Id = Guid.NewGuid(),
            Firstname = "ConstructionCompanyAdminFirstname",
            Lastname = "ConstructionCompanyAdminLastname",
            Email = "ConstructionCompanyAdminEmail@gmail.com",
            Password = "ConstructionCompanyAdminPassword",
            ConstructionCompany = null,
            Role = SystemUserRoleEnum.ConstructionCompanyAdmin
        };
    }

    #endregion

    #region Create Construction Company Admin

    //Happy path
    [TestMethod]
    public void CreateConstructionCompanyAdmin_CreateConstructionCompanyAdminIsCreated()
    {
        
        _constructionCompanyAdminRepository.Setup(constructionCompanyAdminRepository =>
            constructionCompanyAdminRepository.CreateConstructionCompanyAdmin(It.IsAny<ConstructionCompanyAdmin>()));

        _constructionCompanyAdminService.CreateConstructionCompanyAdmin(_constructionCompanyAdminExample);

        _constructionCompanyAdminRepository.Verify(constructionCompanyAdminRepository =>
                constructionCompanyAdminRepository.CreateConstructionCompanyAdmin(It.IsAny<ConstructionCompanyAdmin>()),
            Times.Once);
    }

    #region Create Construction Company Admin , Domain Validations

    [TestMethod]
    public void CreateConstructionCompanyAdminWithEmptyFirstname_ThrowsObjectErrorServiceException()
    {
        _constructionCompanyAdminExample.Firstname = "";
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _constructionCompanyAdminService.CreateConstructionCompanyAdmin(_constructionCompanyAdminExample));
        
    }
    
    [TestMethod]
    public void CreateConstructionCompanyAdminWithEmptyLastname_ThrowsObjectErrorServiceException()
    {
        _constructionCompanyAdminExample.Lastname = "";
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _constructionCompanyAdminService.CreateConstructionCompanyAdmin(_constructionCompanyAdminExample));
        
    }

    #endregion

    #endregion
}