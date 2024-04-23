using Adapter;
using BuildingBuddy.API.Controllers;
using Domain;
using IAdapter;
using IServiceLogic;
using Moq;
using WebModel.Responses.ConstructionCompanyResponses;

namespace Test.Adapters;

[TestClass]
public class ConstructionCompanyAdapterTest
{

    [TestMethod]
    public void GetAllConstructionCompanies()
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
        
        constructionCompanyService.Setup( service => service.GetAllConstructionCompanies()).Returns(expectedServiceResponse);

        ConstructionCompanyAdapter constructionCompanyAdapter =
            new ConstructionCompanyAdapter(constructionCompanyService.Object);
        
        IEnumerable<GetConstructionCompanyResponse> adapterResponse = constructionCompanyAdapter.GetAllConstructionCompanies();
        
        Assert.AreEqual(expectedAdapterResponse.Count(), adapterResponse.Count());
        Assert.IsTrue(expectedAdapterResponse.SequenceEqual(adapterResponse));
    }
    
    
    
    
    
    
    
    
}