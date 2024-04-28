using Domain;
using IRepository;
using Moq;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class ConstructionCompanyServiceTest
{
    #region TestInitialize

    private Mock<IConstructionCompanyRepository> _constructionCompanyRepository;
    private ConstructionCompanyService _constructionCompanyService;

    [TestInitialize]
    public void TestInitialize()
    {
        _constructionCompanyRepository = new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);
        _constructionCompanyService = new ConstructionCompanyService(_constructionCompanyRepository.Object);
    }

    #endregion

    #region GetAllConstructionCompanies

    //Happy Path
    [TestMethod]
    public void GetAllConstructionCompanies_ConstructionCompaniesAreReturn()
    {
        IEnumerable<ConstructionCompany> constructionCompaniesInDb = new List<ConstructionCompany>
        {
            new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Company 1"
            },
            new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Company 2"
            }
        };

        Mock<IConstructionCompanyRepository> constructionCompanyRepository =
            new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);

        constructionCompanyRepository.Setup(constructionCompanyRepository =>
                constructionCompanyRepository.GetAllConstructionCompanies())
            .Returns(constructionCompaniesInDb);

        ConstructionCompanyService constructionCompanyService =
            new ConstructionCompanyService(constructionCompanyRepository.Object);

        IEnumerable<ConstructionCompany> constructionCompaniesObtained =
            constructionCompanyService.GetAllConstructionCompanies();

        Assert.AreEqual(constructionCompaniesInDb.Count(), constructionCompaniesObtained.Count());
        Assert.IsTrue(constructionCompaniesInDb.SequenceEqual(constructionCompaniesObtained));
    }

    #region Get All Construction Companies, Repository Validations

    [TestMethod]
    public void GetAllConstructionCompanies_UnknownServiceExceptionIsThrown()
    {
        Mock<IConstructionCompanyRepository> constructionCompanyRepository =
            new Mock<IConstructionCompanyRepository>(MockBehavior.Strict);

        constructionCompanyRepository.Setup(constructionCompanyRepository =>
                constructionCompanyRepository.GetAllConstructionCompanies())
            .Throws(new Exception());

        ConstructionCompanyService constructionCompanyService =
            new ConstructionCompanyService(constructionCompanyRepository.Object);

        Assert.ThrowsException<UnknownServiceException>(() => constructionCompanyService.GetAllConstructionCompanies());
    }

    #endregion

    #endregion
    
    
}