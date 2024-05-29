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
    private ConstructionCompanyAdmin _constructionCompanyAdminExample;

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

        _constructionCompanyAdminRepository.Setup(constructionCompanyAdminRepository =>
                constructionCompanyAdminRepository.GetAllConstructionCompanyAdmins())
            .Returns(new List<ConstructionCompanyAdmin>());

        _constructionCompanyAdminService.CreateConstructionCompanyAdmin(_constructionCompanyAdminExample);

        _constructionCompanyAdminRepository.Verify(constructionCompanyAdminRepository =>
                constructionCompanyAdminRepository.CreateConstructionCompanyAdmin(It.IsAny<ConstructionCompanyAdmin>()),
            Times.Once);
        _constructionCompanyAdminRepository.Verify(constructionCompanyAdminRepository =>
            constructionCompanyAdminRepository.GetAllConstructionCompanyAdmins(), Times.Once);
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

    [TestMethod]
    public void CreateConstructionCompanyAdminWithIncorrectEmail_ThrowsObjectErrorServiceException()
    {
        _constructionCompanyAdminExample.Email = "a.com";
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _constructionCompanyAdminService.CreateConstructionCompanyAdmin(_constructionCompanyAdminExample));
    }

    [TestMethod]
    public void CreateConstructionCompanyAdminWithIncorrectPassword_ThrowsObjectErrorServiceException()
    {
        _constructionCompanyAdminExample.Password = "pass";
        Assert.ThrowsException<ObjectErrorServiceException>(() =>
            _constructionCompanyAdminService.CreateConstructionCompanyAdmin(_constructionCompanyAdminExample));
    }

    #endregion

    #region Create Construction Company Admin , Repository Validations

    [TestMethod]
    public void CreateConstructionCompanyAdminWithExistingEmail_ThrowsObjectRepeatedServiceException()
    {
        _constructionCompanyAdminRepository.Setup(constructionCompanyAdminRepository =>
                constructionCompanyAdminRepository.GetAllConstructionCompanyAdmins())
            .Returns(new List<ConstructionCompanyAdmin> { _constructionCompanyAdminExample });

        Assert.ThrowsException<ObjectRepeatedServiceException>(() =>
            _constructionCompanyAdminService.CreateConstructionCompanyAdmin(_constructionCompanyAdminExample));
    }

    #endregion

    #endregion

    #region Get All Construction Company Admins

    [TestMethod]
    public void GetAllConstructionCompanyAdmins_ReturnsAllConstructionCompanyAdmins()
    {
        _constructionCompanyAdminRepository.Setup(constructionCompanyAdminRepository =>
                constructionCompanyAdminRepository.GetAllConstructionCompanyAdmins())
            .Returns(new List<ConstructionCompanyAdmin> { _constructionCompanyAdminExample });

        IEnumerable<ConstructionCompanyAdmin> allConstructionCompanyAdmins =
            _constructionCompanyAdminService.GetAllConstructionCompanyAdmins();

        _constructionCompanyAdminRepository.VerifyAll();

        Assert.AreEqual(1, allConstructionCompanyAdmins.Count());
        Assert.IsTrue(_constructionCompanyAdminExample.Equals(allConstructionCompanyAdmins.First()));
    }

    [TestMethod]
    public void GetAllConstructionCompanyAdmins_ThrowsUnexpectedServiceException()
    {
        _constructionCompanyAdminRepository.Setup(constructionCompanyAdminRepository =>
                constructionCompanyAdminRepository.GetAllConstructionCompanyAdmins())
            .Throws(new Exception());

        Assert.ThrowsException<UnknownServiceException>(() =>
            _constructionCompanyAdminService.GetAllConstructionCompanyAdmins());

        _constructionCompanyAdminRepository.VerifyAll();
    }

    #endregion
}