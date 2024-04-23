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

        Mock<IConstructionCompanyService> constructionCompanyService =
            new Mock<IConstructionCompanyService>(MockBehavior.Strict);

        constructionCompanyService.Setup(service => service.GetAllConstructionCompanies())
            .Returns(expectedServiceResponse);

        ConstructionCompanyAdapter constructionCompanyAdapter =
            new ConstructionCompanyAdapter(constructionCompanyService.Object);

        IEnumerable<GetConstructionCompanyResponse> adapterResponse =
            constructionCompanyAdapter.GetAllConstructionCompanies();
        constructionCompanyService.VerifyAll();

        Assert.AreEqual(expectedAdapterResponse.Count(), adapterResponse.Count());
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }

    [TestMethod]
    public void GetAllConstructionCompanies_ThrowUnknownAdapterException()
    {
        Mock<IConstructionCompanyService> constructionCompanyService =
            new Mock<IConstructionCompanyService>(MockBehavior.Strict);

        constructionCompanyService.Setup(service => service.GetAllConstructionCompanies())
            .Throws(new Exception("Internal server error"));

        ConstructionCompanyAdapter constructionCompanyAdapter =
            new ConstructionCompanyAdapter(constructionCompanyService.Object);

        Assert.ThrowsException<UnknownAdapterException>(() => constructionCompanyAdapter.GetAllConstructionCompanies());
        constructionCompanyService.VerifyAll();

    }

#endregion

}