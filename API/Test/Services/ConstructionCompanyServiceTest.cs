using Domain;
using IRepository;
using Moq;
using ServiceLogic;

namespace Test.Services;

[TestClass]
public class ConstructionCompanyServiceTest
{
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

        constructionCompanyRepository.Setup(constructionCompanyRepository => constructionCompanyRepository.GetAllConstructionCompanies())
            .Returns(constructionCompaniesInDb);

        ConstructionCompanyService constructionCompanyService =
            new ConstructionCompanyService(constructionCompanyRepository.Object);

        IEnumerable<ConstructionCompany> constructionCompaniesObtained =
            constructionCompanyService.GetAllConstructionCompanies();

        Assert.AreEqual(constructionCompaniesInDb.Count(), constructionCompaniesObtained.Count());
        Assert.IsTrue(constructionCompaniesInDb.SequenceEqual(constructionCompaniesObtained));
    }
}