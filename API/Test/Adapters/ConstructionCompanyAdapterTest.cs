using Adapter;
using Adapter.CustomExceptions;
using BuildingBuddy.API.Controllers;
using Domain;
using IAdapter;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.ConstructionCompanyResponses;

namespace Test.Adapters;

[TestClass]
public class ConstructionCompanyAdapterTest
{
    #region Initialize

    private Mock<IConstructionCompanyService> _constructionCompanyService;
    private ConstructionCompanyAdapter _constructionCompanyAdapter;

    [TestInitialize]
    public void Initialize()
    {
        _constructionCompanyService =
            new Mock<IConstructionCompanyService>(MockBehavior.Strict);

        _constructionCompanyAdapter = new ConstructionCompanyAdapter(_constructionCompanyService.Object);
    }

    #endregion

    #region Get all construction companies

    [TestMethod]
    public void GetAllConstructionCompanies_ReturnsConstructionCompanyResponses()
    {
        IEnumerable<ConstructionCompany> expectedServiceResponse = new List<ConstructionCompany>
        {
            new ConstructionCompany
            {
                Id = Guid.NewGuid(),
                Name = "Test Company 1"
            }
        };

        IEnumerable<GetConstructionCompanyResponse> expectedAdapterResponse =
            new List<GetConstructionCompanyResponse>
            {
                new GetConstructionCompanyResponse
                {
                    Id = expectedServiceResponse.First().Id,
                    Name = expectedServiceResponse.First().Name
                }
            };

        _constructionCompanyService.Setup(service => service.GetAllConstructionCompanies())
            .Returns(expectedServiceResponse);

        IEnumerable<GetConstructionCompanyResponse> adapterResponse =
            _constructionCompanyAdapter.GetAllConstructionCompanies();
        _constructionCompanyService.VerifyAll();

        Assert.AreEqual(expectedAdapterResponse.Count(), adapterResponse.Count());
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }

    [TestMethod]
    public void GetAllConstructionCompanies_ThrowUnknownAdapterException()
    {
        _constructionCompanyService.Setup(service => service.GetAllConstructionCompanies())
            .Throws(new Exception("Internal server error"));

        Assert.ThrowsException<UnknownAdapterException>(() =>
            _constructionCompanyAdapter.GetAllConstructionCompanies());
        _constructionCompanyService.VerifyAll();
    }

    #endregion

    #region Get construction company by id

    [TestMethod]
    public void GetConstructionCompanyById_ReturnsConstructionCompanyResponse()
    {
        ConstructionCompany expectedServiceResponse = new ConstructionCompany
        {
            Id = Guid.NewGuid(),
            Name = "Test Company 1"
        };

        GetConstructionCompanyResponse expectedAdapterResponse = new GetConstructionCompanyResponse
        {
            Id = expectedServiceResponse.Id,
            Name = expectedServiceResponse.Name
        };

        _constructionCompanyService.Setup(service => service.GetConstructionCompanyById(It.IsAny<Guid>()))
            .Returns(expectedServiceResponse);

        GetConstructionCompanyResponse adapterResponse =
            _constructionCompanyAdapter.GetConstructionCompanyById(It.IsAny<Guid>());
        _constructionCompanyService.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.Equals(adapterResponse));
    }

    #endregion
}